using Autofac;
using Autofac.Extensions.DependencyInjection;
using core.settings;
using entities;
using gateways.repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Npgsql;
using seguranca;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using services;
using web.Logger;
using FluentValidation;
using services.commands.core;
using MediatR;
using core.seedwork;
using core.seedwork.interfaces;
using hateoas.formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;

namespace web
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _logger = logger;
            Configuration = configuration;
#if DEBUG
            InitializeAndConfigureEvolveMigrations();
#endif
        }

        private void InitializeAndConfigureEvolveMigrations()
        {
            try
            {
                var cnx = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));

                var evolve = new Evolve.Evolve(cnx, msg => _logger.LogInformation(msg))
                {
                    Locations = new[] { "Resources/db/migration/postgres" },
                    IsEraseDisabled = true,
                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Database migration failed.", ex);
                throw;
            }
        }

        private IContainer applicationContainer;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddMvc(o => o.OutputFormatters.Add(new JsonHateoasFormatter())).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContextPool<EFApplicationContext>(optionsAction =>
            {
#if DEBUG
                optionsAction.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
#else
                optionsAction.UseNpgsql("Server=165.227.193.200;Port=5432;User Id=postgres;Password=q2w3e4r5t6;Database=sgagro;Timeout=1024;CommandTimeout=10000;");
#endif
                optionsAction.UseLazyLoadingProxies(true);
            });

            services.AddIdentity<Usuario, Perfil>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.Lockout.AllowedForNewUsers = true;
            })
           .AddEntityFrameworkStores<EFApplicationContext>()
           .AddDefaultTokenProviders();

            services.AddMvcCore(o => o.OutputFormatters.Add(new JsonHateoasFormatter()))
              .AddAuthorization(options =>
              {
                    options.AddPolicy("Permission", policyBuilder =>
                    {
                        policyBuilder.Requirements.Add(new PermissionAuthorizationRequirement());
                    });
              })
              .AddJsonFormatters()
              .AddJsonOptions(options =>
              {
                  options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                  options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
                  options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                  options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
              });

            services.AddSingleton(_ => new JsonSerializer
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateFormatString = "dd/MM/yyyy HH:mm:ss"
            });

            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.Configure<SwaggerSettings>(Configuration.GetSection("Swagger"));

            services.Configure<RequestLocalizationOptions>(options =>
            {
#pragma warning disable CC0030 // Make Local Variable Constant.
                var cultureBr = "pt-BR";
#pragma warning restore CC0030 // Make Local Variable Constant.
                var suppCulture = new[]
                {
                    new CultureInfo(cultureBr)
                };

                options.DefaultRequestCulture = new RequestCulture(cultureBr);
                options.SupportedCultures = suppCulture;
                options.SupportedUICultures = suppCulture;
            });

            //services.AddHateoas();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials().Build());
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("165.227.193.200"));
            });

            services.AddDirectoryBrowser();

            ConfigureLogging(services);

            ConfigureSwagger(services);

            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddTransient(s => s.GetService<IHttpContextAccessor>().HttpContext.User);

            AddMediatr(services);

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<AspNetUser>().As<IUser>();
            containerBuilder.RegisterModule<ServicesModule>();
            containerBuilder.RegisterModule<WebModule>();

            containerBuilder.Populate(services);

            applicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(applicationContainer);
        }

        private void ConfigureLogging(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            var swaggerSettings = Configuration.GetSection("Swagger").Get<SwaggerSettings>();
            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerSettings.Version,
                    new Info
                    {
                        Title = swaggerSettings.Title,
                        Version = swaggerSettings.Version,
                        Description = swaggerSettings.Description
                    });

                var caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                var nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                var caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });
        }

        private static void AddMediatr(IServiceCollection services)
        {
            const string applicationAssemblyName = nameof(services);
            var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

            AssemblyScanner.FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));

            services.AddMediatR(assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            UserManager<Usuario> userManager,
            RoleManager<Perfil> roleManager,
            EFApplicationContext context)
        {
            //StartScheduler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseResponseCompression();
            app.UseMvc();

            var swaggerSettings = Configuration.GetSection("Swagger").Get<SwaggerSettings>();

            loggerFactory.AddContext(LogLevel.Warning, Configuration.GetConnectionString("DefaultConnection"));

#if DEBUG
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", swaggerSettings.Title);
                    c.RoutePrefix = string.Empty;
                    c.DocumentTitle = swaggerSettings.Title;
                });

#endif

            app.UseDefaultFiles();
            app.UseStaticFiles();
            // for Linux compatibility
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            app.UseResponseCompression();
            app.UseCors("CorsPolicy");

            DbInitializer.Initialize(userManager, roleManager, context);
        }
    }
}

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>()
#if DEBUG
                .UseUrls("http://localhost:5000")
#else
                .UseUrls("http://165.227.193.200:5000")
#endif
                .UseKestrel( options => {
                options.Limits.MaxRequestBodySize = long.MaxValue;
            });
        }
    }
}

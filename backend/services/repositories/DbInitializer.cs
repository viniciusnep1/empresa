using System;
using entities;
using Microsoft.AspNetCore.Identity;
using seguranca;
using entities.empresa;

namespace gateways.repositories
{
    public static class DbInitializer
    {
        public static void Initialize(
            UserManager<Usuario> userManager,
            RoleManager<Perfil> roleManager,
            EFApplicationContext context)
        {
            context.Database.EnsureCreated();
            SeedRoles(roleManager);
            SeedUsers(context, userManager);
        }

        private static void SeedUsers(EFApplicationContext context, UserManager<Usuario> userManager)
        {
            if (userManager.FindByNameAsync("suporte").Result == null)
            {
                var user = new Usuario
                {
                    Id = Guid.Parse("762238c2-7e4e-4033-a59e-ef0eb042b4fa"),
                    Nome = "Suporte",
                    UserName = "suporte",
                    Email = "suporte@gmail.com"
                };

              

                var perfil = new Perfil
                {
                    Name = "Administrador",
                    DataAlteracao = DateTime.Now,
                    DataCriacao = DateTime.Now,
                    Desativado = true,
                };

                context.Roles.Add(perfil);

                var modulo = new Modulo
                {
                     Nome = "Apontamento Mão Obra",
                     Codigo = "APONTAMENTO_MAO_OBRA",
                };

                context.ModulosSet.Add(modulo);

                var perfilModulo = new PerfilModulo
                {
                     PerfilId = perfil.Id,
                     ModuloId = modulo.Id
                };

                context.PerfilModuloSet.Add(perfilModulo);

                var perfilModeloPermissaoRead = new PerfilModuloPermissao
                {
                    PerfilModuloId = perfilModulo.Id,
                    Permissao = new Permissao
                    {
                        Nome = "Leitura",
                        Codigo = "READ"
                    }
                };
                context.PerfilModuloPermissaoSet.Add(perfilModeloPermissaoRead);

                var perfilModeloPermissaoCreate = new PerfilModuloPermissao
                {
                    PerfilModuloId = perfilModulo.Id,
                    Permissao = new Permissao
                    {
                        Nome = "Criar",
                        Codigo = "CREATE"
                    }
                };
                context.PerfilModuloPermissaoSet.Add(perfilModeloPermissaoCreate);

                var perfilModeloPermissaoUpdate = new PerfilModuloPermissao
                {
                    PerfilModuloId = perfilModulo.Id,
                    Permissao = new Permissao
                    {
                        Nome = "Atualizar",
                        Codigo = "UPDATE"
                    }
                };
                context.PerfilModuloPermissaoSet.Add(perfilModeloPermissaoUpdate);

                var perfilModeloPermissaoDelete = new PerfilModuloPermissao
                {
                    PerfilModuloId = perfilModulo.Id,
                    Permissao = new Permissao
                    {
                        Nome = "Deletar",
                        Codigo = "DELETE"
                    }
                };
                context.PerfilModuloPermissaoSet.Add(perfilModeloPermissaoDelete);


                context.SaveChanges();

                user.Papeis.Add(new UsuarioPerfil
                {
                    RoleId = perfil.Id,
                    UserId = user.Id,
                });

                var result = userManager.CreateAsync(user, "123456").Result;
            }
        }

        private static void SeedRoles(RoleManager<Perfil> roleManager)
        {
            
        }
    }
}

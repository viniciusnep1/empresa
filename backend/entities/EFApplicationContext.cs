using System;
using core.lib.extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using seguranca;
using entities.logs;
using entities.empresa;

namespace entities
{
    public class EFApplicationContext : IdentityDbContext<Usuario, Perfil, Guid, UsuarioClaim, UsuarioPerfil, UsuarioLogin, PerfilClaim, UsuarioToken>
    {
        public virtual DbSet<Equipe> EquipeSet { get; set; }
        public virtual DbSet<Pessoa> PessoaSet { get; set; }
        public virtual DbSet<PessoaTipo> PessoaTipoSet { get; set; }
        public virtual DbSet<StoredEvent> StoredEvent { get; set; }
        public virtual DbSet<EquipePessoa> EquipePessoaSet { get; set; }
        public virtual DbSet<PessoaCategoriaPessoa> PessoaCategoriaPessoaSet { get; set; }




        #region Segurança

        public virtual DbSet<Modulo> ModulosSet { get; set; }
        public virtual DbSet<PerfilModulo> PerfilModuloSet { get; set; }
        public virtual DbSet<PerfilModuloPermissao> PerfilModuloPermissaoSet { get; set; }
        public virtual DbSet<Permissao> PermissaoSet { get; set; }

        #endregion

        public EFApplicationContext(DbContextOptions<EFApplicationContext> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>()
                  .HasMany(e => e.Papeis)
                  .WithOne()
                  .HasForeignKey(e => e.UserId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UsuarioPerfil>()
                .HasOne(e => e.User)
                .WithMany(e => e.Papeis)
                .HasForeignKey(e => e.UserId);

            builder.Entity<UsuarioPerfil>()
                .HasOne(e => e.Role)
                .WithMany(e => e.Papeis)
                .HasForeignKey(e => e.RoleId);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Replace table names
                var currentTableName = builder.Entity(entity.Name).Metadata.Relational().TableName;
                builder.Entity(entity.Name).ToTable(currentTableName.ToLower());

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Relational().ColumnName.ToSnakeCase();
                }

                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
                }
            }
        }
    }
}

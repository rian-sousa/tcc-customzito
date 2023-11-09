using CustomBancoLib;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Customzito.Services.CZDatabase
{
    public class CZContext : DbContext
    {
        
        public DbSet<TB_Perfil> TbPerfil { get; set; }
        public DbSet<TB_Produto> TbProduto { get; set; }
        public DbSet<TB_Endereco> TbEndereco { get; set;}
        public DbSet<TB_Colecao> TbColecao { get; set;}
        public DbSet<TB_Carrinho> TbCarrinho { get; set; }
        public DbSet<TD_TipoUsuario> TdTipoUsuario { get; set; }
        public DbSet<TD_TiposVestimenta> TdTipoVestimenta { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public DbSet<IdentityUser> IdentityUsers { get; set; }

        public CZContext(DbContextOptions<CZContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TB_Produto>()
            .Property(p => p.Avaliacao)
            .HasColumnType("decimal(3, 1)");

            builder.Entity(delegate (EntityTypeBuilder<AspNetRole> entity)
            {
                entity.HasIndex((AspNetRole e) => e.NormalizedName, "RoleNameIndex").IsUnique().HasFilter("([NormalizedName] IS NOT NULL)");
            });
            builder.Entity(delegate (EntityTypeBuilder<AspNetUser> entity)
            {
                entity.HasIndex((AspNetUser e) => e.NormalizedUserName, "UserNameIndex").IsUnique().HasFilter("([NormalizedUserName] IS NOT NULL)");
                entity.HasMany((AspNetUser d) => d.Roles).WithMany((AspNetRole p) => p.Users).UsingEntity("AspNetUserRole", (EntityTypeBuilder<Dictionary<string, object>> l) => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"), (EntityTypeBuilder<Dictionary<string, object>> r) => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"), delegate (EntityTypeBuilder<Dictionary<string, object>> j)
                {
                    j.HasKey("UserId", "RoleId");
                    j.ToTable("AspNetUserRoles");
                    j.HasIndex(new string[1] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                });
            });
            builder.Entity(delegate (EntityTypeBuilder<AspNetUserLogin> entity)
            {
                entity.HasKey((AspNetUserLogin e) => new { e.LoginProvider, e.ProviderKey });
            });
            builder.Entity(delegate (EntityTypeBuilder<AspNetUserToken> entity)
            {
                entity.HasKey((AspNetUserToken e) => new { e.UserId, e.LoginProvider, e.Name });
            });

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

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
        public DbSet<TD_Material> Material { get; set; }
        public DbSet<TD_Frete> Fretes { get; set; }
        public DbSet<TD_FaixasPrecos> FaixasPrecos { get; set; }
        public DbSet<TB_ArquivosCustomBase> ArquivosCustomBase { get; set; }
        public DbSet<TB_PedidoCustomizado> PedidosCustomizados { get; set; }
        public DbSet<TD_Status> Status { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public DbSet<AspNetUsers> AspNetUser { get; set; }
        public DbSet<IdentityUserClaim<string>> IdentityUserClaim { get; set; }

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
            builder.Entity(delegate (EntityTypeBuilder<AspNetUsers> entity)
            {
                entity.HasIndex((AspNetUsers e) => e.NormalizedUserName, "UserNameIndex").IsUnique().HasFilter("([NormalizedUserName] IS NOT NULL)");
                entity.HasMany((AspNetUsers d) => d.Roles).WithMany((AspNetRole p) => p.Users).UsingEntity("AspNetUserRole", (EntityTypeBuilder<Dictionary<string, object>> l) => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"), (EntityTypeBuilder<Dictionary<string, object>> r) => r.HasOne<AspNetUsers>().WithMany().HasForeignKey("UserId"), delegate (EntityTypeBuilder<Dictionary<string, object>> j)
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

            //builder.Entity<IdentityUserClaim<Guid>>().HasKey(p => new { p.UserId, p.RoleId })
            //
            builder.Entity<TB_PedidoCustomizado>()
            .HasOne(pc => pc.Material)
            .WithMany()
            .HasForeignKey(pc => pc.IdMaterial);


            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

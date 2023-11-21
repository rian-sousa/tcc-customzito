using CustomBancoLib;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;
using System.Reflection.Emit;

namespace Customzito.Services.CZDatabase
{
    public class CZContext : IdentityDbContext<AspNetUsers, IdentityRole, string>
    {

        public CZContext(DbContextOptions<CZContext> options)
        : base(options)
        {
        }

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
        public DbSet<IdentityUserRole<string>> IdentityUserRole { get; set; }
        public virtual DbSet<IdentityRole> IdentityRole { get; set; }
        //public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        //public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        //public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        //public DbSet<AspNetUsers> AspNetUser { get; set; }
        public DbSet<IdentityUserClaim<string>> IdentityUserClaim { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TB_Produto>()
                .Property(p => p.Avaliacao)
                .HasColumnType("decimal(3, 1)");

            builder.Entity<IdentityRole>().ToTable("AspNetRole");

            builder.Entity(delegate (EntityTypeBuilder<AspNetRole> entity)
            {
                entity.HasIndex((AspNetRole e) => e.NormalizedName, "RoleNameIndex").IsUnique().HasFilter("([NormalizedName] IS NOT NULL)");
            });

            //builder.Entity<AspNetUserRoles>(entity =>
            //{
            //    // Define a composite primary key
            //    entity.HasKey(ur => new { ur.UserId, ur.RoleId });


            //    entity.HasOne(ur => ur.User)
            //        .WithMany(u => u.UserRoles)
            //        .HasForeignKey(ur => ur.UserId)
            //        .IsRequired();

            //    entity.HasOne(ur => ur.Role)
            //        .WithMany(r => r.RoleUsers)
            //        .HasForeignKey(ur => ur.RoleId)
            //        .IsRequired();

            //    entity.ToTable("AspNetUserRoles");
            //});

            //builder.Entity<IdentityRoleClaim<string>>()
            //        .ToTable("AspNetRoleClaims")
            //        .HasKey(x => x.Id)// Se necessário, ajuste o nome da tabela
            //        .HasOne<IdentityRole>()  // Aqui está a mudança
            //        .WithMany()
            //        .HasForeignKey(x => x.RoleId)
            //        .IsRequired();

            //builder.ApplyConfiguration(new AspNetRoleClaimsConfig());

            //builder.Entity<IdentityRoleClaim<string>>()
            //.HasOne<AspNetRoleClaims>()
            //.WithMany()
            //.HasForeignKey(x => x.RoleId)
            //.HasPrincipalKey(y => y.Id)  // Definir a principal key compatível
            //.IsRequired();


            //builder.Entity<IdentityRoleClaim<string>>(entity =>
            //{
            //    entity.HasKey(x => x.Id);

            //    entity.ToTable("AspNetRoleClaims");

            //    entity.HasOne<IdentityRole>()
            //    .WithMany()
            //    .HasForeignKey(x => x.RoleId)
            //    .IsRequired();

            //});

            //builder.Entity<IdentityUserClaim<string>>()
            //    .ToTable("AspNetUserClaims");

            //builder.Entity(delegate (EntityTypeBuilder<IdentityUser> entity)
            //{
            //    entity.HasKey((AspNetUserLogin e) => new { e.LoginProvider, e.ProviderKey });
            //});

            //builder.Entity(delegate (EntityTypeBuilder<AspNetUserToken> entity)
            //{
            //    entity.HasKey((AspNetUserToken e) => new { e.UserId, e.LoginProvider, e.Name });
            //});

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

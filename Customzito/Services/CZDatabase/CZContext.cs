using CustomBancoLib;
using Microsoft.EntityFrameworkCore;

namespace Customzito.Services.CZDatabase
{
    public class CZContext : DbContext
    {
        public CZContext(DbContextOptions<CZContext> options) : base(options) { }

        public DbSet<TB_Perfil> TbPerfil { get; set; }
        public DbSet<TB_Produto> TbProduto { get; set; }
        public DbSet<TB_Endereco> TbEndereco { get; set;}
        public DbSet<TB_Colecao> TbColecao { get; set;}
        public DbSet<TB_Carrinho> TbCarrinho { get; set; }
        public DbSet<TD_TipoUsuario> TdTipoUsuario { get; set; }
        public DbSet<TD_TiposVestimenta> TdTipoVestimenta { get; set; }
    }
}

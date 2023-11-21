namespace Customzito.Models.Auxiliares
{
    public class PedidoCompletoModel
    {
        public int IdPerfil { get; set; }
        public int IdCarrinho { get; set; }
        public string Protocolo { get; set; }
        public float? ValorTotal { get; set; }
        //public string Email { get; set; }
        public string? TipoPedido { get; set; }
        public string Status { get; set; }
    }
}

using CustomBancoLib;
using Customzito.Models.Auxiliares;
using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customzito.Controllers
{
    [AllowAnonymous]
    public class CarrinhoController : Controller
    {

        private readonly CZContext _czContext;
        private readonly CarrinhoModel _carrinho;

        public CarrinhoController(CZContext context, CarrinhoModel carrinho)
        {
            _czContext = context;
            _carrinho = carrinho;
        }


        public IActionResult Index(int? IdCarrinho)
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
            string layout = userRole == "Cliente" ? "/Views/Shared/_LayoutCliente.cshtml" : (userRole == "Administrador") ? "/Views/Shared/_LoggedLayout.cshtml" : "/Views/Shared/_Layout.cshtml";
            TempData["Layout"] = layout;

            if(IdCarrinho is not null)
            {

            }



            return View();
        }


        public IActionResult AdicionarItemCarrinho(int IdProduto)
        {
            var Produto = _czContext.TbProduto
                .FirstOrDefault(x => x.IdProduto == IdProduto);

            if(Produto != null)
            {
                TB_Carrinho carrinho = new()
                {
                    IdProduto = IdProduto,
                    Quantidade = 1
                };

                _czContext.TbCarrinho.Add(carrinho);

                return RedirectToAction("Index");

            }

            return View();
        }

        public IActionResult VisualizarCarrinho()
        {
            return View();
        }
    }
}

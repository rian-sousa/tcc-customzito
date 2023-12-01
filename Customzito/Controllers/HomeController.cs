using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customzito.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly CZContext _czContext;

        public HomeController(CZContext context)
        {
            _czContext = context;
        }

        
        public async Task<ActionResult> Index(int? id)
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
            string layout = userRole == "Cliente" ? "/Views/Shared/_LayoutCliente.cshtml" : (userRole == "Administrador")? "/Views/Shared/_LoggedLayout.cshtml" : "/Views/Shared/_Layout.cshtml";

            TempData["Layout"] = layout;

            if(userRole == "Cliente")
            {
                var userIdPerfil = HttpContext.Session.GetInt32("IdPerfil");

                var Perfil = await _czContext.TbPerfil
                    .FirstOrDefaultAsync(x => x.IdPerfil == userIdPerfil);

                var Endereco = await _czContext.TbEndereco
                    .FirstOrDefaultAsync(x => x.IdEndereco == Perfil.IdEndereco);

                var Carrinho = await _czContext.TbCarrinho
                    .FirstOrDefaultAsync(x => x.IdPerfil == Perfil.IdPerfil);

                ViewBag.PerfilCliente = Perfil;
                ViewBag.EnderecoCliente = Endereco; //continuar a partir daqui, preciso arranjar um jeito de passar o id do carrinho pra view de carrinho
                ViewBag.CarrinhoCliente = Carrinho;
            }

            var TopProdutos = await _czContext.TbProduto
                .OrderBy(produto => produto.qtd)
                .Take(5)
                .ToListAsync();

            ViewBag.TopProdutos = TopProdutos;


            return View();
        }

    }
}

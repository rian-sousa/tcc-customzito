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
                var Perfil = await _czContext.TbPerfil
                    .FirstOrDefaultAsync(x => x.IdPerfil == id);

                ViewBag.PerfilCliente = Perfil;
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

using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customzito.Controllers
{
    [AllowAnonymous]
    public class ConfigPerfilController : Controller
    {
        private CZContext _czContext;

        public ConfigPerfilController(CZContext context)
        {
            _czContext = context;
        }



        public async Task<IActionResult> Index(int? id)
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
            string layout = userRole == "Cliente" ? "/Views/Shared/_LayoutCliente.cshtml" : (userRole == "Administrador") ? "/Views/Shared/_LoggedLayout.cshtml" : "/Views/Shared/_Layout.cshtml";

            TempData["Layout"] = layout;


            var userIdPerfil = HttpContext.Session.GetInt32("IdPerfil");

            var Perfil = await _czContext.TbPerfil
                .FirstOrDefaultAsync(x => x.IdPerfil == userIdPerfil);

            var Endereco = await _czContext.TbEndereco
                .FirstOrDefaultAsync(x => x.IdEndereco == Perfil.IdEndereco);

            ViewBag.PerfilCliente = Perfil;
            ViewBag.EnderecoCliente = Endereco;


            return View();
        }
    }
}

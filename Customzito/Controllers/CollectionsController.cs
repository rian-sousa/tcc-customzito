using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customzito.Controllers
{
    [AllowAnonymous]
    public class CollectionsController : Controller
    {
        private readonly CZContext _czContext;

        public CollectionsController(CZContext czContext)
        {
            _czContext = czContext;
        }

        
        public async Task<IActionResult> Index()
        {
            var Colecoes = await _czContext.TbColecao
                .ToListAsync();

            var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
            string layout = userRole == "Cliente" ? "~/Views/Shared/_LayoutCliente.cshtml" : "~/Views/Shared/_Layout.cshtml";

            ViewData["Layout"] = layout;


            ViewBag.Colecoes = Colecoes;

            return View();
        }

        public async Task<IActionResult> Quebradas()
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
            string layout = userRole == "Cliente" ? "/Views/Shared/_LayoutCliente.cshtml" : "/Views/Shared/_Layout.cshtml";

            TempData["Layout"] = layout;

            return View();
        }

        public IActionResult Classic()
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
            string layout = userRole == "Cliente" ? "/Views/Shared/_LayoutCliente.cshtml" : "/Views/Shared/_Layout.cshtml";

            TempData["Layout"] = layout;

            return View();
        }
    }
}

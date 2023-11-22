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

        
        public async Task<ActionResult> Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
            string layout = userRole == "Cliente" ? "/Views/Shared/_LayoutCliente.cshtml" : (userRole == "Administrador")? "/Views/Shared/_LoggedLayout.cshtml" : "/Views/Shared/_Layout.cshtml";

            TempData["Layout"] = layout;

            return View();
        }

    }
}

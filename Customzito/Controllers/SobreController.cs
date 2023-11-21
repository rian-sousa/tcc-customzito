using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customzito.Controllers
{
    public class SobreController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
            string layout = userRole == "Cliente" ? "~/Views/Shared/_LayoutCliente.cshtml" : "~/Views/Shared/_Layout.cshtml";

            ViewData["Layout"] = layout;

            return View();
        }
    }
}

using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customzito.Controllers
{
    [AllowAnonymous]
    public class CustomController : Controller
    {
        private readonly CZContext _czContext;

        public CustomController(CZContext context)
        {
            _czContext = context;
        }
        
        public async Task<ActionResult> Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
            string layout = userRole == "Cliente" ? "~/Views/Shared/_LayoutCliente.cshtml" : "~/Views/Shared/_Layout.cshtml";

            ViewData["Layout"] = layout;

            return View();
        }
    }
}

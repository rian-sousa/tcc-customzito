﻿using Microsoft.AspNetCore.Mvc;

namespace Customzito.Controllers
{
    public class ConfigPerfilController : Controller
    {
        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
            string layout = userRole == "Cliente" ? "/Views/Shared/_LayoutCliente.cshtml" : (userRole == "Administrador") ? "/Views/Shared/_LoggedLayout.cshtml" : "/Views/Shared/_Layout.cshtml";

            TempData["Layout"] = layout;


            return View();
        }
    }
}

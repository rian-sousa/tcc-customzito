using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Customzito.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CustomBancoLib;
using System.Data;

namespace Customzito.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AspNetUsers> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<AspNetUsers> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
                HttpContext.Session.SetString("UserRole","DefaultRole");

                string layout = "/Views/Shared/_Layout.cshtml";

                TempData["Layout"] = layout;

                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}

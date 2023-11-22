using CustomBancoLib;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Customzito.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly SignInManager<AspNetUsers> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<AspNetUsers> signInManager,
            ILogger<LoginModel> logger,
            UserManager<AspNetUsers> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string? ReturnUrl { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/Home/Index");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);



            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            

            if (ModelState.IsValid)
            {
                var match = Regex.Match(Input.Email, @"^(.*)@.*$");
                var username = match.Groups[1].Value;

                var result = await _signInManager.PasswordSignInAsync(username, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    var user = await _userManager.FindByNameAsync(username);

                    if(user.UserName == "riansousadf")
                    {
                        await _userManager.AddToRoleAsync(user, "Administrador");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Cliente");
                    }

                    var roles = await _userManager.GetRolesAsync(user);

                    HttpContext.Session.SetString("UserRole", roles.FirstOrDefault() ?? "DefaultRole");

                    if (roles.Contains("Administrador"))
                    {
                        returnUrl ??= Url.Content("~/Interno/Index");
                        return RedirectToAction("Index", "Interno");
                    }
                    else
                    {
                        returnUrl ??= Url.Content("~/Home/Index");
                        return RedirectToAction("Index", "Home");
                    }

                }


                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
            else
            {
                // Acesso aos erros de validação
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                // Fazer o que for necessário com os erros, como exibi-los em uma view ou fazer algum tipo de tratamento
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }

                // Retorne uma resposta adequada, como uma view com os erros ou uma mensagem de erro
                return Page();
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

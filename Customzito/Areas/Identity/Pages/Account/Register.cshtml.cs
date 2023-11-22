﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Customzito.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using CustomBancoLib;
using System.Text.RegularExpressions;
using Customzito.Services.CZDatabase;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Customzito.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AspNetUsers> _signInManager;
        private UserManager<AspNetUsers> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;
        private CZContext _czContext;

        public RegisterModel(
            UserManager<AspNetUsers> userManager,
            SignInManager<AspNetUsers> signInManager,
            ILogger<RegisterModel> logger,
            //IEmailSender emailSender,
            CZContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
            _czContext = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var Role = await _czContext.IdentityRole
                .FirstOrDefaultAsync(x => x.Id == "7E43994F-FDE6-4061-AA98-B76B17E64079");

            //List<IdentityRole> Roles = new List<IdentityRole> { RoleCliente };

            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var match = Regex.Match(Input.Email, @"^(.*)@.*$");
                var username = match.Groups[1].Value;

                username = Regex.Replace(username, @"[^a-zA-Z0-9]", "");

                TB_Endereco endereco = new() { Quadra = $"custom{username}"};

                await _czContext.TbEndereco
                    .AddAsync(endereco);

                await _czContext.SaveChangesAsync();

                var EnderecoRegistrado = await _czContext.TbEndereco
                    .FirstOrDefaultAsync(x => x.Quadra == $"custom{username}");

                TB_Perfil perfil = new() { Nome = username, Sobrenome = "Sobrenome", DataNascimento = DateTime.Now.AddYears(-18), IdTipoUsuario = 2, IdEndereco = EnderecoRegistrado.IdEndereco};

                await _czContext.TbPerfil
                    .AddAsync(perfil);

                await _czContext.SaveChangesAsync();

                var perfilRegistrado = await _czContext.TbPerfil
                    .FirstOrDefaultAsync(x => x.Nome == username);

                var user = new AspNetUsers { UserName = username, Email = Input.Email, IdPerfil = perfilRegistrado.IdPerfil };                               

                var result = await _userManager.CreateAsync(user, Input.Password);

                var AspCriado = await _czContext.AspNetUsers
                    .FirstOrDefaultAsync(x => x.UserName == username);

                await _userManager.AddToRoleAsync(AspCriado, "Cliente");
               

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
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

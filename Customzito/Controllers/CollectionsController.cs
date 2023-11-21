﻿using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customzito.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly CZContext _czContext;

        public CollectionsController(CZContext czContext)
        {
            _czContext = czContext;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        public async Task<IActionResult> Quebradas()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Classic()
        {
            return View();
        }
    }
}

﻿using CustomBancoLib;
using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customzito.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly CZContext _czContext;

        public ProdutosController(CZContext context)
        {
            _czContext = context;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var Produtos = await _czContext.TbProduto
                .ToListAsync();

            ViewBag.Produtos = Produtos;

            var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
            string layout = userRole == "Cliente" ? "/Views/Shared/_LayoutCliente.cshtml" : (userRole == "Administrador") ? "/Views/Shared/_LoggedLayout.cshtml" : "/Views/Shared/_Layout.cshtml";

            TempData["Layout"] = layout;


            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Item(int id)
        
        {
            var Produto = await _czContext.TbProduto
                .FirstOrDefaultAsync(x => x.IdProduto == id);

            var Tipo = await _czContext.TdTipoVestimenta
                .FirstOrDefaultAsync(x => x.IdTipoVestimenta == Produto.IdTipoVestimenta);

            var userRole = HttpContext.Session.GetString("UserRole") ?? "DefaultRole";
            string layout = userRole == "Cliente" ? "/Views/Shared/_LayoutCliente.cshtml" : (userRole == "Administrador") ? "/Views/Shared/_LoggedLayout.cshtml" : "/Views/Shared/_Layout.cshtml";

            TempData["Layout"] = layout;

            ViewBag.Tipo = Tipo.Descricao;

            return View(Produto);
        }
    }
}
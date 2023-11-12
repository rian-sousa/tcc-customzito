using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customzito.Controllers
{
    public class InternoController : Controller
    {
        private CZContext _czContext;
        public InternoController(CZContext contexto) 
        {
            _czContext = contexto;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ControleEstoque()
        {
            var Colecoes = await _czContext.TbColecao
                .ToListAsync();

            var Produtos = await _czContext.TbProduto
                .ToListAsync();

            ViewBag.lstProdutos = Produtos;
            ViewBag.lstColecoes = Colecoes;

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Adicionar()
        {
            return default;
        }
    }
}

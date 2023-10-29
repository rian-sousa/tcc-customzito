using CustomBancoLib;
using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customzito.Controllers
{
    public class ItemController : Controller
    {
        private readonly CZContext _czContext;

        public ItemController(CZContext context)
        {
            _czContext = context;
        }

        public async Task<ActionResult> Index()
        {
            var Produtos = await _czContext.TbProduto
                .ToListAsync();

            ViewBag.Produtos = Produtos;    

            return View();
        }
    }
}
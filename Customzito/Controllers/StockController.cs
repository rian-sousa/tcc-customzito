using CustomBancoLib;
using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customzito.Controllers
{
    public class StockController : Controller
    {
        private readonly CZContext _czContext;

        public StockController(CZContext context)
        {
            _czContext = context;
        }

        public async Task<ActionResult> Index()
        {
            //var result = await _czContext.TbColecao.CountAsync();
            //TempData["QtdColecao"] = result;

            return View();
        }
    }
}

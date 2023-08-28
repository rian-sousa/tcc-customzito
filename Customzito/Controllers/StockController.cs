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
            for (int i = 0; i > 2; i++)
            {
                bool? rand;
                if (i == i % 2)
                {
                    rand = true;
                }
                else
                {
                    rand = false;
                }
                TB_Colecao obj = new TB_Colecao { Descricao = "TESTE! " + i, Limitado = rand };
                var contar = await _czContext.TbColecao.AddAsync(obj);
            }

            TempData["QtdColecao"] = await _czContext.TbColecao.CountAsync();

            return View();
        }
    }
}

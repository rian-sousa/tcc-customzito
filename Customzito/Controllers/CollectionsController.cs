using Customzito.Services.CZDatabase;
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

        public async Task<IActionResult> Index()
        {
            var Colecoes = await _czContext.TbColecao
                .ToListAsync();

            ViewBag.Colecoes = Colecoes;

            return View();
        }

        public async Task<IActionResult> Quebradas()
        {
            return View();
        }

        public IActionResult Classic()
        {
            return View();
        }
    }
}

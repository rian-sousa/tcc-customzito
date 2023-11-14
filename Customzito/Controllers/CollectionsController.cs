using Customzito.Services.CZDatabase;
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

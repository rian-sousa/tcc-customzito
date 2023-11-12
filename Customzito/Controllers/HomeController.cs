using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customzito.Controllers
{
    public class HomeController : Controller
    {
        private readonly CZContext _czContext;

        public HomeController(CZContext context)
        {
            _czContext = context;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            return View();
        }

    }
}

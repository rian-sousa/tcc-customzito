using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Mvc;

namespace Customzito.Controllers
{
    public class CustomController : Controller
    {
        private readonly CZContext _czContext;

        public CustomController(CZContext context)
        {
            _czContext = context;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }
    }
}

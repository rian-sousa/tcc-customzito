using Microsoft.AspNetCore.Mvc;

namespace Customzito.Controllers
{
    public class ExternoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

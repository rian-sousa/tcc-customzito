using Microsoft.AspNetCore.Mvc;

namespace Customzito.Controllers
{
    public class SobreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

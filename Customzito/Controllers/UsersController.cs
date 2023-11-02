using Microsoft.AspNetCore.Mvc;

namespace Customzito.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Phone { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;

        public string EmailConfirmed { get; set; } = string.Empty;
        public string PasswordConfirmed { get; set; } 
        public string PhoneConfirmed { get; set; } = string.Empty;

        public string PhoneConfirmedEmail { get; set; } = string.Empty;
        public string PhoneConfirmedPassword { get; set; } = string.Empty;

    }
}

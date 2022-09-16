using Microsoft.AspNetCore.Mvc;

namespace SurfsUp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

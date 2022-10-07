using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SurfsUp_API.Controllers
{
    public class UsersController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost(Name = "Create U")]
        [ValidateAntiForgeryToken]
        public IActionResult Index()
        {
            return View();
        }
    }
}

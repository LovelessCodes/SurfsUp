using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurfsUp.Areas.Identity.Data;
using System.Data;

namespace SurfsUp.Controllers
{
    public class ManageUsersController : Controller
    {
        private readonly UserManager<SurfsUpUser> userManager;
      
        public ManageUsersController(UserManager<SurfsUpUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users.ToList());
        }
    }
}

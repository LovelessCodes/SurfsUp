using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


      
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {


            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);


                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListUsers");
            }
        }
      

    }
}

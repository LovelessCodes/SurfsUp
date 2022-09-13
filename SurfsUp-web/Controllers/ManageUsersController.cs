using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfsUp.Areas.Identity.Data;
using System.Collections.Generic;
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
        public async Task<IActionResult> ListUsers()
        {
            var users = userManager.Users;
            var admins = await userManager.GetUsersInRoleAsync("Admin");
            List<List<SurfsUpUser>> model = new()
            {
                users.ToList(), admins.ToList()
            };
            return View("ListUsers", model);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> ToggleAdmin(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            var userRoles = await userManager.GetRolesAsync(user);
            IdentityResult result = new IdentityResult();
            if (userRoles.Contains("Administrator"))
            {
                result = await userManager.RemoveFromRoleAsync(user, "Admin");
            }
            else
            {
                result = await userManager.AddToRoleAsync(user, "Admin");
            }
            if (result.Succeeded)
            {
                return RedirectToAction("ListUsers");
            }
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Description);
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction("ListUsers");
        }


        [Authorize(Roles = "Administrator")]
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

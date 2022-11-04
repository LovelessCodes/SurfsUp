using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfsUp_API.Areas.Identity.Data;
using System.Collections.Generic;
using System.Data;

namespace SurfsUp.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<SurfsUpUser> userManager;

        public UsersController(UserManager<SurfsUpUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var users = userManager.Users;
            var admins = await userManager.GetUsersInRoleAsync("Admin");
            List<List<SurfsUpUser>> model = new()
            {
                users.ToList(), admins.ToList()
            };
            return View(model);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult> ToggleAdmin(string id)
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
                result = await userManager.RemoveFromRoleAsync(user, "Admin");
            else
                result = await userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Description);
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string id)
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
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(nameof(Index));
            }
        }
      

    }
}

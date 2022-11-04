using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurfsUp.ViewModels;
namespace SurfsUp.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpPost]
        [Authorize("Administrator")]
        public async Task<ActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                    return RedirectToAction(nameof(UsersController.Index));

                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
    }
}

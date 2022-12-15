using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurfsUpBlazor.Server.Models;
using System.Security.Claims;

namespace SurfsUpBlazor.Server.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
		[HttpGet]
        public ActionResult<string> GetRole()
        {
            return User.IsInRole("Lessor") ? "Lessor" : "User";
        }

        [HttpGet("toggle")]
        public async Task<ActionResult> ToggleRole()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return NotFound();
            }
            if (User.IsInRole("Lessor"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Lessor");
                await _userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, "User");
                await _userManager.AddToRoleAsync(user, "Lessor");
            }
            return Ok();
        }
    }
}

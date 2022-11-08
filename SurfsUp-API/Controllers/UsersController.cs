using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurfsUp_Models;

namespace SurfsUp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserManager<SurfsUpUser> _userManager;

        public UsersController(UserManager<SurfsUpUser> userManager)
        {
            _userManager = userManager;
        }

        [ProducesResponseType(typeof(IQueryable<SurfsUpUser>), 200)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        [HttpGet("Read", Name = "List Users")]
        public async Task<ActionResult> Get()
        {
            var users = _userManager.Users;
            foreach (var user in users)
                user.Roles = await _userManager.GetRolesAsync(user);
            return Ok(users.ToList());
        }

        [ProducesResponseType(typeof(OkObjectResult), 200)]
        [ProducesResponseType(typeof(IdentityResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        [HttpPut("Update", Name = "Toggle Admin")]
        public async Task<ActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NoContent();
            var userRoles = await _userManager.GetRolesAsync(user);
            IdentityResult result;
            if (userRoles.Contains("Administrator"))
                result = await _userManager.RemoveFromRoleAsync(user, "Admin");
            else
                result = await _userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
                return Ok(result);
            return new ObjectResult(result.Errors) { StatusCode = 400 };
        }

        [ProducesResponseType(typeof(OkObjectResult), 201)]
        [ProducesResponseType(typeof(IdentityResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        [HttpDelete("Delete", Name = "Delete User")]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return NoContent();
            return new ObjectResult(result.Errors) { StatusCode = 400 };
        }
    }
}

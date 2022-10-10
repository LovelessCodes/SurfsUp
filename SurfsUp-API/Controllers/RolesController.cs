using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfsUp.Models;

namespace SurfsUp_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {

        private readonly ILogger<RolesController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(ILogger<RolesController> logger, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
        }

        [ProducesResponseType(typeof(IdentityRole), 200)]
        [ProducesResponseType(typeof(IdentityResult), 400)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        [HttpPost("Create", Name = "Create Role")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Create(IdentityRole role)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return new ObjectResult(role) { StatusCode = 201 };
            return new ObjectResult(result.Errors) { StatusCode = 400 };
        }

        [ProducesResponseType(typeof(IQueryable<IdentityRole>), 200)]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [HttpGet("Read", Name = "List Roles")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Get()
        {
            var roles = from r in _roleManager.Roles select r;
            if (roles.Any())
                return Ok(roles);
            return NoContent();
        }

        [ProducesResponseType(typeof(NoContentResult), 201)]
        [ProducesResponseType(typeof(IQueryable<IdentityResult>), 400)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [HttpPut("Update", Name = "Update Role")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Update(IdentityRole role)
        {
            var r = _roleManager.Roles.FirstOrDefault(m => m.Id == role.Id);
            if (r == null)
                return NotFound();
            r = role;
            IdentityResult result = await _roleManager.UpdateAsync(r);
            if (result.Succeeded)
                return NoContent();
            return new ObjectResult(result.Errors) { StatusCode = 400 };
        }

        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(IdentityResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        [HttpDelete("Delete", Name = "Delete Role")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            IdentityRole? role = await _roleManager.Roles.FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
                return NotFound();
            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return NoContent();
            return new ObjectResult(result.Errors) { StatusCode = 400 };
        }
    }
}

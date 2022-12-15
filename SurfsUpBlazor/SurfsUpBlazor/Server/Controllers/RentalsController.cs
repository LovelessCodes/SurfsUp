using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurfsUpBlazor.Server.Data;
using SurfsUpBlazor.Server.Models;
using SurfsUpBlazor.Shared;
using System.Security.Claims;

namespace SurfsUpBlazor.Server.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class RentalsController : ControllerBase
	{
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public RentalsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Rental>>> GetRentals()
        {
            if (User.IsInRole("Admin"))
            {
                return _context.Rentals.ToList();
            }
            else
            {
                var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return user.Rentals;
            }
        }
    }
}

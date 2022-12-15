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
    public class SurfboardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public SurfboardsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult<List<Surfboard>> GetSurfboards()
        {
            return _context.Surfboards.ToList();
        }

        [HttpGet("user")]
        public async Task<ActionResult<List<Surfboard>>> GetMySurfboards()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return user.Surfboards;
        }

        [HttpGet("{id}")]
        public ActionResult<Surfboard> GetSurfboard(int id)
        {
            var surfboard = _context.Surfboards.Find(id);
            if (surfboard == null)
            {
                return NotFound();
            }
            return surfboard;
        }

        [HttpGet("{id}/rentals")]
        public ActionResult<List<Rental>> GetRentalsOnBoard(int id)
        {
            var surfboard = _context.Surfboards.Find(id);
            if (surfboard == null)
            {
                return NotFound();
            }
            return surfboard.Rentals;
        }

        [HttpGet("{id}/owner")]
        public ActionResult<ApplicationUser> GetOwner(int id)
        {
            var surfboard = _context.Surfboards.Find(id);
            if (surfboard == null)
            {
                return NotFound();
            }
            var owner = _userManager.Users.Where(u => u.Surfboards.Contains(surfboard)).FirstOrDefault();
            if (owner == null)
            {
                return NotFound();
            }
            return owner;
        }

        [HttpPost]
        public ActionResult<Surfboard> CreateSurfboard(Surfboard surfboard)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Lessor"))
            {
                surfboard.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Surfboards.Add(surfboard);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetSurfboard), new { id = surfboard.Id }, surfboard);
            }
            return Unauthorized();
        }

        [HttpPut]
        public async Task<ActionResult<Surfboard>> EditSurfboard(Surfboard surfboard)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (User.IsInRole("Admin") || user.Surfboards.Contains(surfboard))
            {
                _context.Surfboards.Update(surfboard);
                _context.SaveChanges();
                return surfboard;
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSurfboard([FromQuery] int id)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Surfboard? surfboard = user.Surfboards.Where(s => s.Id == id).FirstOrDefault();
            if (surfboard == null)
            {
                return Unauthorized();
            }
            _context.Surfboards.Remove(surfboard);
            _context.SaveChanges();
            return Ok();
        }
    }
}

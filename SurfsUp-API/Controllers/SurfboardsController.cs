using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfsUp.Areas.Identity.Data;
using SurfsUp.Data;
using SurfsUp.Models;
using static SurfsUp.Models.Surfboard;

namespace SurfsUp_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurfboardsController : ControllerBase
    {

        private readonly ILogger<SurfboardsController> _logger;
        private readonly SurfsUpContext _context;
        private readonly UserManager<SurfsUpUser> _userManager;

        public SurfboardsController(ILogger<SurfboardsController> logger, SurfsUpContext context, UserManager<SurfsUpUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet(Name = "")]
        public async Task<PaginatedList<Surfboard>> Get(string? sortOrder, string? currentFilter, string? searchString, int? pageNumber)
        {
            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            var bookings = from s in _context.Booking
                           where s.ReturnDate > DateTime.Now &&
                           s.BookingDate < DateTime.Now select s;

            var boards = from s in _context.Surfboard
                         select s;

            foreach (var booking in bookings)
                if (booking.SurfboardId != null)
                    boards = boards.Where(b => b.Id != booking.SurfboardId);

            if (!String.IsNullOrEmpty(searchString))
                boards = boards.Where(s => s.Title.Contains(searchString) || s.Type.Contains(searchString));

            switch (sortOrder)
            {
                case "Title_desc":
                    boards = boards.OrderByDescending(s => s.Title);
                    break;
                case "Price_desc":
                    boards = boards.OrderByDescending(s => s.Price);
                    break;
                case "Length_desc":
                    boards = boards.OrderByDescending(s => s.Length);
                    break;
                case "Width_desc":
                    boards = boards.OrderByDescending(s => s.Width);
                    break;
                case "Thickness_desc":
                    boards = boards.OrderByDescending(s => s.Thickness);
                    break;
                case "Volume_desc":
                    boards = boards.OrderByDescending(s => s.Volume);
                    break;
                case "Price":
                    boards = boards.OrderBy(s => s.Price);
                    break;
                case "Length":
                    boards = boards.OrderBy(s => s.Length);
                    break;
                case "Width":
                    boards = boards.OrderBy(s => s.Width);
                    break;
                case "Thickness":
                    boards = boards.OrderBy(s => s.Thickness);
                    break;
                case "Volume":
                    boards = boards.OrderBy(s => s.Volume);
                    break;
                default:
                    boards = boards.OrderBy(s => s.Title);
                    break;
            }
            int pageSize = 5;
            return await PaginatedList<Surfboard>.CreateAsync(boards.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        [ProducesResponseType(typeof(Surfboard), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        [HttpPost("Create", Name = "Create Surfboard")]
        public async Task<ActionResult> Create([FromBody] Surfboard surfboard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surfboard);
                await _context.SaveChangesAsync();
                return new ObjectResult(surfboard) { StatusCode = StatusCodes.Status201Created };
            }
            return BadRequest();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var surfboard = _context.Surfboard
                .Where(s => s.Id == id)
                .FirstOrDefault();

            if (surfboard != null)
            {
                _context.Surfboard.Remove(surfboard);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }




    }
}
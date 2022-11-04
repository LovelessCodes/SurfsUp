using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfsUp_API.Database;
using SurfsUp_Models;

namespace SurfsUp_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : Controller
    {

        private readonly SurfsUpContext _context;
        private readonly UserManager<SurfsUpUser> userManager;

        public BookingsController(SurfsUpContext context, UserManager<SurfsUpUser> userManager)
        {
            this.userManager = userManager;
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult> Get(Booking booking)
        {
            if (booking != null)
            {
                var book = from s in _context.Booking
                           where s.Id == booking.Id select s;
                return Ok(book.ToList());
            }
            var user = await userManager.GetUserAsync(User);
            if (user.Roles.Contains("Administrator"))
            {
                var bookings = from s in _context.Booking
                               select s;
                return Ok(bookings.ToList());
            }
            if (user != null)
            {
                var bookings = from s in _context.Booking
                               where s.UserId == user.Id select s;
                return Ok(await bookings.ToListAsync());
            }
            return NotFound();
        }
        [ProducesResponseType(typeof(Surfboard), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        [HttpPost("Create", Name = "Create Booking")]
        public async Task<ActionResult> Create([FromBody] Booking booking)
        {
            var user = await userManager.GetUserAsync(User);
            booking.UserId = user.Id;
            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();
            return BadRequest();
        }

        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [HttpDelete("Delete", Name = "Delete Booking")]
        public async Task<ActionResult> Delete(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [ProducesResponseType(typeof(OkObjectResult), 200)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Booking booking, int id)
        {


            var user = await userManager.GetUserAsync(User);
            booking.UserId = user.Id;
            if (id != booking.Id)
            {
                return NotFound();
            }
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();


        }
    }
}

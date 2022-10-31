using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurfsUp_API.Areas.Identity.Data;
using SurfsUp_API.Models;
using SurfsUp_API.Database;

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
        public async Task<List<Booking>> Get(Booking booking)
        {

            var user = await userManager.GetUserAsync(User);

            var bookings = from s in _context.Booking
                           select s;           

            if (!User.IsInRole("Admin"))
            {
                bookings = bookings.Where(b => b.UserId == user.Id);
            return await bookings.ToListAsync();
        }
        [ProducesResponseType(typeof(Surfboard), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        [HttpPost("Create", Name = "Create Booking")]
        public async Task<ActionResult> Create([FromBody] Booking booking)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }


            return View(booking);
        }

        // GET: Bookings/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null || _context.Surfboard == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfboard == null)
            {
                return NotFound();
            }
            ViewData["SurfboardId"] = surfboard.Id;
            ViewData["Name"] = surfboard.Title;

            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] int id, [Bind("BookingDate,ReturnDate")] Booking booking)
        {

            if (_context.Surfboard == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfboard == null)
            {
                return NotFound();
            }
            

            if (User == null)
            {
                return NotFound();
            }
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
                return NotFound();
            }

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
                return NotFound();
            }

            return View(booking);
        }

            var user = await userManager.GetUserAsync(User);
            booking.UserId = user.Id;
            if (id != booking.Id)
            {
                return NotFound();
            }
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();


        private bool BookingExists(int id)
        {
          return _context.Booking.Any(e => e.Id == id);
        }
    }
}

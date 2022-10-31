using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfsUp_API.Areas.Identity.Data;
using SurfsUp_API.Database;
using SurfsUp_API.Models;
using System.Data;
using static SurfsUp_API.Models.Surfboard;

namespace SurfsUp_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurfboardsController : ControllerBase
    {
        private readonly SurfsUpContext _context;

        public SurfboardsController(SurfsUpContext context)
        {
            _context = context;
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

        [ProducesResponseType(typeof(PaginatedList<Surfboard>), 200)]
        [HttpGet("Read", Name = "List Surfboards")]
        public SurfboardsList Get(string? sortOrder, string? currentFilter, string? searchString, int? pageNumber)
        {
            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            var bookings = from s in _context.Booking
                           where s.ReturnDate > DateTime.Now &&
                           s.BookingDate < DateTime.Now
                           select s;

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
            return new SurfboardsList() {
                Surfboards = boards.ToList(),
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize
            };
        }

        [ProducesResponseType(typeof(Surfboard), 200)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [HttpGet("Single", Name ="Get Single Surfboard")]
        public ActionResult GetSingle(int? id)
        {
            Surfboard surfboard = _context.Surfboard.FirstOrDefault(o => o.Id == id);
            if (surfboard == null)
                return NotFound();
            return Ok(surfboard);
        }

        [ProducesResponseType(typeof(Surfboard), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [HttpPut("Update", Name = "Update Surfboard")]
        public async Task<ActionResult> Update(int id, [FromBody] Surfboard surfboard)
        {
            Console.WriteLine(id);
            if (surfboard == null || surfboard.Id != id)
                return BadRequest();
            var surfboardToUpdate = _context.Surfboard
                .FirstOrDefault(s => s.Id == id);
            if (surfboardToUpdate == null)
                return NotFound();
            surfboardToUpdate.Title = surfboard.Title;
            surfboardToUpdate.Type = surfboard.Type;
            surfboardToUpdate.Price = surfboard.Price;
            surfboardToUpdate.Length = surfboard.Length;
            surfboardToUpdate.Width = surfboard.Width;
            surfboardToUpdate.Thickness = surfboard.Thickness;
            surfboardToUpdate.Volume = surfboard.Volume;

            _context.Surfboard.Update(surfboardToUpdate);
            await _context.SaveChangesAsync();
            return Ok(surfboardToUpdate);
        }
        
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [HttpDelete("Delete", Name = "Delete Surfboard")]
        public async Task<ActionResult> Delete(int id)
        {
            var surfboard = _context.Surfboard
                .Where(s => s.Id == id)
                .FirstOrDefault();
            if (surfboard == null)
                return NotFound();
            _context.Surfboard.Remove(surfboard);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
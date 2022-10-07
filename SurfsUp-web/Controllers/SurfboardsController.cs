using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurfsUp.Data;
using SurfsUp.Models;
using SurfsUp.Areas.Identity.Data;

namespace SurfsUp.Controllers
{
    public class SurfboardsController : Controller
    {
        private readonly UserManager<SurfsUpUser> userManager;
        private readonly SurfsUpContext _context;

        public SurfboardsController(SurfsUpContext context, UserManager<SurfsUpUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Surfboards
        public async Task<IActionResult> Index(string sortOrder,string currentFilter,string searchString,int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewData["LengthSortParm"] = sortOrder == "Length" ? "Length_desc" : "Length";
            ViewData["WidthSortParm"] = sortOrder == "Width" ? "Width_desc" : "Width";
            ViewData["ThicknessSortParm"] = sortOrder == "Thickness" ? "Thickness_desc" : "Thickness";
            ViewData["VolumeSortParm"] = sortOrder == "Volume" ? "Volume_desc" : "Volume";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var bookings = from s in _context.Booking
                           select s;

            bookings = bookings.Where(b => b.ReturnDate > DateTime.Now && b.BookingDate < DateTime.Now);
           


            var boards = from s in _context.Surfboard
                           select s;


            foreach (var booking in bookings)
            {
                
                if (booking.SurfboardId != null)
                {
                     
                     boards = boards.Where(b => b.Id != booking.SurfboardId);
                }

            }


        
            if (!String.IsNullOrEmpty(searchString))
            {
                boards = boards.Where(s => s.Title.Contains(searchString) || s.Type.Contains(searchString));
            }
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
            Console.WriteLine(ViewData["CurrentSort"]);
            return View(await PaginatedList<Surfboard>.CreateAsync(boards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        
        // GET: Surfboards/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(surfboard);
        }

        // GET: Surfboards/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Surfboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Image,Id,Title,Length,Width,Volume,Thickness,Type,Price,Equipment")] Surfboard surfboard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surfboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surfboard);
        }

        // GET: Surfboards/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Surfboard == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboard.FindAsync(id);
            if (surfboard == null)
            {
                return NotFound();
            }
            return View(surfboard);
        }

        // POST: Surfboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Image,Id,Title,Length,Width,Volume,Thickness,Type,Price,Equipment")] Surfboard surfboard)
        {
            if (id != surfboard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surfboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurfboardExists((int)surfboard.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(surfboard);
        }

        // GET: Surfboards/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
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

            return View(surfboard);
        }

        // POST: Surfboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Surfboard == null)
            {
                return Problem("Entity set 'SurfsUpContext.Surfboard'  is null.");
            }
            var surfboard = await _context.Surfboard.FindAsync(id);
            if (surfboard != null)
            {
                _context.Surfboard.Remove(surfboard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurfboardExists(int id)
        {
          return (_context.Surfboard?.Any(e => e.Id == id)).GetValueOrDefault();
        }

       
    }
}

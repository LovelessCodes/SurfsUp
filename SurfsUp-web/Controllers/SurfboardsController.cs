using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfsUp.Models;

namespace SurfsUp.Controllers
{
    public class SurfboardsController : Controller
    {
        private readonly SurfboardContext _context;

        public SurfboardsController(SurfboardContext context)
        {
            _context = context;
        }

        // GET: Surfboards
        public async Task<IActionResult> Index()
        {
              return _context.Surfboard != null ? 
                          View(await _context.Surfboard.ToListAsync()) :
                          Problem("Entity set 'SurfboardContext.Surfboard' is null.");
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Surfboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Length,Width,Thickness,Volume,Type,Price,Equipment")] Surfboard surfboard, [Bind("ImageData")] Image image)
        {
            if (ModelState.IsValid && image.ImageData != null)
            {
                if (image.ImageData.Length > 0){
                    using (var ms = new MemoryStream()) {
                        image.ImageData.CopyTo(ms);
                        surfboard.Image = ms.ToArray();
                    }
                }
                // string fileName = image.ImageData.FileName;
                // surfboard.Image = new byte[image.ImageData.Length];
                _context.Add(surfboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surfboard);
        }

        // GET: Surfboards/Edit/5
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Length,Width,Thickness,Volume,Type,Price,Equipment")] Surfboard surfboard, [Bind("ImageData")] Image image)
        {
            if (id != surfboard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (image.ImageData != null)
                    {
                        if (image.ImageData.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                image.ImageData.CopyTo(ms);
                                surfboard.Image = ms.ToArray();
                            }
                        }
                    }
                    _context.Update(surfboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurfboardExists(surfboard.Id))
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Surfboard == null)
            {
                return Problem("Entity set 'SurfboardContext.Surfboard'  is null.");
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
        
        // POST: Surfboards/Rent/5
        [HttpPost, ActionName("Rent")]
        public async Task<IActionResult> Rent(int id)
        {
            if (_context.Surfboard == null)
            {
                return Problem("Entity set 'SurfboardContext.Surfboard'  is null.");
            }
            var surfboard = await _context.Surfboard.FindAsync(id);
            var success = false;
            if (surfboard != null)
            {
                surfboard.RentedOut = !surfboard.RentedOut;
                _context.Surfboard.Update(surfboard);
                success = true;
            }

            await _context.SaveChangesAsync();
            return Json(new { updated = success });
        }

        // GET: Surfboards/Image/5
        [HttpGet, ActionName("Image")]
        public async Task<IActionResult> Image(int? id)
        {
            Console.WriteLine("Image id is " + id);
            if (id == null || _context.Surfboard == null)
            {
                return NotFound();
            }
            var surfboard = await _context.Surfboard.FindAsync(id);
            if (surfboard == null)
            {
                return NotFound();
            }
            if (surfboard.Image == null)
            {
                return NotFound();
            }
            // return String.Format("data:image/png;base64,{0}", Convert.ToBase64String(surfboard.Image));
            return File(surfboard.Image, "image/png");
        }
    }
}

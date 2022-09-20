using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfsUp.Data;
using SurfsUp.Models;
using System.Drawing.Printing;


namespace SurfsUp.Controllers
{
    public class BookingController : Controller
    {

        public async Task<IActionResult> Booking(int? id)
        {
            return View();
        }

        public BookingController(SurfsUpContext context)
        {
            _context = context;
        }

        private readonly SurfsUpContext _context;

        public async Task<IActionResult> Index(int id)
        {
            var boards = from s in _context.Surfboard
                         select s;
            ViewData["SelectedSurfBoard"]=id;
            return View(await PaginatedList<Surfboard>.CreateAsync(boards.AsNoTracking(), 1, 100));

        }



        public void GetBooking(Booking booking)
        {
            var boards = from s in _context.Booking where s.SurfBoard == booking.SurfBoard select s;
            if (boards.Any(x => DateTime.Compare(x.DeliveryDate, booking.RentalDate)>0))
            {
                return;
            }

            _context.Booking.Add(booking);
            _context.SaveChanges(); 
        }
    }
}

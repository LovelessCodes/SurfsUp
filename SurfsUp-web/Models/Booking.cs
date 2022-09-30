using SurfsUp.Areas.Identity.Data;

namespace SurfsUp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int? SurfboardId { get; set; }
        public string? UserId { get; set; }
    }
}

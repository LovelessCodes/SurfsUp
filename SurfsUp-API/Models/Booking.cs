using SurfsUp_API.Areas.Identity.Data;

namespace SurfsUp_API.Models
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

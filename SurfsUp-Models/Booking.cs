namespace SurfsUp_Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int? SurfboardId { get; set; }
        public string? Email { get; set; }
    }
}

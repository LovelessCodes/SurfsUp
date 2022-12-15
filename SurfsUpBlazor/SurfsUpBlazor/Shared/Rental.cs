using System.ComponentModel.DataAnnotations;

namespace SurfsUpBlazor.Shared
{
	public class Rental
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string ApplicationUserId { get; set; } = String.Empty;
        public int SurfboardId { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfsUp.Models
{
    public class Surfboard
    {
        public byte[]? Image { get; set; }
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Title { get; set; }

        [Required]
        public float Length { get; set; }

        [Required]
        public float Width { get; set; }

        [Required]
        [Range(1, 1000)]
        public float Volume { get; set; }

        [Required]
        [Range(1, 30)]
        public float Thickness { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [StringLength(30)]
        public string? Type { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        
        [StringLength(60, MinimumLength = 3)]
        public string? Equipment { get; set; }

        [Required]
        public bool RentedOut { get; set; }
    }
}

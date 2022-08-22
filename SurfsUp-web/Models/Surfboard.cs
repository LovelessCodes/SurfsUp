
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfsUp.Models
{
    public class Surfboard
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Volume { get; set; }
        public float Thickness { get; set; }
        public string? Type { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        
        public string? Equipment { get; set; }
        public bool RentedOut { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfsUpBlazor.Shared
{
    public class Surfboard : ICloneable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Must be more than 3 characters, and less than 60 characters")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Name must be capitalized")]
        public string Name { get; set; } = String.Empty;
        
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        
        [Required]
        [DataType(DataType.ImageUrl)]
        [RegularExpression(@"^https?:\/\/.*\.(?:png|jpg|gif)$", ErrorMessage = "Must be a valid (secure) image URL")]
        public string ImageUrl { get; set; } = String.Empty;
        
        [Required]
        public string Description { get; set; } = String.Empty;
        
        [Required]
        [Range(1, 1000, ErrorMessage = "Must be between 1 and 1000")]
        public float Length { get; set; }
        
        [Required]
        [Range(1, 1000, ErrorMessage = "Must be between 1 and 1000")]
        public float Width { get; set; }
        
        [Required]
        [Range(1, 1000, ErrorMessage = "Must be between 1 and 1000")]
        public float Thickness { get; set; }
        
        [Required]
        [Range(1, 1000, ErrorMessage = "Must be between 1 and 1000")]
        public float Volume { get; set; }
        
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Type must be capitalized")]
        public string Type { get; set; } = String.Empty;
        
        [Required]
        public string Place { get; set; } = String.Empty;

        public List<Rental> Rentals { get; set; } = new();
        public string ApplicationUserId { get; set; } = String.Empty;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Data;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace SurfsUp.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Email { get; set; }

       // [Range (1, int.MaxValue)]
        [Required]
        public string Phone{ get; set; }


        [Required]
        public int SurfBoard { get; set; }

        
        [Required]
        public DateTime RentalDate { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }        


    }
}

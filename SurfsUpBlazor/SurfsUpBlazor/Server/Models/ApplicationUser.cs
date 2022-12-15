using Microsoft.AspNetCore.Identity;
using SurfsUpBlazor.Shared;

namespace SurfsUpBlazor.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Surfboard> Surfboards { get; set; } = new();
        public List<Rental> Rentals { get; set; } = new();
        public List<Message> Messages { get; set; } = new();
    }
}
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfsUp_Models;

// Add profile data for application users by adding properties to the SurfsUpUser class
public class SurfsUpUser : IdentityUser
{
    [NotMapped]
    public IList<string> Roles { get; set; }
}


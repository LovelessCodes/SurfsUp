using System.ComponentModel.DataAnnotations;

namespace SurfsUp.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}

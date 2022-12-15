using System.ComponentModel.DataAnnotations;

namespace SurfsUpBlazor.Shared
{
    public class Supboard : Surfboard
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Must be more than 2 characters, and less than 100 characters")]
        public string Equipment { get; set; } = String.Empty;
    }
}

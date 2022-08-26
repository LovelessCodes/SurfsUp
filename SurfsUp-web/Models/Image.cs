
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfsUp.Models
{
    public class Image
    {
        public int Id { get; set; }

        public byte[]? ImageData { get; set; }
    }
}

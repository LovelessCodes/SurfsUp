using System.ComponentModel.DataAnnotations;

namespace SurfsUpBlazor.Shared
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; } = String.Empty;
        public string User { get; set; } = String.Empty;
        public DateTime Time { get; set; } = DateTime.Now;
        public string ApplicationUserId { get; set; } = String.Empty;
    }
}

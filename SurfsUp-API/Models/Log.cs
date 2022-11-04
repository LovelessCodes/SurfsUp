using SurfsUp_API.Database;

namespace SurfsUp_Models
{
    public class Log
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}

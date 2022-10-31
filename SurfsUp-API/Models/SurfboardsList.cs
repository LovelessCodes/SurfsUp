namespace SurfsUp_API.Models
{
    public class SurfboardsList
    {
        public List<Surfboard> Surfboards { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

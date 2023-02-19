namespace BiletSatis.Models
{
    public class Movie : ModelBase
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Poster { get; set; }
        public int MovieDbId { get; set; }
    }
}
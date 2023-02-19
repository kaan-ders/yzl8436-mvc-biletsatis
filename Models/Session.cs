using System.ComponentModel.DataAnnotations;

namespace BiletSatis.Models
{
    public class Session : ModelBase
    {
        [Required(ErrorMessage = "Tarih alanı zorunludur")]
        public DateTime DateTime { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [Required]
        public int SaloonId { get; set; }
        public Saloon Saloon { get; set; }
    }
}

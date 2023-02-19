using System.ComponentModel.DataAnnotations;

namespace BiletSatis.Models
{
    public class Ticket : ModelBase
    {
        [Required]
        public int Amount { get; set; }
        public decimal Price { get; set; }

        [Required]
        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
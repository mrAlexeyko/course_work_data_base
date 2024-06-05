using System.ComponentModel.DataAnnotations.Schema;

namespace CurseWork.Models
{
    [Table("luggage")]
    public class Luggage
    {
        [Column("luggageid")]
        public int LuggageId { get; set; }

        [Column("ticketid")]
        public int TicketId { get; set; }

        [Column("weight")]
        public double Weight { get; set; }

        [Column("volume")]
        public double Volume { get; set; }

        [Column("extrafee")]
        public decimal ExtraFee { get; set; }

        public Ticket Ticket { get; set; } = null!;
    }
}

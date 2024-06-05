using System.ComponentModel.DataAnnotations.Schema;

namespace CurseWork.Models
{
    [Table("ticket")]
    public class Ticket
    {
        [Column("ticketid")]
        public int TicketId { get; set; }

        [Column("flightid")]
        public int FlightId { get; set; }

        [Column("passengerid")]
        public int PassengerId { get; set; }

        [Column("seatnumber")]
        public string SeatNumber { get; set; } = string.Empty;

        [Column("price")]
        public decimal Price { get; set; }

        public Flight Flight { get; set; } = null!;
        public Passenger Passenger { get; set; } = null!;
    }
}

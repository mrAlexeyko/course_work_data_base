using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurseWork.Models
{
    [Table("registration")]
    public class Registration
    {
        [Column("registrationid")]
        public int RegistrationId { get; set; }

        [Column("ticketid")]
        public int TicketId { get; set; }

        [Column("status")]
        public string Status { get; set; } = string.Empty;

        [Column("time")]
        public DateTime Time { get; set; }

        public Ticket Ticket { get; set; } = null!;
    }
}

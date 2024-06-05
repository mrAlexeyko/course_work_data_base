using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurseWork.Models
{
    [Table("route")]
    public class Route
    {
        [Column("routeid")]
        public int RouteId { get; set; }

        [Column("departuretime")]
        public TimeSpan DepartureTime { get; set; }

        [Column("arrivaltime")]
        public TimeSpan ArrivalTime { get; set; }

        [Column("departureairport")]
        public string DepartureAirport { get; set; } = string.Empty;

        [Column("arrivalairport")]
        public string ArrivalAirport { get; set; } = string.Empty;

        [Column("terminaltype")]
        public char TerminalType { get; set; }
    }
}

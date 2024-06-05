using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurseWork.Models
{
    [Table("flight")]
    public class Flight
    {
        [Column("flightid")]
        public int FlightId { get; set; }

        [Column("routeid")]
        public int RouteId { get; set; }

        [Column("aircraftid")]
        public int AircraftId { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("passengercount")]
        public int PassengerCount { get; set; }

        public Route Route { get; set; } = null!;
        public Aircraft Aircraft { get; set; } = null!;
    }
}

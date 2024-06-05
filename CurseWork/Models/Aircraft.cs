using System.ComponentModel.DataAnnotations.Schema;

namespace CurseWork.Models
{
    [Table("aircraft")]
    public class Aircraft
    {
        [Column("aircraftid")]
        public int AircraftId { get; set; }

        [Column("passengercapacity")]
        public int PassengerCapacity { get; set; }

        [Column("model")]
        public string Model { get; set; } = string.Empty;

        [Column("airline")]
        public string Airline { get; set; } = string.Empty;
    }
}

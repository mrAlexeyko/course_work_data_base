using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurseWork.Models
{
    [Table("visa")]
    public class Visa
    {
        [Column("visaid")]
        public int VisaId { get; set; }

        [Column("passengerid")]
        public int PassengerId { get; set; }

        [Column("country")]
        public string Country { get; set; } = string.Empty;

        [Column("expirydate")]
        public DateTime ExpiryDate { get; set; }

        public Passenger Passenger { get; set; } = null!;
    }
}

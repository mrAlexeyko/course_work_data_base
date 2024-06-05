using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurseWork.Models
{
    [Table("passenger")]
    public class Passenger
    {
        [Column("passengerid")]
        public int PassengerId { get; set; }

        [Column("firstname")]
        public string FirstName { get; set; } = string.Empty;

        [Column("lastname")]
        public string LastName { get; set; } = string.Empty;

        [Column("passportnumber")]
        public string PassportNumber { get; set; } = string.Empty;

        [Column("birthdate")]
        public DateTime BirthDate { get; set; }

        [Column("insearch")]
        public bool InSearch { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using CurseWork.Models;

namespace CurseWork.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Luggage> Luggages { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<CurseWork.Models.Route> Routes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Visa> Visas { get; set; }
    }
}

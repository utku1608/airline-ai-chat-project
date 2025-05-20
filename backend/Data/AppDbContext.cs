using Microsoft.EntityFrameworkCore;
using AirlineApi.Models;

namespace AirlineApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary keys
            modelBuilder.Entity<Flight>().HasKey(f => f.Id);
            modelBuilder.Entity<Passenger>().HasKey(p => new { p.FlightId, p.Name });

            // Optional: Initial data seed (istersen)
            base.OnModelCreating(modelBuilder);
        }
    }
}

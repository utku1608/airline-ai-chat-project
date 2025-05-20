namespace AirlineApi.Models
{
    public class Flight
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? AirportFrom { get; set; }
        public string? AirportTo { get; set; }
        public int Duration { get; set; } // in minutes
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
    }
}

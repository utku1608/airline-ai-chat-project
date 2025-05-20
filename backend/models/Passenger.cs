namespace AirlineApi.Models
{
    public class Passenger
    {
        public string Name { get; set; } = "";
        public string SeatNumber { get; set; } = "";
        public Guid FlightId { get; set; }
        public DateTime Date { get; set; }
    }
}

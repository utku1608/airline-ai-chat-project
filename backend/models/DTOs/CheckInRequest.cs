namespace AirlineApi.Models.DTOs
{
    public class CheckInRequest
    {
        public Guid FlightId { get; set; }
        public DateTime Date { get; set; }
        public string PassengerName { get; set; } = "";
    }
}

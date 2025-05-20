namespace AirlineApi.Models.DTOs
{
    public class AddFlightRequest
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string AirportFrom { get; set; }
        public string AirportTo { get; set; }
        public int Duration { get; set; }
        public int Capacity { get; set; }
    }
}

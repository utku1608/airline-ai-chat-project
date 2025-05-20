namespace AirlineApi.Models.DTOs
{
    public class QueryFlightRequest
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? AirportFrom { get; set; }
        public string? AirportTo { get; set; }
        public int NumberOfPeople { get; set; }
        public string? TripType { get; set; } // "oneway" veya "roundtrip"
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}

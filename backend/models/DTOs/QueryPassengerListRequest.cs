namespace AirlineApi.Models.DTOs
{
    public class QueryPassengerListRequest
    {
        public Guid FlightId { get; set; }
        public DateTime Date { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}

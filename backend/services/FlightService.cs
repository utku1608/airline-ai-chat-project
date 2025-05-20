using AirlineApi.Models;
using AirlineApi.Models.DTOs;
using AirlineApi.Data;
using Microsoft.EntityFrameworkCore;

namespace AirlineApi.Services
{
    public class FlightService
    {
        private readonly AppDbContext _context;

        public FlightService(AppDbContext context)
        {
            _context = context;
        }

        public Flight AddFlight(AddFlightRequest request)
        {
            var flight = new Flight
            {
                Id = Guid.NewGuid(),
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                AirportFrom = request.AirportFrom,
                AirportTo = request.AirportTo,
                Duration = request.Duration,
                Capacity = request.Capacity,
                AvailableSeats = request.Capacity
            };

            _context.Flights.Add(flight);
            _context.SaveChanges();

            return flight;
        }

  public List<Flight> QueryFlights(QueryFlightRequest request)
{
    return _context.Flights
        .AsEnumerable() // EF Core LINQ içinde .Date desteklemez → AsEnumerable() ile memory’ye alıyoruz
        .Where(f =>
            f.DateFrom.Date == request.DateFrom.Date &&
            f.DateTo.Date == request.DateTo.Date &&
            string.Equals(f.AirportFrom, request.AirportFrom, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(f.AirportTo, request.AirportTo, StringComparison.OrdinalIgnoreCase) &&
            f.AvailableSeats >= request.NumberOfPeople
        )
        .Skip((request.Page - 1) * request.PageSize)
        .Take(request.PageSize)
        .ToList();
}


        public (bool success, string message, string? ticketNumber) BuyTicket(BuyTicketRequest request)
        {
            var flight = _context.Flights.FirstOrDefault(f =>
                f.Id == request.FlightId &&
                f.DateFrom.Date == request.Date.Date);

            if (flight == null)
                return (false, "Flight not found", null);

            if (flight.AvailableSeats <= 0)
                return (false, "sold out", null);

            flight.AvailableSeats--;
            _context.SaveChanges();

            var ticketNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            return (true, "ticket purchased", ticketNumber);
        }

        public (bool success, string message, string? seatNumber) CheckIn(CheckInRequest request)
        {
            var existing = _context.Passengers.FirstOrDefault(p =>
                p.FlightId == request.FlightId &&
                p.Date.Date == request.Date.Date &&
                p.Name.ToLower() == request.PassengerName.ToLower());

            if (existing != null)
                return (true, "already checked in", existing.SeatNumber);

            var flight = _context.Flights.FirstOrDefault(f =>
                f.Id == request.FlightId &&
                f.DateFrom.Date == request.Date.Date);

            if (flight == null)
                return (false, "Flight not found", null);

            var passengerCount = _context.Passengers
                .Count(p => p.FlightId == request.FlightId && p.Date.Date == request.Date.Date);

            var seatNumber = "P" + (passengerCount + 1);

            var passenger = new Passenger
            {
                FlightId = request.FlightId,
                Date = request.Date,
                Name = request.PassengerName,
                SeatNumber = seatNumber
            };

            _context.Passengers.Add(passenger);
            _context.SaveChanges();

            return (true, "checked in", seatNumber);
        }
        public List<Passenger> GetPassengers(QueryPassengerListRequest request)
        {
            return _context.Passengers
                .Where(p =>
                    p.FlightId == request.FlightId &&
                    p.Date.Date == request.Date.Date)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
        }

        public List<Flight> GetAllFlights()
        {
            return _context.Flights.ToList();
        }
    }
}

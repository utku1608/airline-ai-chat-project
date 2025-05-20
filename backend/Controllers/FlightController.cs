using Microsoft.AspNetCore.Mvc;
using AirlineApi.Models.DTOs;
using AirlineApi.Services;
using Microsoft.AspNetCore.Authorization;


namespace AirlineApi.Controllers
{
    [ApiController]
    [Route("api/v1/flight")]
    public class FlightController : ControllerBase
    {
        private readonly FlightService _flightService;

        public FlightController(FlightService flightService)
        {
            _flightService = flightService;
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddFlight([FromBody] AddFlightRequest request)
        {
            var flight = _flightService.AddFlight(request);
            return Ok(new { message = "Flight added successfully", flight.Id });
        }
        [HttpPost("query")]
public IActionResult QueryFlights([FromBody] QueryFlightRequest request)
{
    var flights = _flightService.QueryFlights(request);
    return Ok(flights);
}
[Authorize]
[HttpPost("buy")]
public IActionResult BuyTicket([FromBody] BuyTicketRequest request)
{
    var result = _flightService.BuyTicket(request);

    if (!result.success)
        return BadRequest(new { message = result.message });

    return Ok(new
    {
        message = result.message,
        ticketNumber = result.ticketNumber
    });
}
[HttpPost("checkin")]
public IActionResult CheckIn([FromBody] CheckInRequest request)
{
    var result = _flightService.CheckIn(request);

    if (!result.success)
        return BadRequest(new { message = result.message });

    return Ok(new
    {
        message = result.message,
        seatNumber = result.seatNumber
    });
}
[Authorize]
[HttpPost("passengerlist")]
public IActionResult GetPassengerList([FromBody] QueryPassengerListRequest request)
{
    var passengers = _flightService.GetPassengers(request);
    return Ok(passengers);
}

}
    
}


using LotApi.Data;
using Microsoft.AspNetCore.Mvc;
using LotApi.Mappers;
using LotApi.Dto;


namespace LotApi.Controllers
{

    [ApiController]
    [Route("api/flights")]
    public class FlightController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public FlightController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var flights = _dataContext.Flight.ToList()
            .Select(f => f.ToFlightDto());
            return Ok(flights);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var flight = _dataContext.Flight.Find(id);

            if(flight == null)
            {
                return NotFound();
            }
            return Ok(flight.ToFlightDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateFlightRequestDto flightDto)
        {
            var flightModel = flightDto.ToFlightFromCreateDto();
            _dataContext.Flight.Add(flightModel);
            _dataContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = flightModel.Id }, flightModel.ToFlightDto());
        }

    }
}

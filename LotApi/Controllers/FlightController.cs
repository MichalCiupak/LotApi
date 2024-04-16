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
            var flights = _dataContext.Flight.ToList();
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
            return Ok(flight);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateFlightRequestDto flightDto)
        {
            var flightModel = flightDto.ToFlightFromCreateDto();
            _dataContext.Flight.Add(flightModel);
            _dataContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = flightModel.Id }, flightModel);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateFlightRequestDto updateDto)
        {
            var flightModel = _dataContext.Flight.FirstOrDefault(x => x.Id == id);

            if (flightModel == null)
            {
                return NotFound();  
            }

            flightModel.FlightNumber = updateDto.FlightNumber;
            flightModel.DepartureDate = updateDto.DepartureDate;    
            flightModel.DepartureLocation = updateDto.DepartureLocation;
            flightModel.ArrivalLocation = updateDto.ArrivalLocation;
            flightModel.AircraftType = updateDto.AircraftType;

            _dataContext.SaveChanges();

            return Ok(flightModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var flightModel = _dataContext.Flight.FirstOrDefault(x => x.Id == id);

            if (flightModel == null)
            {
                return NotFound();
            }

            _dataContext.Flight.Remove(flightModel);

            _dataContext.SaveChanges();

            return NoContent();
        }


    }
}

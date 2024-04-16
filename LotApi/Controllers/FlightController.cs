using LotApi.Data;
using Microsoft.AspNetCore.Mvc;

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

        /*private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }*/
    }
}

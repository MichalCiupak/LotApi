using LotApi.Models;

namespace LotApi.Dto
{
    public class CreateFlightRequestDto
    {
        public string FlightNumber { get; set; } = null!;
        public DateTime DepartureDate { get; set; }
        public string DepartureLocation { get; set; } = null!;
        public string ArrivalLocation { get; set; } = null!;
        public string AircraftType { get; set; } = null!;
    }
}

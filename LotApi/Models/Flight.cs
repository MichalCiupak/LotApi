namespace LotApi.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; } = null!;
        public DateTime DepartureDate { get; set; }
        public FlightLocation DepartureLocation { get; set; } = null!;
        public FlightLocation ArrivalLocation { get; set; } = null!;
        public string AircraftType { get; set; } = null!;    
    }
}

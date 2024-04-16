namespace LotApi.Models
{
    public class FlightLocation
    {
        public int Id { get; set; }
        public string AirportName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}

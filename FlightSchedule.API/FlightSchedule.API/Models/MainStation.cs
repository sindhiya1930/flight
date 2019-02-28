using System.Diagnostics.CodeAnalysis;

namespace FlightSchedule.API.Models
{
    [ExcludeFromCodeCoverage]
    public partial class MainStation
    {
        public int StationId { get; set; }
        public string StationShortCode { get; set; }
        public string StationLongCode { get; set; }
        public string Airline { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }
}

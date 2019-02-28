using System;
using System.Diagnostics.CodeAnalysis;

namespace FlightSchedule.API.Models
{
    [ExcludeFromCodeCoverage]
    public partial class Linops
    {
        public int LinopsId { get; set; }
        public DateTime FlightDate { get; set; }
        public string Airline { get; set; }
        public string StationShortCode { get; set; }
        public string FlightNumber { get; set; }
        public string AircraftReg { get; set; }
        public string WorkControlNum { get; set; }
        public string MaintenanceStatus { get; set; }
        public DateTime MaintDate { get; set; }
    }
}

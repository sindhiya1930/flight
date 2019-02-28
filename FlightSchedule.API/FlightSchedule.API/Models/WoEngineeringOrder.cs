using System.Diagnostics.CodeAnalysis;

namespace FlightSchedule.API.Models
{
    [ExcludeFromCodeCoverage]
    public partial class WoEngineeringOrder
    {
        public string AircraftReg { get; set; }
        public int Wonumber { get; set; }
        public string Status { get; set; }
        public string Eonumber { get; set; }
    }
}

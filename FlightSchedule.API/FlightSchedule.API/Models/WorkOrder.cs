using System;
using System.Diagnostics.CodeAnalysis;

namespace FlightSchedule.API.Models
{
    [ExcludeFromCodeCoverage]
    public partial class WorkOrder
    {
        public string AircraftReg { get; set; }
        public string Station { get; set; }
        public DateTime ScheduleStartDate { get; set; }
        public int Wonumber { get; set; }
        public string Wodescription { get; set; }
        public string Wostatus { get; set; }
        public int AircraftType { get; set; }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;

namespace FlightSchedule.API.Models
{
    [ExcludeFromCodeCoverage]
    public partial class FltSeg
    {
        public int FltSegId { get; set; }
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime Setd { get; set; }
        public DateTime Seta { get; set; }
        public string OpsType { get; set; }
        public string Airline { get; set; }
        public DateTime? LocalDate { get; set; }
        public string Aircraftreg { get; set; }
    }
}

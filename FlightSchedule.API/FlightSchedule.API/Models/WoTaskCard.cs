using System.Diagnostics.CodeAnalysis;

namespace FlightSchedule.API.Models
{
    [ExcludeFromCodeCoverage]
    public partial class WoTaskCard
    {
        public string EngineeringOrder { get; set; }
        public string TaskCard { get; set; }
        public string TaskCardDescription { get; set; }
        public string Status { get; set; }
        public int Wonumber { get; set; }
    }
}

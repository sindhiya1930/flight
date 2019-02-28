using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace FlightSchedule.API.Models
{
    [ExcludeFromCodeCoverage]
    public class FlightScheduleQueryParams
    {
        [DataMember]
        [XmlElement]
        public string AircraftRegistration { get; set; }

        [DataMember]
        [XmlElement]//
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Flight Date Format ('yyyy-MM-dd'). ")]
        [RegularExpression(@"^(\d{4}-[0-1]\d{1}-[0-3]\d{1})$", ErrorMessage = "Invalid Flight Date Format ('yyyy-MM-dd'). ")]
        public string FlightDate { get; set; }

        [DataMember]
        [XmlElement]
        public string Station { get; set; }
    }
}

using FlightSchedule.API.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace FlightSchedule.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FlightScheduleController : ControllerBase
    {
        IDBAccess<Models.FltSeg> db = null;

        public FlightScheduleController(IDBAccess<Models.FltSeg> dBAccess)
        {
            db = dBAccess;
        }
        // GET api/values
        [HttpGet("AircraftRegistration/{aircraftRegistration}/Station/{station}/Date/{date}")]
        public async Task<ActionResult> GetFlightSchedule(string aircraftRegistration, string station, string date)
        {
            DateTime scheduleDate;
            if (string.IsNullOrWhiteSpace(aircraftRegistration))
            {
                return BadRequest("Aircraft Registration should not be blank");
            }
            if (string.IsNullOrWhiteSpace(station))
            {
                return BadRequest("Station should not be blank");
            }
            if (string.IsNullOrWhiteSpace(date))
            {
                return BadRequest("Date should not be blank");
            }
            if (!DateTime.TryParseExact(date,
                       "yyyy-MM-dd",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out scheduleDate))
            {
                return BadRequest("Date format is not correct");
            }

            var data = await db.Get(aircraftRegistration, scheduleDate, station);
            return Ok(data);
        }

        [HttpGet("AircraftRegistration/{aircraftRegistration}/Date/{date}")]
        public async Task<ActionResult> GetFlightScheduleByACDate(string aircraftRegistration, string date)
        {
            DateTime scheduleDate;
            if (string.IsNullOrEmpty(aircraftRegistration))
            {
                return BadRequest("Aircraft Registration should not be blank");
            }
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest("Date should not be blank");
            }
            if (!DateTime.TryParseExact(date,
                       "yyyy-MM-dd",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out scheduleDate))
            {
                return BadRequest("Date format is not correct");
            }

            var data = await db.Get(aircraftRegistration, scheduleDate);
            return Ok(data);
        }
    }
}

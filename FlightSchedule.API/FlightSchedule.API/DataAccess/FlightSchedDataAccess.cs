using FlightSchedule.API.DB;
using FlightSchedule.API.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSchedule.API.DataAccess
{
    [ExcludeFromCodeCoverage]
    public class FlightSchedDataAccess:IDBAccess<Models.FltSeg>
    {
        public async Task<IEnumerable<Models.FltSeg>> Get(string aircraftReg, DateTime flightScheduleDate, string destination)
        {
            return await Task.Run(() =>
            {
                List<Models.FltSeg> flightSchedules = new List<Models.FltSeg>();
                using (var db = new AlaskaPoCDBContext())
                {
                    flightSchedules = db.FltSeg.Where(x => x.Destination== destination && x.LocalDate == flightScheduleDate.Date && x.Aircraftreg==aircraftReg).ToList();
                }
                return flightSchedules;
            });
        }

        public async Task<IEnumerable<FltSeg>> Get(string aircraftReg, DateTime flightScheduleDate)
        {
            return await Task.Run(() =>
            {
                List<Models.FltSeg> flightSchedules = new List<Models.FltSeg>();
                using (var db = new AlaskaPoCDBContext())
                {
                    flightSchedules = db.FltSeg.Where(x => x.LocalDate == flightScheduleDate.Date && x.Aircraftreg == aircraftReg).ToList();
                }
                return flightSchedules;
            });
        }
    }
}

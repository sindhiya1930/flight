using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightSchedule.API.DataAccess
{
    public interface IDBAccess<T>
    {

        Task<IEnumerable<T>> Get(string aircraftReg,DateTime flightScheduleDate,string destination);
        Task<IEnumerable<T>> Get(string aircraftReg, DateTime flightScheduleDate);

    }
}

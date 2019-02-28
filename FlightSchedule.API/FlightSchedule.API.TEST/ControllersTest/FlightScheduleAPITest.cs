using FlightSchedule.API.DataAccess;
using FlightSchedule.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FlightSchedule.API.TEST
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FlightScheduleAPITest
    {
        Controllers.FlightScheduleController objFlightScheduleController = null;

        [TestMethod]
        [DataRow("N281VA", "SEA", "2018-11-19")]
        public void GetFlightSchedule_Positive(string aircraftRegistration, string station, string date)
        {
            try
            {
                List<FltSeg> retVal = new List<FltSeg>() { new FltSeg() { Aircraftreg = "", Airline = "", Destination = "" } };
                var mockManager = new Mock<IDBAccess<FltSeg>>();
                mockManager.Setup(x => x.Get(aircraftRegistration, Convert.ToDateTime(date), station)).ReturnsAsync(retVal);
                objFlightScheduleController = new Controllers.FlightScheduleController(mockManager.Object);
                var result = objFlightScheduleController.GetFlightSchedule(aircraftRegistration, station, date).Result;
                var objResult = (result as OkObjectResult).Value as List<FltSeg>;
                Assert.IsNotNull(objResult);
                Assert.AreEqual(objResult.Count, 1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [DataRow("", "SEA", "2018-11-19")]
        public void GetFlightSchedule_Negative_EmptyAircraftRegistration(string aircraftRegistration, string station, string date)
        {
            try
            {
                List<FltSeg> retVal = new List<FltSeg>();
                var mockManager = new Mock<IDBAccess<FltSeg>>();
                mockManager.Setup(x => x.Get(aircraftRegistration, Convert.ToDateTime(date), station)).ReturnsAsync(retVal);
                objFlightScheduleController = new Controllers.FlightScheduleController(mockManager.Object);
                var result = objFlightScheduleController.GetFlightSchedule(aircraftRegistration, station, date).Result;
                var objResult = (result as BadRequestObjectResult).Value;
                Assert.AreEqual("Aircraft Registration should not be blank", objResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [DataRow("N281VA", "", "2018-11-19")]
        public void GetFlightSchedule_Negative_EmptyStation(string aircraftRegistration, string station, string date)
        {
            try
            {
                List<FltSeg> retVal = new List<FltSeg>();
                var mockManager = new Mock<IDBAccess<FltSeg>>();
                mockManager.Setup(x => x.Get(aircraftRegistration, Convert.ToDateTime(date), station)).ReturnsAsync(retVal);
                objFlightScheduleController = new Controllers.FlightScheduleController(mockManager.Object);
                var result = objFlightScheduleController.GetFlightSchedule(aircraftRegistration, station, date).Result;
                var objResult = (result as BadRequestObjectResult).Value;
                Assert.AreEqual("Station should not be blank", objResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [DataRow("N281VA", "SEA", "")]
        public void GetFlightSchedule_Negative_EmptyDate(string aircraftRegistration, string station, string date)
        {
            try
            {
                List<FltSeg> retVal = new List<FltSeg>();
                var mockManager = new Mock<IDBAccess<FltSeg>>();
                mockManager.Setup(x => x.Get(aircraftRegistration, Convert.ToDateTime("2018-11-19"), station)).ReturnsAsync(retVal);
                objFlightScheduleController = new Controllers.FlightScheduleController(mockManager.Object);
                var result = objFlightScheduleController.GetFlightSchedule(aircraftRegistration, station, date).Result;
                var objResult = (result as BadRequestObjectResult).Value;
                Assert.AreEqual("Date should not be blank", objResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [DataRow("N281VA", "2018-11-19")]
        public void GetFlightScheduleByACDate_Positive(string aircraftRegistration, string date)
        {
            try
            {
                List<FltSeg> retVal = new List<FltSeg>() { new FltSeg() { Aircraftreg = "", Airline = "", Destination = "" } };
                var mockManager = new Mock<IDBAccess<FltSeg>>();
                mockManager.Setup(x => x.Get(aircraftRegistration, Convert.ToDateTime(date))).ReturnsAsync(retVal);
                objFlightScheduleController = new Controllers.FlightScheduleController(mockManager.Object);
                var result = objFlightScheduleController.GetFlightScheduleByACDate(aircraftRegistration, date).Result;
                var objResult = (result as OkObjectResult).Value as List<FltSeg>;
                Assert.IsNotNull(objResult);
                Assert.AreEqual(objResult.Count, 1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        [DataRow("", "2018-11-19")]
        [DataRow("N285VA", "")]
        [DataRow("", "")]
        [DataRow("N285VA", "19-11-2018")]
        public void GetFlightScheduleByACDate_Negative(string aircraftRegistration, string date)
        {
            try
            {
                List<FltSeg> retVal = new List<FltSeg>();
                var mockManager = new Mock<IDBAccess<FltSeg>>();
                mockManager.Setup(x => x.Get(aircraftRegistration, Convert.ToDateTime("2018-11-19"))).ReturnsAsync(retVal);
                objFlightScheduleController = new Controllers.FlightScheduleController(mockManager.Object);
                var result = objFlightScheduleController.GetFlightScheduleByACDate(aircraftRegistration, date).Result;
                var objResult = (result as BadRequestObjectResult).Value;
                if (string.IsNullOrWhiteSpace(aircraftRegistration) && string.IsNullOrWhiteSpace(date))
                {
                    Assert.AreEqual("Aircraft Registration should not be blank", objResult);
                }
                else if (string.IsNullOrWhiteSpace(aircraftRegistration))
                {
                    Assert.AreEqual("Aircraft Registration should not be blank", objResult);
                }
                else if (string.IsNullOrWhiteSpace(date))
                {
                    Assert.AreEqual("Date should not be blank", objResult);
                }
                else
                {
                    Assert.AreEqual("Date format is not correct", objResult);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

    }
}

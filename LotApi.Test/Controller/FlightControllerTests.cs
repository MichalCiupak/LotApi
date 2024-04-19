using System;
using LotApi.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using LotApi.Models;
using LotApi.Controllers;
using FluentAssertions;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using LotApi.Dto;


namespace LotApi.Test.Controller
{
    public class FlightControllerTests 
    {

        [Fact]
        public void FlightController_GetAll_ReturnOk()
        {
            var flightsData = new List<Flight>
             {
                 new Flight { Id = 1, FlightNumber = "ABC123", DepartureDate = new DateTime(2024, 4, 17), DepartureLocation = "New York", ArrivalLocation = "Los Angeles", AircraftType = "Boeing 737" },
                 new Flight { Id = 2, FlightNumber = "XYZ789", DepartureDate = new DateTime(2024, 4, 18), DepartureLocation = "London", ArrivalLocation = "Paris", AircraftType = "Airbus A320" }
             }.AsQueryable();

            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            var mockDataContext = new DataContext(dbContextOptions);

            mockDataContext.Flight = MockDbSet(flightsData);

            var controller = new FlightController(mockDataContext);

            var result = controller.GetAll() as OkObjectResult;
            Assert.NotNull(result);

            var flights = result.Value as IEnumerable<Flight>;
            Assert.NotNull(flights);
            Assert.Equal(200, result.StatusCode);

            var firstFlight = flights.First();
            Assert.NotNull(firstFlight);
            Assert.Equal(1, firstFlight.Id);
            Assert.Equal("ABC123", firstFlight.FlightNumber);
            Assert.Equal(new DateTime(2024, 4, 17), firstFlight.DepartureDate);
            Assert.Equal("New York", firstFlight.DepartureLocation);
            Assert.Equal("Los Angeles", firstFlight.ArrivalLocation);
            Assert.Equal("Boeing 737", firstFlight.AircraftType);
        }

        [Fact]
        public void FlightController_GetById_ReturnOk()
        {
            var flightsData = new List<Flight>
            {
                new Flight { Id = 1, FlightNumber = "ABC123", DepartureDate = new DateTime(2024, 4, 17), DepartureLocation = "New York", ArrivalLocation = "Los Angeles", AircraftType = "Boeing 737" },
                new Flight { Id = 2, FlightNumber = "XYZ789", DepartureDate = new DateTime(2024, 4, 18), DepartureLocation = "London", ArrivalLocation = "Paris", AircraftType = "Airbus A320" }
            }.AsQueryable();

            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            var mockDataContext = new DataContext(dbContextOptions);

            mockDataContext.Flight = MockDbSet(flightsData);

            var controller = new FlightController(mockDataContext);

            var result = controller.GetById(1) as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var flight = result.Value as Flight;
            Assert.NotNull(flight);
            Assert.Equal(1, flight.Id);
            Assert.Equal("ABC123", flight.FlightNumber);
            Assert.Equal(new DateTime(2024, 4, 17), flight.DepartureDate);
            Assert.Equal("New York", flight.DepartureLocation);
            Assert.Equal("Los Angeles", flight.ArrivalLocation);
            Assert.Equal("Boeing 737", flight.AircraftType);
        }
        [Fact]
        public void FlightController_create_ReturnOk()
        {

            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            var mockDataContext = new DataContext(dbContextOptions);
            var flightDto = new CreateFlightRequestDto
            {
                FlightNumber = "ABC123",
                DepartureDate = new DateTime(2024, 4, 17),
                DepartureLocation = "New York",
                ArrivalLocation = "Los Angeles",
                AircraftType = "Boeing 737"
            };

            var controller = new FlightController(mockDataContext);

            var result = controller.Create(flightDto) as CreatedAtActionResult;
            Assert.NotNull(result);

            var flight = result.Value as Flight;
            Assert.NotNull(flight);
            Assert.Equal("ABC123", flight.FlightNumber);
            Assert.Equal(new DateTime(2024, 4, 17), flight.DepartureDate);
            Assert.Equal("New York", flight.DepartureLocation);
            Assert.Equal("Los Angeles", flight.ArrivalLocation);
            Assert.Equal("Boeing 737", flight.AircraftType);
        }

        [Fact]
        public void FlightController_delete_ReturnOk()
        {
            var flightsData = new List<Flight>
            {
                new Flight { Id = 1, FlightNumber = "ABC123", DepartureDate = new DateTime(2024, 4, 17), DepartureLocation = "New York", ArrivalLocation = "Los Angeles", AircraftType = "Boeing 737" },
                new Flight { Id = 2, FlightNumber = "XYZ789", DepartureDate = new DateTime(2024, 4, 18), DepartureLocation = "London", ArrivalLocation = "Paris", AircraftType = "Airbus A320" }
            }.AsQueryable();

            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            var mockDataContext = new DataContext(dbContextOptions);

            mockDataContext.Flight = MockDbSet(flightsData);

            var controller = new FlightController(mockDataContext);

            var result = controller.Delete(1) as NoContentResult;

            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void FlightController_update_ReturnOk()
        {
            var flightsData = new List<Flight>
            {
                new Flight { Id = 1, FlightNumber = "ABC123", DepartureDate = new DateTime(2024, 4, 17), DepartureLocation = "New York", ArrivalLocation = "Los Angeles", AircraftType = "Boeing 737" },
                new Flight { Id = 2, FlightNumber = "XYZ789", DepartureDate = new DateTime(2024, 4, 18), DepartureLocation = "London", ArrivalLocation = "Paris", AircraftType = "Airbus A320" }
            }.AsQueryable();

            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            var mockDataContext = new DataContext(dbContextOptions);

            mockDataContext.Flight = MockDbSet(flightsData);

            var controller = new FlightController(mockDataContext);

            var updateDto = new UpdateFlightRequestDto
            {
                FlightNumber = "DEF456",
                DepartureDate = new DateTime(2024, 4, 19),
                DepartureLocation = "Berlin",
                ArrivalLocation = "Madrid",
                AircraftType = "Boeing 747"
            };

            var result = controller.Update(1, updateDto) as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var updatedFlight = result.Value as Flight;
            Assert.NotNull(updatedFlight);
            Assert.Equal(1, updatedFlight.Id);
            Assert.Equal("DEF456", updatedFlight.FlightNumber);
            Assert.Equal(new DateTime(2024, 4, 19), updatedFlight.DepartureDate);
            Assert.Equal("Berlin", updatedFlight.DepartureLocation);
            Assert.Equal("Madrid", updatedFlight.ArrivalLocation);
            Assert.Equal("Boeing 747", updatedFlight.AircraftType);



        }

        private static DbSet<Flight> MockDbSet(IQueryable<Flight> data)
        {
            var mockSet = new Mock<DbSet<Flight>>();
            mockSet.As<IQueryable<Flight>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }
    }
}

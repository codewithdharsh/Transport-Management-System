using NUnit.Framework;
using TransportManagementSystem.dao;
using TransportManagementSystem.entity;
using TransportManagementSystem.myexceptions;
using System;

namespace TransportManagementSystem.Tests
{
    [TestFixture]
    public class TransportServiceTests
    {
        private ITransportManagementService _service;

        [SetUp]
        public void Setup()
        {
            _service = new TransportManagementServiceImpl();
        }


        [Test]
        public void AddVehicle_ShouldAddSuccessfully()
        {
            var vehicle = new Vehicle(0, "Unit Test Van", 7.5m, "Van", "Available");
            bool result = _service.AddVehicle(vehicle);
            Assert.IsTrue(result);
        }

        [Test]
        public void BookTrip_ShouldCreateBooking()
        {
            bool result = _service.BookTrip(1, 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.IsTrue(result);
        }

        

        [Test]
        public void GetBooking_InvalidBooking_ShouldThrowException()
        {
            Assert.Throws<BookingNotFoundException>(() => _service.GetBookingsByPassenger(-100));
        }
    }
}

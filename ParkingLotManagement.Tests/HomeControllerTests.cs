using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using ParkingLotManagement.Controllers;
using ParkingLotManagement.Domain;
using ParkingLotManagement.Infrastructure;
using ParkingLotManagement.Models;
using System.Collections.Generic;
using Xunit;

namespace ParkingLotManagement.Tests
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller;
        private readonly Mock<ILogger<HomeController>> _loggerMock;
        private readonly Mock<IParkingLotRepository> _parkingLotRepositoryMock;
        private readonly Mock<IParkingLotSpaceRepository> _parkingLotSpaceRepositoryMock;
        private readonly Mock<IParkingLotTransactionRepository> _parkingLotTransactionRepositoryMock;

        public HomeControllerTests()
        {
            _loggerMock = new Mock<ILogger<HomeController>>();
            _parkingLotRepositoryMock = new Mock<IParkingLotRepository>();
            _parkingLotSpaceRepositoryMock = new Mock<IParkingLotSpaceRepository>();
            _parkingLotTransactionRepositoryMock = new Mock<IParkingLotTransactionRepository>();

            // Mock IConfiguration
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetValue<decimal>("HourlyFee")).Returns(10m);

            _controller = new HomeController(
                _loggerMock.Object,
                _parkingLotRepositoryMock.Object,
                _parkingLotSpaceRepositoryMock.Object,
                _parkingLotTransactionRepositoryMock.Object );
        }

        [Fact]
        public void Index_ShouldReturnView()
        {
            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CheckinParkingLot_WithNullTagNumber_ShouldReturnError()
        {
            // Act
            var result = _controller.CheckinParkingLot(null) as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, _controller.Response.StatusCode);
            Assert.False((bool)result.Value.GetType().GetProperty("success").GetValue(result.Value));
        }
        [Fact]
        public void CheckOutParkingLot_WithNullTagNumber_ShouldReturnError()
        {
            // Act
            var result = _controller.CheckOutParkingLot(null) as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, _controller.Response.StatusCode);
            Assert.False((bool)result.Value.GetType().GetProperty("success").GetValue(result.Value));
        }

        [Fact]
        public void CheckOutParkingLot_WithValidTagNumber_ShouldReturnSuccess()
        {
            // Arrange
            string tagNumber = "ABC123";
            var parkingLotTransaction = new ParkingLotTransaction { Id = 1, TagNumber = tagNumber };
            _parkingLotTransactionRepositoryMock.Setup(r => r.GetParkingLotTransactionByTagNumber(tagNumber)).Returns(parkingLotTransaction);

            // Act
            var result = _controller.CheckOutParkingLot(tagNumber) as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.Value.GetType().GetProperty("status").GetValue(result.Value));
            Assert.True((bool)result.Value.GetType().GetProperty("success").GetValue(result.Value));
        }


        [Fact]
        public void GetParkingLotTransactionOccupied_ShouldReturnOccupiedTransactions()
        {
            // Arrange
            var transactions = new List<ParkingLotTransaction> { new ParkingLotTransaction { Id = 1, TagNumber = "ABC123"} };
            _parkingLotTransactionRepositoryMock.Setup(r => r.GetParkingLotTransactionOccupied()).Returns(transactions);

            // Act
            var result = _controller.GetParkingLotTransactionOccupied() as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(transactions, result.Value);
        }

        [Fact]
        public void GetHeaderData_ShouldReturnHeaderData()
        {
            // Arrange
            var parkingLotSpaces = new List<ParkingLotSpace> { new ParkingLotSpace { Id = 1, ParkingLotId = 1 } };
            _parkingLotSpaceRepositoryMock.Setup(r => r.GetAllParkingLotSpaces()).Returns(parkingLotSpaces);

            // Act
            var result = _controller.GetHeaderData() as JsonResult;

            // Assert
            Assert.NotNull(result); 
        }

        [Fact]
        public void GetStats_ShouldReturnStatsData()
        {
            // Arrange
            var parkingLotSpaces = new List<ParkingLotSpace> { new ParkingLotSpace { Id = 1, ParkingLotId = 1 } };
            _parkingLotSpaceRepositoryMock.Setup(r => r.GetAllParkingLotSpaces()).Returns(parkingLotSpaces);

            // Act
            var result = _controller.GetStats() as JsonResult;

            // Assert
            Assert.NotNull(result); 
        }

    }
}
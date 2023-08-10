using Moq;
using Xunit;
using Dapper;
using ParkingLotManagement.Domain;
using ParkingLotManagement.Infrastructure;
using System.Data;

namespace ParkingLotManagement.Tests
{
    public class ParkingLotRepositoryTests
    {
        private readonly ParkingLotRepository _repository;
        private readonly Mock<IDbConnection> _connectionMock;
        private readonly Mock<IDbCommand> _commandMock;

        public ParkingLotRepositoryTests()
        {
            _connectionMock = new Mock<IDbConnection>();
            _commandMock = new Mock<IDbCommand>();
            _connectionMock.Setup(c => c.CreateCommand()).Returns(_commandMock.Object);
            _repository = new ParkingLotRepository(_connectionMock.ToString());
        }

        [Fact]
        public void GetAllParkingLots_ShouldReturnParkingLots()
        {
            // Arrange
            var parkingLots = new List<ParkingLot> { new ParkingLot { Id = 1 } };
            _connectionMock.Setup(c => c.Query<ParkingLot>(It.IsAny<string>(), null, null, true, null, null)).Returns(parkingLots);

            // Act
            var result = _repository.GetAllParkingLots();

            // Assert
            Assert.Equal(parkingLots, result);
        }

        [Fact]
        public void GetParkingLotById_ShouldReturnParkingLot()
        {
            // Arrange
            var parkingLot = new ParkingLot {Id=1 };
            _connectionMock.Setup(c => c.Query<ParkingLot>(It.IsAny<string>(), It.IsAny<object>(), null, true, null, null)).Returns(new[] { parkingLot });

            // Act
            var result = _repository.GetParkingLotById(1);

            // Assert
            Assert.Equal(parkingLot, result);
        }

     }
}
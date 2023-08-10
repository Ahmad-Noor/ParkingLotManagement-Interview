using Moq;
using Xunit;
using Dapper;
using ParkingLotManagement.Domain;
using ParkingLotManagement.Infrastructure;
using System.Data;

namespace ParkingLotManagement.Tests
{
    public class ParkingLotSpaceRepositoryTests
    {
        private readonly ParkingLotSpaceRepository _repository;
        private readonly Mock<IDbConnection> _connectionMock;

        public ParkingLotSpaceRepositoryTests()
        {
            _connectionMock = new Mock<IDbConnection>();
            _repository = new ParkingLotSpaceRepository(_connectionMock.ToString());
        }

        [Fact]
        public void GetAllParkingLotSpaces_ShouldReturnAllSpaces()
        {
            // Arrange
            var spaces = new List<ParkingLotSpace> { new ParkingLotSpace { /* properties */ } };
            _connectionMock.Setup(c => c.Query<ParkingLotSpace>(It.IsAny<string>(), null, null, true, null, null)).Returns(spaces);

            // Act
            var result = _repository.GetAllParkingLotSpaces();

            // Assert
            Assert.Equal(spaces, result);
        }

        [Fact]
        public void GetParkingLotSpacesAvailable_ShouldReturnAvailableSpace()
        {
            // Arrange
            var space = new ParkingLotSpace { /* properties */ };
            _connectionMock.Setup(c => c.Query<ParkingLotSpace>(It.IsAny<string>(), It.IsAny<object>(), null, true, null, null)).Returns(new[] { space });

            // Act
            var result = _repository.GetParkingLotSpacesAvailable(1);

            // Assert
            Assert.Equal(space, result);
        }

        [Fact]
        public void GetParkingLotSpaceById_ShouldReturnSpaceById()
        {
            // Arrange
            var space = new ParkingLotSpace { /* properties */ };
            _connectionMock.Setup(c => c.Query<ParkingLotSpace>(It.IsAny<string>(), It.IsAny<object>(), null, true, null, null)).Returns(new[] { space });

            // Act
            var result = _repository.GetParkingLotSpaceById(1);

            // Assert
            Assert.Equal(space, result);
        }

        [Fact]
        public void InsertParkingLotSpace_ShouldInsertSpace()
        {
            // Arrange
            var space = new ParkingLotSpace { /* properties */ };
            _connectionMock.Setup(c => c.Execute(It.IsAny<string>(), space, null, null, null)).Returns(1);

            // Act
            _repository.InsertParkingLotSpace(space);

            // Assert
            _connectionMock.Verify(c => c.Execute(It.IsAny<string>(), space, null, null, null), Times.Once);
        }

        [Fact]
        public void UpdateParkingLotSpace_ShouldUpdateSpace()
        {
            // Arrange
            var space = new ParkingLotSpace { /* properties */ };
            _connectionMock.Setup(c => c.Execute(It.IsAny<string>(), space, null, null, null)).Returns(1);

            // Act
            _repository.UpdateParkingLotSpace(space);

            // Assert
            _connectionMock.Verify(c => c.Execute(It.IsAny<string>(), space, null, null, null), Times.Once);
        }

        [Fact]
        public void DeleteParkingLotSpace_ShouldDeleteSpaceById()
        {
            // Arrange
            int id = 1;
            _connectionMock.Setup(c => c.Execute(It.IsAny<string>(), new { Id = id }, null, null, null)).Returns(1);

            // Act
            _repository.DeleteParkingLotSpace(id);

            // Assert
            _connectionMock.Verify(c => c.Execute(It.IsAny<string>(), new { Id = id }, null, null, null), Times.Once);
        }
    }
}
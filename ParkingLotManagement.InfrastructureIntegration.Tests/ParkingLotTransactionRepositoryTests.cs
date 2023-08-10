using Xunit;
using Dapper;
using ParkingLotManagement.Domain;
using ParkingLotManagement.Infrastructure;
using Moq;
using System.Data;

namespace ParkingLotTransactionRepositoryTests.Tests
{
    public class ParkingLotTransactionRepositoryTests
    {
        private readonly ParkingLotTransactionRepository _repository;
        private readonly Mock<IDbConnection> _connectionMock;

        public ParkingLotTransactionRepositoryTests()
        {
            _connectionMock = new Mock<IDbConnection>();
            _repository = new ParkingLotTransactionRepository(_connectionMock.ToString());
        }

        [Fact]
        public void GetAllParkingLotTransactions_ShouldReturnAllTransactions()
        {
            // Arrange
            var transactions = new List<ParkingLotTransaction> { new ParkingLotTransaction { Id=1, TagNumber = "ABC123" } };
            _connectionMock.Setup(c => c.Query<ParkingLotTransaction>(It.IsAny<string>(),null, null, true,null, null)).Returns(transactions);

            // Act
            var result = _repository.GetAllParkingLotTransactions();

            // Assert
            Assert.Equal(transactions, result);
        }

        [Fact]
        public void GetParkingLotTransactionById_ShouldReturnTransactionById()
        {
            // Arrange
            int id = 1;
            var transaction = new ParkingLotTransaction { Id=1};
            _connectionMock.Setup(c => c.Query<ParkingLotTransaction>(It.IsAny<string>(), new { Id = id }, null, true, null, null)).Returns(new[] { transaction });

            // Act
            var result = _repository.GetParkingLotTransactionById(id);

            // Assert
            Assert.Equal(transaction, result);
        }

        [Fact]
        public void GetParkingLotTransactionByTagNumber_ShouldReturnTransactionByTag()
        {
            // Arrange
            string tagNumber = "ABC123";
            var transaction = new ParkingLotTransaction { Id=1};
            _connectionMock.Setup(c => c.Query<ParkingLotTransaction>(It.IsAny<string>(), new { TagNumber = tagNumber }, null, true, null, null)).Returns(new[] { transaction });

            // Act
            var result = _repository.GetParkingLotTransactionByTagNumber(tagNumber);

            // Assert
            Assert.Equal(transaction, result);
        }

        [Fact]
        public void GetAverageCarsForPast30Days_ShouldReturnAverageCars()
        {
            // Arrange
            decimal avgCars = 10.5m;
            _connectionMock.Setup(c => c.Query<decimal>(It.IsAny<string>(), It.IsAny<object>(), null, true, null, null)).Returns(new[] { avgCars });

            // Act
            var result = _repository.GetAverageCarsForPast30Days();

            // Assert
            Assert.Equal(avgCars, result);
        }

        [Fact]
        public void GetAverageRevenueForPast30Days_ShouldReturnAverageRevenue()
        {
            // Arrange
            decimal avgRevenue = 1000.5m;
            _connectionMock.Setup(c => c.Query<decimal>(It.IsAny<string>(), It.IsAny<object>(), null, true, null, null)).Returns(new[] { avgRevenue });

            // Act
            var result = _repository.GetAverageRevenueForPast30Days();

            // Assert
            Assert.Equal(avgRevenue, result);
        }

        [Fact]
        public void GetTodayRevenue_ShouldReturnTodayRevenue()
        {
            // Arrange
            decimal revenue = 500.5m;
            _connectionMock.Setup(c => c.Query<decimal>(It.IsAny<string>(), It.IsAny<object>(), null, true, null, null)).Returns(new[] { revenue });

            // Act
            var result = _repository.GetTodayRevenue();

            // Assert
            Assert.Equal(revenue, result);
        }

        [Fact]
        public void GetParkingLotTransactionOccupied_ShouldReturnOccupiedTransactions()
        {
            // Arrange
            var transactions = new List<ParkingLotTransaction> { new ParkingLotTransaction { Id=1} };
            _connectionMock.Setup(c => c.Query<ParkingLotTransaction>(It.IsAny<string>(), null, null, true, null, null)).Returns(transactions);

            // Act
            var result = _repository.GetParkingLotTransactionOccupied();

            // Assert
            Assert.Equal(transactions, result);
        }

        [Fact]
        public void InsertParkingLotTransaction_ShouldInsertTransaction()
        {
            // Arrange
            var transaction = new ParkingLotTransaction { Id=1};
            _connectionMock.Setup(c => c.Execute(It.IsAny<string>(), transaction, null, null, null)).Returns(1);

            // Act
            _repository.InsertParkingLotTransaction(transaction);

            // Assert
            _connectionMock.Verify(c => c.Execute(It.IsAny<string>(), transaction, null, null, null), Times.Once);
        }

        [Fact]
        public void UpdateParkingLotTransaction_ShouldUpdateTransaction()
        {
            // Arrange
            var transaction = new ParkingLotTransaction { Id=1};
            _connectionMock.Setup(c => c.Execute(It.IsAny<string>(), transaction, null, null, null)).Returns(1);

            // Act
            _repository.UpdateParkingLotTransaction(transaction);

            // Assert
            _connectionMock.Verify(c => c.Execute(It.IsAny<string>(), transaction, null, null, null), Times.Once);
        }

        [Fact]
        public void DeleteParkingLotTransaction_ShouldDeleteTransactionById()
        {
            // Arrange
            int id = 1;
            _connectionMock.Setup(c => c.Execute(It.IsAny<string>(), new { Id = id }, null, null, null)).Returns(1);

            // Act
            _repository.DeleteParkingLotTransaction(id);

            // Assert
            _connectionMock.Verify(c => c.Execute(It.IsAny<string>(), new { Id = id }, null, null, null), Times.Once);
        }
    }
}
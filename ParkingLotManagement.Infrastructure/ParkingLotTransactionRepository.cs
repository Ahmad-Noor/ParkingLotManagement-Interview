using System.Data;
using System.Data.SqlClient;
using Dapper;
using ParkingLotManagement.Domain;

namespace ParkingLotManagement.Infrastructure
{
    public class ParkingLotTransactionRepository : IParkingLotTransactionRepository
    {
        private readonly string connectionString;

        public ParkingLotTransactionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Connection => new SqlConnection(connectionString);

        public IEnumerable<ParkingLotTransaction> GetAllParkingLotTransactions()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ParkingLotTransaction>("SELECT * FROM ParkingLotTransactions");
            }
        }

        public ParkingLotTransaction GetParkingLotTransactionById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ParkingLotTransaction>("SELECT * FROM ParkingLotTransactions WHERE Id = @Id", new { Id = id }).FirstOrDefault();
            }
        }
        public ParkingLotTransaction GetParkingLotTransactionByTagNumber(string tagNumber)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ParkingLotTransaction>("SELECT * FROM ParkingLotTransactions WHERE CheckoutTime is null and TagNumber = @TagNumber", new { TagNumber = tagNumber }).FirstOrDefault();
            }
        }
        public decimal GetAverageCarsForPast30Days()
        {
            DateTime thirtyDaysAgo = DateTime.Now.AddDays(-30);
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<decimal>("SELECT ROUND(Count(*)/30.0,2)  as avgCars FROM ParkingLotTransactions WHERE CheckOuttime >= @ThirtyDaysAgo", new { ThirtyDaysAgo = thirtyDaysAgo }).FirstOrDefault();
            }
        }
        public decimal GetAverageRevenueForPast30Days()
        {
            DateTime thirtyDaysAgo = DateTime.Now.AddDays(-30);
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<decimal>("SELECT ROUND(Sum(AmountPaid)/30.0,2) as avgRevenue FROM ParkingLotTransactions WHERE Checkouttime >= @ThirtyDaysAgo", new { ThirtyDaysAgo = thirtyDaysAgo }).FirstOrDefault();
            }
        }        
        public decimal GetTodayRevenue()
        {
            DateTime date = DateTime.Now;
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<decimal>("SELECT Sum(AmountPaid) as Revenue FROM ParkingLotTransactions WHERE day(Checkouttime) = @PDay and month(Checkouttime) = @PMonth and year(Checkouttime) = @PYear", new { PDay = date.Day, PMonth = date.Month, PYear = date.Year }).FirstOrDefault();
            }
        }
        public IEnumerable<ParkingLotTransaction> GetParkingLotTransactionOccupied()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ParkingLotTransaction>("SELECT * FROM ParkingLotTransactions WHERE CheckoutTime is null");
            }
        }

        public void InsertParkingLotTransaction(ParkingLotTransaction parkingLotTransaction)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO ParkingLotTransactions (CheckinTime, CheckoutTime, TagNumber, ParkingLotSpaceId, AmountPaid) VALUES(@CheckinTime, @CheckoutTime, @TagNumber, @ParkingLotSpaceId, @AmountPaid)", parkingLotTransaction);
            }
        }

        public void UpdateParkingLotTransaction(ParkingLotTransaction parkingLotTransaction)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE ParkingLotTransactions SET CheckinTime = @CheckinTime, CheckoutTime = @CheckoutTime, TagNumber = @TagNumber, ParkingLotSpaceId = @ParkingLotSpaceId, AmountPaid=@AmountPaid  WHERE Id = @Id", parkingLotTransaction);
            }
        }

        public void DeleteParkingLotTransaction(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM ParkingLotTransactions WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
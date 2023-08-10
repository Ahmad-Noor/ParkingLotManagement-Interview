using System.Data;
using System.Data.SqlClient;
using Dapper;
using ParkingLotManagement.Domain;

namespace ParkingLotManagement.Infrastructure
{
    public class ParkingLotRepository: IParkingLotRepository
    {
        private readonly string connectionString;

        public ParkingLotRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Connection => new SqlConnection(connectionString);

        public IEnumerable<ParkingLot> GetAllParkingLots()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ParkingLot>("SELECT * FROM ParkingLots");
            }
        }

        public ParkingLot GetParkingLotById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ParkingLot>("SELECT * FROM ParkingLots WHERE Id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void InsertParkingLot(ParkingLot parkingLot)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO ParkingLots (Name, Address, TotalSpaces) VALUES(@Name, @Address, @TotalSpaces)", parkingLot);
            }
        }

        public void UpdateParkingLot(ParkingLot parkingLot)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE ParkingLots SET Name = @Name, Address = @Address, TotalSpaces = @TotalSpaces WHERE Id = @Id", parkingLot);
            }
        }

        public void DeleteParkingLot(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM ParkingLots WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
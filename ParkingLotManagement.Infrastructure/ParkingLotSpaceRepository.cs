using System.Data;
using System.Data.SqlClient;
using Dapper;
using ParkingLotManagement.Domain;

namespace ParkingLotManagement.Infrastructure
{
    public class ParkingLotSpaceRepository: IParkingLotSpaceRepository
    {
        private readonly string connectionString;

        public ParkingLotSpaceRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Connection => new SqlConnection(connectionString);

        public IEnumerable<ParkingLotSpace> GetAllParkingLotSpaces()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ParkingLotSpace>("SELECT * FROM ParkingLotSpaces");
            }
        }
        public ParkingLotSpace GetParkingLotSpacesAvailable(int parkingLotId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ParkingLotSpace>("SELECT * FROM ParkingLotSpaces WHERE Occupied = 0 and ParkingLotId = @ParkingLotId", new { ParkingLotId = parkingLotId }).FirstOrDefault();
            }
        }

        public ParkingLotSpace GetParkingLotSpaceById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ParkingLotSpace>("SELECT * FROM ParkingLotSpaces WHERE Id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void InsertParkingLotSpace(ParkingLotSpace parkingLotSpace)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO ParkingLotSpaces (ParkingLotId, SpaceNumber, Occupied) VALUES(@ParkingLotId, @SpaceNumber, @Occupied)", parkingLotSpace);
            }
        }

        public void UpdateParkingLotSpace(ParkingLotSpace parkingLotSpace)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE ParkingLotSpaces SET ParkingLotId = @ParkingLotId, SpaceNumber = @SpaceNumber, Occupied = @Occupied WHERE Id = @Id", parkingLotSpace);
            }
        }

        public void DeleteParkingLotSpace(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM ParkingLotSpaces WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
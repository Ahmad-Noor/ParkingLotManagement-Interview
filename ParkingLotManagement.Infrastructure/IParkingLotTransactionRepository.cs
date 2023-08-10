using ParkingLotManagement.Domain;

namespace ParkingLotManagement.Infrastructure
{
    public interface IParkingLotTransactionRepository
    {
        public IEnumerable<ParkingLotTransaction> GetAllParkingLotTransactions();

        public ParkingLotTransaction GetParkingLotTransactionById(int id);
        public ParkingLotTransaction GetParkingLotTransactionByTagNumber(string tagNumber);
        public decimal GetAverageCarsForPast30Days();
        public decimal GetAverageRevenueForPast30Days();
        public decimal GetTodayRevenue();
        public IEnumerable<ParkingLotTransaction> GetParkingLotTransactionOccupied();

        public void InsertParkingLotTransaction(ParkingLotTransaction parkingLotTransaction);

        public void UpdateParkingLotTransaction(ParkingLotTransaction parkingLotTransaction);

        public void DeleteParkingLotTransaction(int id);
    }
}
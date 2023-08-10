using ParkingLotManagement.Domain;

namespace ParkingLotManagement.Infrastructure
{
    public interface IParkingLotRepository
    {
        public IEnumerable<ParkingLot> GetAllParkingLots();

        public ParkingLot GetParkingLotById(int id);

        public void InsertParkingLot(ParkingLot parkingLot);

        public void UpdateParkingLot(ParkingLot parkingLot);
        public void DeleteParkingLot(int id);
    }
}
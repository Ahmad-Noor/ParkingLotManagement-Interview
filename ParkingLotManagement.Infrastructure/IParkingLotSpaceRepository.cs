using ParkingLotManagement.Domain;

namespace ParkingLotManagement.Infrastructure
{
    public interface IParkingLotSpaceRepository
    {
        public IEnumerable<ParkingLotSpace> GetAllParkingLotSpaces();
        public ParkingLotSpace GetParkingLotSpacesAvailable(int parkingLotId);
        public ParkingLotSpace GetParkingLotSpaceById(int id);
        public void InsertParkingLotSpace(ParkingLotSpace parkingLotSpace);
        public void UpdateParkingLotSpace(ParkingLotSpace parkingLotSpace);
        public void DeleteParkingLotSpace(int id);
    }
}
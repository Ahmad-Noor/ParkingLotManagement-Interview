namespace ParkingLotManagement.Domain
{
    public class ParkingLotSpace
    {
        public int Id { get; set; } 
        public int ParkingLotId { get; set; } 
        public string SpaceNumber { get; set; } 
        public bool Occupied { get; set; } 
    }
}

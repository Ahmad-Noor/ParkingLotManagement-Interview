namespace ParkingLotManagement.Domain
{
    public class ParkingLotTransaction
    {
        public int Id { get; set; } 
        public DateTime CheckinTime { get; set; }  
        public DateTime? CheckoutTime { get; set; }  
        public string TagNumber { get; set; } 
        public int ParkingLotSpaceId { get; set; }
        public decimal AmountPaid { get; set; }  
    }
}

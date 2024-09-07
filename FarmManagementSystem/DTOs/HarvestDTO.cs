namespace FarmManagementSystem.DTOs
{
    public class HarvestDTO
    {
        public int HarvestID { get; set; }
        public int CropID { get; set; }
        public int WorkerID { get; set; }
        public DateTime HarvestDate { get; set; }
        public decimal Quantity { get; set; }

        // Additional properties could be included in the future
    }
}

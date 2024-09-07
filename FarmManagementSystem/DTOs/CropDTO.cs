namespace FarmManagementSystem.DTOs
{
    public class CropDTO
    {
        public int CropID { get; set; }
        public string Name { get; set; }
        public DateTime PlantingDate { get; set; }
        public DateTime? HarvestDate { get; set; }
        public int FieldID { get; set; }
        public string FieldName { get; set; }
        // Includes FieldName
        // Hides Field and Harvests
    }
}

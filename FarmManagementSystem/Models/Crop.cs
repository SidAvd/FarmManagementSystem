using System.ComponentModel.DataAnnotations;

namespace FarmManagementSystem.Models
{
    public class Crop
    {
        [Key]
        public int CropID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime PlantingDate { get; set; }
        public DateTime? HarvestDate { get; set; }
        [Required]
        public int FieldID { get; set; }

        // Navigation properties help establish relationships
        // between entities and enable Entity Framework Core
        // to understand the associations between tables.
        public Field Field { get; set; }
        public ICollection<Harvest> Harvests { get; set; }
    }
}

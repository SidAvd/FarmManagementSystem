using System.ComponentModel.DataAnnotations;

namespace FarmManagementSystem.Models
{
    public class Harvest
    {
        [Key]
        public int HarvestID { get; set; }
        [Required]
        public int CropID { get; set; }
        [Required]
        public int WorkerID { get; set; }
        [Required]
        public DateTime HarvestDate { get; set; }
        [Range(0, double.MaxValue,ErrorMessage ="Quantity must be positive number.")]
        public decimal Quantity { get; set; } // Quantity of harvested produce

        // Navigation properties help establish relationships
        // between entities and enable Entity Framework Core
        // to understand the associations between tables.
        public Crop Crop { get; set; }
        public Worker Worker { get; set; }
    }
}

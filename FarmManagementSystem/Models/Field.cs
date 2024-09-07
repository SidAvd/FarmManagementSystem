using System.ComponentModel.DataAnnotations;

namespace FarmManagementSystem.Models
{
    public class Field
    {
        [Key]
        public int FieldID { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Size must be positive number.")]
        public decimal? Size { get; set; }
        [StringLength(200)]
        public string? Location { get; set; }

        // Navigation properties help establish relationships
        // between entities and enable Entity Framework Core
        // to understand the associations between tables.
        public ICollection<Crop> Crops { get; set; } = new List<Crop>();
        public ICollection<WorkerAssignment> WorkerAssignments { get; set; } = new List<WorkerAssignment>();
    }
}

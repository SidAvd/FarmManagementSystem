using System.ComponentModel.DataAnnotations;

namespace FarmManagementSystem.Models
{
    public class WorkerAssignment
    {
        [Key]
        public int WorkerID { get; set; }
        [Key]
        public int FieldID { get; set; }
        [Required]
        public DateTime Startdate { get; set; }
        public DateTime? EndDate { get; set; } // Nullable, it might still be ongoing

        // Navigation properties help establish relationships
        // between entities and enable Entity Framework Core
        // to understand the associations between tables.
        public Worker Worker { get; set; }
        public Field Field { get; set; }
    }
}

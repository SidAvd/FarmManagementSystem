using System.ComponentModel.DataAnnotations;

namespace FarmManagementSystem.Models
{
    public class Worker
    {
        [Key]
        public int WorkerID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Role { get; set; }
        [StringLength(100)]
        public string ContactInfo { get; set; }

        // Navigation properties help establish relationships
        // between entities and enable Entity Framework Core
        // to understand the associations between tables.
        public ICollection<Harvest> Harvests { get; set; } = new List<Harvest>();
        public ICollection<WorkerAssignment> WorkerAssignments { get; set; } = new List<WorkerAssignment>();
    }
}

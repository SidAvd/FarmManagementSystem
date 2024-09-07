namespace FarmManagementSystem.DTOs
{
    public class WorkerAssignmentDTO
    {
        public int WorkerID { get; set; }
        public int FieldID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

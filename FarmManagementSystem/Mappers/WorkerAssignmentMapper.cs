using FarmManagementSystem.DTOs;
using FarmManagementSystem.Models;

namespace FarmManagementSystem.Mappers
{
    public static class WorkerAssignmentMapper
    {
        // Maps WorkerAssignment entity to WorkerAssignmentDTO
        public static WorkerAssignmentDTO ToDTO(WorkerAssignment workerAssignment)
        {
            return new WorkerAssignmentDTO
            {
                WorkerID = workerAssignment.WorkerID,
                FieldID = workerAssignment.FieldID,
                StartDate = workerAssignment.Startdate,
                EndDate = workerAssignment.EndDate
            };
        }

        // Maps WorkerAssignmentDTO to WorkerAssignment entity
        public static WorkerAssignment ToEntity(WorkerAssignmentDTO workerAssignmentDTO)
        {
            return new WorkerAssignment
            {
                WorkerID = workerAssignmentDTO.WorkerID,
                FieldID = workerAssignmentDTO.FieldID,
                Startdate = workerAssignmentDTO.StartDate,
                EndDate = workerAssignmentDTO.EndDate
            };
        }
    }
}

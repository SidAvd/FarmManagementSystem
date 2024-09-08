using FarmManagementSystem.DTOs;

namespace FarmManagementSystem.Services.Interfaces
{
    public interface IWorkerAssignmentService
    {
        Task<IEnumerable<WorkerAssignmentDTO>> GetAllWorkerAssignmentsAsync();
        Task<WorkerAssignmentDTO> GetWorkerAssignmentByIdAsync(int workerId, int fieldId);
        Task<bool> UpdateWorkerAssignmentAsync(int workerId, int fieldId, WorkerAssignmentDTO workerAssignmentDTO);
        Task<WorkerAssignmentDTO> CreateWorkerAssignmentAsync(WorkerAssignmentDTO workerAssignmentDTO);
        Task<bool> DeleteWorkerAssignmentAsync(int workerId, int fieldId);
    }
}

using FarmManagementSystem.DTOs;

namespace FarmManagementSystem.Services.Interfaces
{
    public interface IWorkerService
    {
        Task<IEnumerable<WorkerDTO>> GetAllWorkersAsync();
        Task<WorkerDTO> GetWorkerByIdAsync(int id);
        Task<bool> UpdateWorkerAsync(int id, WorkerDTO workerDTO);
        Task<WorkerDTO> CreateWorkerAsync(WorkerDTO workerDTO);
        Task<bool> DeleteWorkerAsync(int id);
    }
}

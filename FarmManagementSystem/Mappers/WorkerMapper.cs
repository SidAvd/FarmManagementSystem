using FarmManagementSystem.DTOs;
using FarmManagementSystem.Models;

namespace FarmManagementSystem.Mappers
{
    public static class WorkerMapper
    {
        // Maps Worker entity to WorkerDTO
        public static WorkerDTO ToDTO(Worker worker)
        {
            return new WorkerDTO
            {
                WorkerID = worker.WorkerID,
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Role = worker.Role,
                ContactInfo = worker.ContactInfo
            };
        }

        // Maps WorkerDTO to Worker entity
        public static Worker ToEntity(WorkerDTO workerDTO)
        {
            return new Worker
            {
                WorkerID = workerDTO.WorkerID,
                FirstName = workerDTO.FirstName,
                LastName = workerDTO.LastName,
                Role = workerDTO.Role,
                ContactInfo = workerDTO.ContactInfo
            };
        }
    }
}

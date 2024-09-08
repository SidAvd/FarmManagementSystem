using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FarmManagementSystem.Data;
using FarmManagementSystem.DTOs;
using FarmManagementSystem.Models;
using FarmManagementSystem.Services.Interfaces;
using FarmManagementSystem.Mappers;  // Import the mappers

namespace FarmManagementSystem.Services.Implementations
{
    public class WorkerService : IWorkerService
    {
        private readonly FarmDbContext _context;

        public WorkerService(FarmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkerDTO>> GetAllWorkersAsync()
        {
            return await _context.Workers
                .Select(worker => WorkerMapper.ToDTO(worker))
                .ToListAsync();
        }

        public async Task<WorkerDTO> GetWorkerByIdAsync(int id)
        {
            var worker = await _context.Workers
                .FindAsync(id);

            if (worker == null)
            {
                return null;
            }

            return WorkerMapper.ToDTO(worker);
        }

        public async Task<bool> UpdateWorkerAsync(int id, WorkerDTO workerDTO)
        {
            if (id != workerDTO.WorkerID)
            {
                return false;
            }

            var existingWorker = await _context.Workers
                .FindAsync(id);

            if (existingWorker == null)
            {
                return false;
            }

            var worker = WorkerMapper.ToEntity(workerDTO);  // Convert DTO to entity
            _context.Entry(existingWorker).CurrentValues.SetValues(worker);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<WorkerDTO> CreateWorkerAsync(WorkerDTO workerDTO)
        {
            var worker = WorkerMapper.ToEntity(workerDTO);  // Convert DTO to entity

            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();

            return WorkerMapper.ToDTO(worker);  // Convert saved entity back to DTO
        }

        public async Task<bool> DeleteWorkerAsync(int id)
        {
            var worker = await _context.Workers
                .FindAsync(id);

            if (worker == null)
            {
                return false;
            }

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

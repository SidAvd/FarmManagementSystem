using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FarmManagementSystem.Data;
using FarmManagementSystem.DTOs;
using FarmManagementSystem.Mappers;
using FarmManagementSystem.Services.Interfaces;

namespace FarmManagementSystem.Services.Implementations
{
    public class WorkerAssignmentService : IWorkerAssignmentService
    {
        private readonly FarmDbContext _context;

        public WorkerAssignmentService(FarmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkerAssignmentDTO>> GetAllWorkerAssignmentsAsync()
        {
            var workerAssignments = await _context.WorkerAssignments
                .Include(wa => wa.Worker)
                .Include(wa => wa.Field)
                .ToListAsync();

            return workerAssignments.Select(wa => WorkerAssignmentMapper.ToDTO(wa));
        }

        public async Task<WorkerAssignmentDTO> GetWorkerAssignmentByIdAsync(int workerId, int fieldId)
        {
            var workerAssignment = await _context.WorkerAssignments
                .Include(wa => wa.Worker)
                .Include(wa => wa.Field)
                .FirstOrDefaultAsync(wa => wa.WorkerID == workerId && wa.FieldID == fieldId);

            return workerAssignment == null ? null : WorkerAssignmentMapper.ToDTO(workerAssignment);
        }

        public async Task<bool> UpdateWorkerAssignmentAsync(int workerId, int fieldId, WorkerAssignmentDTO workerAssignmentDTO)
        {
            if (workerId != workerAssignmentDTO.WorkerID || fieldId != workerAssignmentDTO.FieldID)
                return false;

            var workerAssignment = WorkerAssignmentMapper.ToEntity(workerAssignmentDTO);
            _context.Entry(workerAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.WorkerAssignments.AnyAsync(wa => wa.WorkerID == workerId && wa.FieldID == fieldId))
                    return false;
                throw;
            }
        }

        public async Task<WorkerAssignmentDTO> CreateWorkerAssignmentAsync(WorkerAssignmentDTO workerAssignmentDTO)
        {
            var workerAssignment = WorkerAssignmentMapper.ToEntity(workerAssignmentDTO);

            // Ensure the entities are only being referenced and not modified
            var existingField = await _context.Fields
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.FieldID == workerAssignment.FieldID);

            var existingWorker = await _context.Workers
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.WorkerID == workerAssignment.WorkerID);

            if (existingField == null || existingWorker == null)
            {
                return null;
            }

            _context.WorkerAssignments.Add(workerAssignment);
            await _context.SaveChangesAsync();

            return WorkerAssignmentMapper.ToDTO(workerAssignment);
        }

        public async Task<bool> DeleteWorkerAssignmentAsync(int workerId, int fieldId)
        {
            var workerAssignment = await _context.WorkerAssignments
                .FirstOrDefaultAsync(wa => wa.WorkerID == workerId && wa.FieldID == fieldId);

            if (workerAssignment == null)
                return false;

            _context.WorkerAssignments.Remove(workerAssignment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

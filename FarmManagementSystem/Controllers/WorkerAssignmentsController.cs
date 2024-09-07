using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmManagementSystem.Data;
using FarmManagementSystem.Models;
using FarmManagementSystem.DTOs;
using FarmManagementSystem.Mappers;

namespace FarmManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerAssignmentsController : ControllerBase
    {
        private readonly FarmDbContext _context;

        public WorkerAssignmentsController(FarmDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkerAssignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerAssignmentDTO>>> GetWorkerAssignments()
        {
            var workerAssignments = await _context.WorkerAssignments.ToListAsync();
            var workerAssignmentDTOs = workerAssignments.Select(wa => WorkerAssignmentMapper.ToDTO(wa)).ToList();
            return Ok(workerAssignmentDTOs);  // Return list of WorkerAssignmentDTOs
        }

        // GET: api/WorkerAssignments/5
        [HttpGet("{workerId}/{fieldId}")]
        public async Task<ActionResult<WorkerAssignmentDTO>> GetWorkerAssignment(int workerId, int fieldId)
        {
            var workerAssignment = await _context.WorkerAssignments
                .FirstOrDefaultAsync(wa => wa.WorkerID == workerId && wa.FieldID == fieldId);

            if (workerAssignment == null)
            {
                return NotFound();
            }

            return Ok(WorkerAssignmentMapper.ToDTO(workerAssignment));  // Return WorkerAssignmentDTO
        }

        // PUT: api/WorkerAssignments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{workerId}/{fieldId}")]
        public async Task<IActionResult> PutWorkerAssignment(int workerId, int fieldId, WorkerAssignmentDTO workerAssignmentDTO)
        {
            if (workerId != workerAssignmentDTO.WorkerID || fieldId != workerAssignmentDTO.FieldID)
            {
                return BadRequest();
            }

            var workerAssignment = WorkerAssignmentMapper.ToEntity(workerAssignmentDTO);  // Convert DTO to entity
            _context.Entry(workerAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerAssignmentExists(workerId, fieldId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WorkerAssignments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkerAssignmentDTO>> PostWorkerAssignment(WorkerAssignmentDTO workerAssignmentDTO)
        {
            // Ensure that only the WorkerAssignment is being added
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
                return BadRequest("Field or Worker does not exist.");
            }

            _context.WorkerAssignments.Add(workerAssignment);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log or handle exception
                if (WorkerAssignmentExists(workerAssignment.WorkerID, workerAssignment.FieldID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var createdWorkerAssignmentDTO = WorkerAssignmentMapper.ToDTO(workerAssignment);
            return CreatedAtAction("GetWorkerAssignment", new { workerId = workerAssignment.WorkerID, fieldId = workerAssignment.FieldID }, createdWorkerAssignmentDTO);
        }

        // DELETE: api/WorkerAssignments/5
        [HttpDelete("{workerId}/{fieldId}")]
        public async Task<IActionResult> DeleteWorkerAssignment(int workerId, int fieldId)
        {
            var workerAssignment = await _context.WorkerAssignments
                .FirstOrDefaultAsync(wa => wa.WorkerID == workerId && wa.FieldID == fieldId);

            if (workerAssignment == null)
            {
                return NotFound();
            }

            _context.WorkerAssignments.Remove(workerAssignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkerAssignmentExists(int workerId, int fieldId)
        {
            return _context.WorkerAssignments.Any(e => e.WorkerID == workerId && e.FieldID == fieldId);
        }
    }
}

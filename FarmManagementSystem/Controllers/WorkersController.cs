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
    public class WorkersController : ControllerBase
    {
        private readonly FarmDbContext _context;

        public WorkersController(FarmDbContext context)
        {
            _context = context;
        }

        // GET: api/Workers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerDTO>>> GetWorkers()
        {
            var workers = await _context.Workers.ToListAsync();
            var workerDTOs = workers.Select(worker => WorkerMapper.ToDTO(worker)).ToList();
            return Ok(workerDTOs);  // Return list of WorkerDTOs
        }

        // GET: api/Workers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerDTO>> GetWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);

            if (worker == null)
            {
                return NotFound();
            }

            return Ok(WorkerMapper.ToDTO(worker));  // Return WorkerDTO
        }

        // PUT: api/Workers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorker(int id, WorkerDTO workerDTO)
        {
            if (id != workerDTO.WorkerID)
            {
                return BadRequest();
            }

            var worker = WorkerMapper.ToEntity(workerDTO);  // Convert DTO to entity
            _context.Entry(worker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(id))
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

        // POST: api/Workers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkerDTO>> PostWorker(WorkerDTO workerDTO)
        {
            var worker = WorkerMapper.ToEntity(workerDTO);  // Convert DTO to entity
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();

            var createdWorkerDTO = WorkerMapper.ToDTO(worker);  // Convert saved entity back to DTO
            return CreatedAtAction("GetWorker", new { id = worker.WorkerID }, createdWorkerDTO);
        }

        // DELETE: api/Workers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(e => e.WorkerID == id);
        }
    }
}

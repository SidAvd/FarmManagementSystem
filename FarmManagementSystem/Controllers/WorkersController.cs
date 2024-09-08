using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FarmManagementSystem.DTOs;
using FarmManagementSystem.Services.Interfaces;

namespace FarmManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkersController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        // GET: api/Workers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerDTO>>> GetWorkers()
        {
            var workers = await _workerService.GetAllWorkersAsync();
            return Ok(workers);
        }

        // GET: api/Workers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerDTO>> GetWorker(int id)
        {
            var worker = await _workerService.GetWorkerByIdAsync(id);

            if (worker == null)
            {
                return NotFound();
            }

            return Ok(worker);
        }

        // PUT: api/Workers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorker(int id, WorkerDTO workerDTO)
        {
            if (id != workerDTO.WorkerID)
            {
                return BadRequest();
            }

            var result = await _workerService.UpdateWorkerAsync(id, workerDTO);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Workers
        [HttpPost]
        public async Task<ActionResult<WorkerDTO>> PostWorker(WorkerDTO workerDTO)
        {
            var createdWorkerDTO = await _workerService.CreateWorkerAsync(workerDTO);
            return CreatedAtAction(nameof(GetWorker), new { id = createdWorkerDTO.WorkerID }, createdWorkerDTO);
        }

        // DELETE: api/Workers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            var result = await _workerService.DeleteWorkerAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

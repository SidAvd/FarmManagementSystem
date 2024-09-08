using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FarmManagementSystem.DTOs;
using FarmManagementSystem.Services.Interfaces;

namespace FarmManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerAssignmentsController : ControllerBase
    {
        private readonly IWorkerAssignmentService _workerAssignmentService;

        public WorkerAssignmentsController(IWorkerAssignmentService workerAssignmentService)
        {
            _workerAssignmentService = workerAssignmentService;
        }

        // GET: api/WorkerAssignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerAssignmentDTO>>> GetWorkerAssignments()
        {
            var workerAssignments = await _workerAssignmentService.GetAllWorkerAssignmentsAsync();
            return Ok(workerAssignments);
        }

        // GET: api/WorkerAssignments/5/10
        [HttpGet("{workerId}/{fieldId}")]
        public async Task<ActionResult<WorkerAssignmentDTO>> GetWorkerAssignment(int workerId, int fieldId)
        {
            var workerAssignment = await _workerAssignmentService.GetWorkerAssignmentByIdAsync(workerId, fieldId);

            if (workerAssignment == null)
            {
                return NotFound();
            }

            return Ok(workerAssignment);
        }

        // PUT: api/WorkerAssignments/5/10
        [HttpPut("{workerId}/{fieldId}")]
        public async Task<IActionResult> PutWorkerAssignment(int workerId, int fieldId, WorkerAssignmentDTO workerAssignmentDTO)
        {
            if (workerId != workerAssignmentDTO.WorkerID || fieldId != workerAssignmentDTO.FieldID)
            {
                return BadRequest();
            }

            var result = await _workerAssignmentService.UpdateWorkerAssignmentAsync(workerId, fieldId, workerAssignmentDTO);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/WorkerAssignments
        [HttpPost]
        public async Task<ActionResult<WorkerAssignmentDTO>> PostWorkerAssignment(WorkerAssignmentDTO workerAssignmentDTO)
        {
            var createdWorkerAssignment = await _workerAssignmentService.CreateWorkerAssignmentAsync(workerAssignmentDTO);
            if (createdWorkerAssignment == null)
            {
                return BadRequest("Field or Worker does not exist.");
            }

            return CreatedAtAction("GetWorkerAssignment", new { workerId = createdWorkerAssignment.WorkerID, fieldId = createdWorkerAssignment.FieldID }, createdWorkerAssignment);
        }

        // DELETE: api/WorkerAssignments/5/10
        [HttpDelete("{workerId}/{fieldId}")]
        public async Task<IActionResult> DeleteWorkerAssignment(int workerId, int fieldId)
        {
            var result = await _workerAssignmentService.DeleteWorkerAssignmentAsync(workerId, fieldId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

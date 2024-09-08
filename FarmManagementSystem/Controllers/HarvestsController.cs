using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FarmManagementSystem.DTOs;
using FarmManagementSystem.Services.Interfaces;

namespace FarmManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarvestsController : ControllerBase
    {
        private readonly IHarvestService _harvestService;

        public HarvestsController(IHarvestService harvestService)
        {
            _harvestService = harvestService;
        }

        // GET: api/Harvests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HarvestDTO>>> GetHarvests()
        {
            var harvests = await _harvestService.GetAllHarvestsAsync();
            return Ok(harvests);
        }

        // GET: api/Harvests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HarvestDTO>> GetHarvest(int id)
        {
            var harvest = await _harvestService.GetHarvestByIdAsync(id);

            if (harvest == null)
            {
                return NotFound();
            }

            return Ok(harvest);
        }

        // PUT: api/Harvests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHarvest(int id, HarvestDTO harvestDTO)
        {
            if (id != harvestDTO.HarvestID)
            {
                return BadRequest();
            }

            var result = await _harvestService.UpdateHarvestAsync(id, harvestDTO);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Harvests
        [HttpPost]
        public async Task<ActionResult<HarvestDTO>> PostHarvest(HarvestDTO harvestDTO)
        {
            var createdHarvest = await _harvestService.CreateHarvestAsync(harvestDTO);
            return CreatedAtAction("GetHarvest", new { id = createdHarvest.HarvestID }, createdHarvest);
        }

        // DELETE: api/Harvests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHarvest(int id)
        {
            var result = await _harvestService.DeleteHarvestAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

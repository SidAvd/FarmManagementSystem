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
    public class HarvestsController : ControllerBase
    {
        private readonly FarmDbContext _context;

        public HarvestsController(FarmDbContext context)
        {
            _context = context;
        }

        // GET: api/Harvests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HarvestDTO>>> GetHarvests()
        {
            var harvests = await _context.Harvests.ToListAsync();
            var harvestDTOs = harvests.Select(harvest => HarvestMapper.ToDTO(harvest)).ToList();
            return Ok(harvestDTOs);  // Return list of HarvestDTOs
        }

        // GET: api/Harvests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HarvestDTO>> GetHarvest(int id)
        {
            var harvest = await _context.Harvests.FindAsync(id);

            if (harvest == null)
            {
                return NotFound();
            }

            return Ok(HarvestMapper.ToDTO(harvest));  // Return HarvestDTO
        }

        // PUT: api/Harvests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHarvest(int id, HarvestDTO harvestDTO)
        {
            if (id != harvestDTO.HarvestID)
            {
                return BadRequest();
            }

            var harvest = HarvestMapper.ToEntity(harvestDTO);  // Convert DTO to entity
            _context.Entry(harvest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HarvestExists(id))
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

        // POST: api/Harvests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HarvestDTO>> PostHarvest(HarvestDTO harvestDTO)
        {
            var harvest = HarvestMapper.ToEntity(harvestDTO);  // Convert DTO to entity
            _context.Harvests.Add(harvest);
            await _context.SaveChangesAsync();

            var createdHarvestDTO = HarvestMapper.ToDTO(harvest);  // Convert saved entity back to DTO
            return CreatedAtAction("GetHarvest", new { id = harvest.HarvestID }, createdHarvestDTO);
        }

        // DELETE: api/Harvests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHarvest(int id)
        {
            var harvest = await _context.Harvests.FindAsync(id);
            if (harvest == null)
            {
                return NotFound();
            }

            _context.Harvests.Remove(harvest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HarvestExists(int id)
        {
            return _context.Harvests.Any(e => e.HarvestID == id);
        }
    }
}

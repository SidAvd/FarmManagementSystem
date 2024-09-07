using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmManagementSystem.Data;
using FarmManagementSystem.DTOs;
using FarmManagementSystem.Models;
using FarmManagementSystem.Mappers;

namespace FarmManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropsController : ControllerBase
    {
        private readonly FarmDbContext _context;

        public CropsController(FarmDbContext context)
        {
            _context = context;
        }

        // GET: api/Crops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CropDTO>>> GetCrops()
        {
            var crops = await _context.Crops
                .Include(c => c.Field)
                .ToListAsync();

            // Use CropMapper to map the list of Crop entities to CropDTOs
            var cropDTOs = crops.Select(c => CropMapper.ToDTO(c)).ToList();

            return Ok(cropDTOs);
        }

        // GET: api/Crops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CropDTO>> GetCrop(int id)
        {
            var crop = await _context.Crops
                .Include(c => c.Field)
                .FirstOrDefaultAsync(c => c.CropID == id);

            if (crop == null)
            {
                return NotFound();
            }

            // Use CropMapper to convert the entity to DTO
            var cropDTO = CropMapper.ToDTO(crop);

            return cropDTO;
        }

        // PUT: api/Crops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrop(int id, CropDTO cropDTO)
        {
            if (id != cropDTO.CropID)
            {
                return BadRequest();
            }

            // Convert DTO to entity using CropMapper
            var crop = CropMapper.ToEntity(cropDTO);

            _context.Entry(crop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CropExists(id))
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

        // POST: api/Crops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CropDTO>> PostCrop(CropDTO cropDTO)
        {
            // Convert DTO to entity using CropMapper
            var crop = CropMapper.ToEntity(cropDTO);

            _context.Crops.Add(crop);
            await _context.SaveChangesAsync();

            // Return the newly created crop as cropDTO
            var createdCropDTO = CropMapper.ToDTO(crop);

            return CreatedAtAction("GetCrop", new { id = crop.CropID }, createdCropDTO);
        }

        // DELETE: api/Crops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrop(int id)
        {
            var crop = await _context.Crops.FindAsync(id);
            if (crop == null)
            {
                return NotFound();
            }

            _context.Crops.Remove(crop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CropExists(int id)
        {
            return _context.Crops.Any(e => e.CropID == id);
        }
    }
}

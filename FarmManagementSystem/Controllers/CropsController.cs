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
using FarmManagementSystem.Services.Interfaces;

namespace FarmManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropsController : ControllerBase
    {
        private readonly ICropService _cropService;

        public CropsController(ICropService cropService)
        {
            _cropService = cropService;
        }

        // GET: api/Crops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CropDTO>>> GetCrops()
        {
            var crops = await _cropService.GetAllCropsAsync();
            return Ok(crops);
        }

        // GET: api/Crops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CropDTO>> GetCrop(int id)
        {
            var crop = await _cropService.GetCropByIdAsync(id);
            if (crop == null) return NotFound();
            return Ok(crop);
        }

        // PUT: api/Crops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrop(int id, CropDTO cropDTO)
        {
            if(!await _cropService.UpdateCropAsync(id,cropDTO)) 
                return BadRequest();
            return NoContent();
        }

        // POST: api/Crops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CropDTO>> PostCrop(CropDTO cropDTO)
        {
            var createdCrop = await _cropService.CreateCropAsync(cropDTO);
            return CreatedAtAction(nameof(GetCrop), new { id = createdCrop.CropID }, createdCrop);
        }

        // DELETE: api/Crops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrop(int id)
        {
            if (!await _cropService.DeleteCropAsync(id)) return NotFound();
            return NoContent();
        }
    }
}

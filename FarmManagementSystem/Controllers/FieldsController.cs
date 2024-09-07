using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmManagementSystem.Data;
using FarmManagementSystem.Models;
using FarmManagementSystem.Mappers;
using FarmManagementSystem.DTOs;

namespace FarmManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly FarmDbContext _context;

        public FieldsController(FarmDbContext context)
        {
            _context = context;
        }

        // GET: api/Fields
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldDTO>>> GetFields()
        {
            var fields = await _context.Fields.ToListAsync();
            var fieldDTOs = fields.Select(f => FieldMapper.ToDTO(f)).ToList();

            return fieldDTOs;
        }

        // GET: api/Fields/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FieldDTO>> GetField(int id)
        {
            var @field = await _context.Fields.FindAsync(id);

            if (@field == null)
            {
                return NotFound();
            }

            return Ok(FieldMapper.ToDTO(@field));
        }

        // PUT: api/Fields/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutField(int id, FieldDTO fieldDTO)
        {
            if (id != fieldDTO.FieldID)
            {
                return BadRequest();
            }

            // Convert DTO to entity
            var @field = FieldMapper.ToEntity(fieldDTO);
            _context.Entry(@field).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FieldExists(id))
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

        // POST: api/Fields
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FieldDTO>> PostField(FieldDTO fieldDTO)
        {
            // Convert DTO to entity
            var @field = FieldMapper.ToEntity(fieldDTO);
            _context.Fields.Add(@field);
            await _context.SaveChangesAsync();

            // Convert saved entity to DTO
            var createdFieldDTO = FieldMapper.ToDTO(field);


            return CreatedAtAction("GetField", new { id = @field.FieldID }, createdFieldDTO);
        }

        // DELETE: api/Fields/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            var @field = await _context.Fields.FindAsync(id);
            if (@field == null)
            {
                return NotFound();
            }

            _context.Fields.Remove(@field);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FieldExists(int id)
        {
            return _context.Fields.Any(e => e.FieldID == id);
        }
    }
}

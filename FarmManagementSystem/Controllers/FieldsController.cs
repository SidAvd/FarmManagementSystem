using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FarmManagementSystem.DTOs;
using FarmManagementSystem.Services.Interfaces;

namespace FarmManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldsController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        // GET: api/Fields
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldDTO>>> GetFields()
        {
            var fields = await _fieldService.GetAllFieldsAsync();
            return Ok(fields);
        }

        // GET: api/Fields/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FieldDTO>> GetField(int id)
        {
            var field = await _fieldService.GetFieldByIdAsync(id);

            if (field == null)
            {
                return NotFound();
            }

            return Ok(field);
        }

        // PUT: api/Fields/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutField(int id, FieldDTO fieldDTO)
        {
            if (id != fieldDTO.FieldID)
            {
                return BadRequest();
            }

            var result = await _fieldService.UpdateFieldAsync(id, fieldDTO);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Fields
        [HttpPost]
        public async Task<ActionResult<FieldDTO>> PostField(FieldDTO fieldDTO)
        {
            var createdField = await _fieldService.CreateFieldAsync(fieldDTO);
            return CreatedAtAction("GetField", new { id = createdField.FieldID }, createdField);
        }

        // DELETE: api/Fields/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            var result = await _fieldService.DeleteFieldAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
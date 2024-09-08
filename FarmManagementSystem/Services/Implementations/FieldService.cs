using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FarmManagementSystem.Data;
using FarmManagementSystem.DTOs;
using FarmManagementSystem.Mappers;
using FarmManagementSystem.Services.Interfaces;

namespace FarmManagementSystem.Services.Implementations
{
    public class FieldService : IFieldService
    {
        private readonly FarmDbContext _context;

        public FieldService(FarmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FieldDTO>> GetAllFieldsAsync()
        {
            var fields = await _context.Fields.ToListAsync();
            return fields.Select(FieldMapper.ToDTO);
        }

        public async Task<FieldDTO> GetFieldByIdAsync(int id)
        {
            var field = await _context.Fields.FindAsync(id);
            return field == null ? null : FieldMapper.ToDTO(field);
        }

        public async Task<bool> UpdateFieldAsync(int id, FieldDTO fieldDTO)
        {
            if (id != fieldDTO.FieldID)
                return false;

            var field = FieldMapper.ToEntity(fieldDTO);
            _context.Entry(field).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Fields.AnyAsync(f => f.FieldID == id))
                    return false;
                throw;
            }
        }

        public async Task<FieldDTO> CreateFieldAsync(FieldDTO fieldDTO)
        {
            var field = FieldMapper.ToEntity(fieldDTO);
            _context.Fields.Add(field);
            await _context.SaveChangesAsync();
            return FieldMapper.ToDTO(field);
        }

        public async Task<bool> DeleteFieldAsync(int id)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null)
                return false;

            _context.Fields.Remove(field);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
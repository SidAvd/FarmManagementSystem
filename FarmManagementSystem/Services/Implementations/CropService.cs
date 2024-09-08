using FarmManagementSystem.Data;
using FarmManagementSystem.DTOs;
using FarmManagementSystem.Services.Interfaces;
using FarmManagementSystem.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;



namespace FarmManagementSystem.Services.Implementations
{
    public class CropService : ICropService
    {
        private readonly FarmDbContext _context;

        public CropService(FarmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CropDTO>> GetAllCropsAsync()
        {
            var crops = await _context.Crops
                .Include(c => c.Field)
                .ToListAsync();
            return crops.Select(c => CropMapper.ToDTO(c));
        }

        public async Task<CropDTO> GetCropByIdAsync(int id)
        {
            var crop = await _context.Crops
                .Include(c => c.Field)
                .FirstOrDefaultAsync(c => c.CropID == id);
            return crop != null ? CropMapper.ToDTO(crop) : null;
        }

        public async Task<bool> UpdateCropAsync(int id, CropDTO cropDTO)
        {
            if (id != cropDTO.CropID) return false;
            var crop = CropMapper.ToEntity(cropDTO);
            _context.Entry(crop).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Crops.Any(e => e.CropID == id)) return false;
                throw;
            }
        }

        public async Task<CropDTO> CreateCropAsync(CropDTO cropDTO)
        {
            var crop = CropMapper.ToEntity(cropDTO);
            _context.Crops.Add(crop);
            await _context.SaveChangesAsync();
            return CropMapper.ToDTO(crop);
        }

        public async Task<bool> DeleteCropAsync(int id)
        {
            var crop = await _context.Crops.FindAsync(id);
            if (crop == null) return false;
            _context.Crops.Remove(crop);
            await _context.SaveChangesAsync();
            return true;
        }
    }   
}

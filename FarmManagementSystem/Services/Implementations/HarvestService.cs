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
    public class HarvestService : IHarvestService
    {
        private readonly FarmDbContext _context;

        public HarvestService(FarmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HarvestDTO>> GetAllHarvestsAsync()
        {
            var harvests = await _context.Harvests
                .Include(h => h.Crop)
                .Include(h => h.Worker)
                .ToListAsync();
            return harvests.Select(harvest => HarvestMapper.ToDTO(harvest));
        }

        public async Task<HarvestDTO> GetHarvestByIdAsync(int id)
        {
            var harvest = await _context.Harvests
                .Include(h => h.Crop)
                .Include(h => h.Worker)
                .FirstOrDefaultAsync(h => h.HarvestID == id);

            return harvest == null ? null : HarvestMapper.ToDTO(harvest);
        }

        public async Task<bool> UpdateHarvestAsync(int id, HarvestDTO harvestDTO)
        {
            if (id != harvestDTO.HarvestID)
                return false;

            var harvest = HarvestMapper.ToEntity(harvestDTO);
            _context.Entry(harvest).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Harvests.AnyAsync(h => h.HarvestID == id))
                    return false;
                throw;
            }
        }

        public async Task<HarvestDTO> CreateHarvestAsync(HarvestDTO harvestDTO)
        {
            var harvest = HarvestMapper.ToEntity(harvestDTO);
            _context.Harvests.Add(harvest);
            await _context.SaveChangesAsync();
            return HarvestMapper.ToDTO(harvest);
        }

        public async Task<bool> DeleteHarvestAsync(int id)
        {
            var harvest = await _context.Harvests.FindAsync(id);
            if (harvest == null)
                return false;

            _context.Harvests.Remove(harvest);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


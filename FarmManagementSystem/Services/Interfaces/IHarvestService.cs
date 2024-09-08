using FarmManagementSystem.DTOs;

namespace FarmManagementSystem.Services.Interfaces
{
    public interface IHarvestService
    {
        Task<IEnumerable<HarvestDTO>> GetAllHarvestsAsync();
        Task<HarvestDTO> GetHarvestByIdAsync(int id);
        Task<bool> UpdateHarvestAsync(int id, HarvestDTO harvestDTO);
        Task<HarvestDTO> CreateHarvestAsync(HarvestDTO harvestDTO);
        Task<bool> DeleteHarvestAsync(int id);
    }
}

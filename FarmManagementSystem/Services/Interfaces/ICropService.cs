using FarmManagementSystem.DTOs;

namespace FarmManagementSystem.Services.Interfaces
{
    public interface ICropService
    {
        Task<IEnumerable<CropDTO>> GetAllCropsAsync();
        Task<CropDTO> GetCropByIdAsync(int id);
        Task<bool> UpdateCropAsync(int id, CropDTO cropDTO);
        Task<CropDTO> CreateCropAsync(CropDTO cropDTO);
        Task<bool> DeleteCropAsync(int id);
    }
}

using FarmManagementSystem.DTOs;

namespace FarmManagementSystem.Services.Interfaces
{
    public interface IFieldService
    {
        Task<IEnumerable<FieldDTO>> GetAllFieldsAsync();
        Task<FieldDTO> GetFieldByIdAsync(int id);
        Task<bool> UpdateFieldAsync(int id, FieldDTO fieldDTO);
        Task<FieldDTO> CreateFieldAsync(FieldDTO fieldDTO);
        Task<bool> DeleteFieldAsync(int id);
    }
}

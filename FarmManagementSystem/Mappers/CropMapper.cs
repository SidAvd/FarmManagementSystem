using FarmManagementSystem.DTOs;
using FarmManagementSystem.Models;

namespace FarmManagementSystem.Mappers
{
    public static class CropMapper
    {
        public static CropDTO ToDTO(Crop crop)
        {
            return new CropDTO
            {
                CropID = crop.CropID,
                Name = crop.Name,
                PlantingDate = crop.PlantingDate,
                HarvestDate = crop.HarvestDate,
                FieldID = crop.FieldID,
                FieldName = crop.Field?.Name
            };
        }

        public static Crop ToEntity(CropDTO cropDTO)
        {
            return new Crop
            {
                CropID = cropDTO.CropID,
                Name = cropDTO.Name,
                PlantingDate = cropDTO.PlantingDate,
                HarvestDate = cropDTO.HarvestDate,
                FieldID = cropDTO.FieldID
            };
        }
    }
}

using FarmManagementSystem.DTOs;
using FarmManagementSystem.Models;

namespace FarmManagementSystem.Mappers
{
    public static class FieldMapper
    {
        // Maps Field entity to FieldDTO
        public static FieldDTO ToDTO(Field field)
        {
            return new FieldDTO
            {
                FieldID = field.FieldID,
                Name = field.Name,
                Size = field.Size,
                Location = field.Location
            };
        }

        // Maps FieldDTO to Field entity
        public static Field ToEntity(FieldDTO fieldDTO)
        {
            return new Field
            {
                FieldID = fieldDTO.FieldID,
                Name = fieldDTO.Name,
                Size = fieldDTO.Size,
                Location = fieldDTO.Location
            };
        }
    }
}

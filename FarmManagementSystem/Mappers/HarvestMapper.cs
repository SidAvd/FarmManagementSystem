using FarmManagementSystem.DTOs;
using FarmManagementSystem.Models;

namespace FarmManagementSystem.Mappers
{
    public static class HarvestMapper
    {
        // Maps Harvest entity to HarvestDTO
        public static HarvestDTO ToDTO(Harvest harvest)
        {
            return new HarvestDTO
            {
                HarvestID = harvest.HarvestID,
                CropID = harvest.CropID,
                WorkerID = harvest.WorkerID,
                HarvestDate = harvest.HarvestDate,
                Quantity = harvest.Quantity
            };
        }

        // Maps HarvestDTO to Harvest entity
        public static Harvest ToEntity(HarvestDTO harvestDTO)
        {
            return new Harvest
            {
                HarvestID = harvestDTO.HarvestID,
                CropID = harvestDTO.CropID,
                WorkerID = harvestDTO.WorkerID,
                HarvestDate = harvestDTO.HarvestDate,
                Quantity = harvestDTO.Quantity
            };
        }
    }
}

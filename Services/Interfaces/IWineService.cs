
using Common.Enums;
using Common.Models;
using Data.Entities;

namespace Services.Interfaces
{
    public interface IWineService
    {
        void CreateWine(CreateWineDTO createWineDTO);
        IEnumerable<Wine> GetAvailableWines();
        IEnumerable<Wine> GetWinesByVariety(Variety variety);
        void UpdateWineStockById(int id, int stock);
    }
}

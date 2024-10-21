using Common.Enums;
using Common.Models;
using Data.Entities;

namespace Data.Repository.Interfaces
{
    public interface IWineRepository
    {
        IEnumerable<Wine> GetWines();
        IEnumerable<Wine> GetAvailableWines();
        IEnumerable<Wine> GetWinesByVariety(Variety variety);
        int CreateWine(Wine wine);
        void UpdateWineStockById(int id, int stock);
    }
}
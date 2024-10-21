using Data.Entities;
using Common.Models;
using Data.Repository.Interfaces;
using Common.Enums;
using Services.Interfaces;

namespace Services.Implementations
{
    public class WineService : IWineService
    {
        public readonly IWineRepository _repository;
        public WineService(IWineRepository repository)
        {
            _repository = repository;
        }


        public void CreateWine(CreateWineDTO createWineDTO)
        {
            if (_repository.GetWines().All(wine => wine.Name != createWineDTO.Name))
            {
                _repository.CreateWine(
                new Wine
                {
                    Name = createWineDTO.Name,
                    Variety = createWineDTO.Variety,
                    Year = createWineDTO.Year,
                    Region = createWineDTO.Region,
                    Stock = createWineDTO.Stock,
                    CreatedAt = DateTime.UtcNow,
                }
                );
            }
            else throw new InvalidOperationException();
        }

        public IEnumerable<Wine> GetAvailableWines()
        {
            return _repository.GetAvailableWines();
        }

        public IEnumerable<Wine> GetWinesByVariety(Variety variety)
        {
            return _repository.GetWinesByVariety(variety);
        }

        public void UpdateWineStockById(int id, int stock)
        {
            _repository.UpdateWineStockById(id, stock);
        }
    }
}
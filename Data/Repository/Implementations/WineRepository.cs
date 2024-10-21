using Common.Enums;
using Data.Entities;
using Data.Repository.Interfaces;

namespace Data.Repository.Implementations
{
    public class WineRepository : IWineRepository
    {
        private readonly WineCellarContext _context;
        public WineRepository(WineCellarContext context)
        {
            _context = context;
        }
        public IEnumerable<Wine> GetWines()
        {
            return _context.Wines.ToList();
        }

        public IEnumerable<Wine> GetAvailableWines()
        {
            try
            {
                return _context.Wines.Where(w => w.Stock > 0);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public IEnumerable<Wine> GetWinesByVariety(Variety variety)
        {
            try
            {
                return _context.Wines.Where(w => w.Variety == variety && w.Stock > 0);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public int CreateWine(Wine wine)
        {
            try
            {
                _context.Add(wine);
                _context.SaveChanges();
                return _context.Wines.Max(w => w.Id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public void UpdateWineStockById(int id, int newStock)
        {
            try
            {
                _context.Wines.Single(w => w.Id == id).Stock = newStock;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
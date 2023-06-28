using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.Interfaces
{
    public interface IVansRepository
    {
        Task<Vans> GetByIdAsync(int id);
        Task<Vans> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Vans>> GetAllVansByCity(string city);
        Task<IEnumerable<Vans>> GetAll();

        bool Add(Vans vans);
        bool Update(Vans vans);
        bool Delete(Vans vans);
        bool Save();
    }
}

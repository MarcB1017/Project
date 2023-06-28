using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.Interfaces
{
    public interface IBikesRepository
    {
        Task<Bikes> GetByIdAsync(int id);
        Task<Bikes> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Bikes>> GetAllBikesByCity(string city);
        Task<IEnumerable<Bikes>> GetAll();

        bool Add(Bikes bike);
        bool Update(Bikes bike);
        bool Delete(Bikes bike);
        bool Save();
    }
}

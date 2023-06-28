using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.Interfaces
{
    public interface ICarsRepository
    {
        Task<Cars> GetByIdAsync(int id);
        Task<Cars> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Cars>> GetAllCarsByCity(string city);
        Task<IEnumerable<Cars>> GetAll();

        bool Add(Cars cars);
        bool Update(Cars cars);
        bool Delete(Cars cars);
        bool Save();
    }
}

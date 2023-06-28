using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.Interfaces
{
    public interface IDashBoardRepository
    {
        Task<List<Vans>> GetAllUserVans();
        Task<List<Bikes>> GetAllUserBikes();
        Task<List<Cars>> GetAllUserCars();

        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetUserByIdNoTracking(string id);

        bool Update(AppUser user);
        bool Save();
    }
}

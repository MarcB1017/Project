using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string id);

        Task<AppUser> GetUserByIdNoTracking(String id);
        Task<AppUser> GetUserByIdAsync(string id);

        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
    }
}

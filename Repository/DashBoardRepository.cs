using Microsoft.EntityFrameworkCore;
using MVC_CarsForSale.Data;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.Repository
{
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashBoardRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Cars>> GetAllUserCars()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userCars = _context.Cars.Where(x => x.AppUser.Id == curUser);
            return await Task.Run(() => userCars.ToList());
        }
        public async Task<List<Bikes>> GetAllUserBikes()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userBikes =  _context.Bikes.Where(x => x.AppUser.Id == curUser);
            return await Task.Run(()=>userBikes.ToList());
        }
        public async Task<List<Vans>> GetAllUserVans()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userVans = _context.Vans.Where(x => x.AppUser.Id == curUser);
            return await Task.Run(()=>userVans.ToList());
        }
        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await _context.Users.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }
    }
}

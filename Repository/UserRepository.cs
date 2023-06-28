using Microsoft.EntityFrameworkCore;
using MVC_CarsForSale.Data;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context){ _context = context;}
        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await _context.Users.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public bool Add(AppUser user)
        {
            _context.Add(user);
            return Save();
        }
        public bool Delete(AppUser user)
        {
            _context.Remove(user);
            return Save();
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

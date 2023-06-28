using Microsoft.EntityFrameworkCore;
using MVC_CarsForSale.Data;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.Repository
{
    public class CarsRepository : ICarsRepository
    {
        private readonly AppDbContext _context;
        public CarsRepository(AppDbContext context) { _context = context; }
        public async Task<IEnumerable<Cars>> GetAll()
        {
            return await _context.Cars.ToListAsync();
        }
        public async Task<IEnumerable<Cars>> GetAllCarsByCity(string city)
        {
            return await _context.Cars.Where(x => x.Address.City.Contains(city)).ToListAsync();
        }
        public async Task<Cars> GetByIdAsync(int id)
        {
            return await _context.Cars.Include(x => x.Address).Include(v => v.AppUser).FirstOrDefaultAsync(y => y.Id == id);
        }
        public async Task<Cars> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Cars.Include(x => x.Address).AsNoTracking().FirstOrDefaultAsync();
        }
        public bool Add(Cars cars)
        {
            _context.Add(cars);
            return Save();
        }
        public bool Delete(Cars cars)
        {
            _context.Remove(cars);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool Update(Cars cars)
        {
            _context.Update(cars);
            return Save();
        }
    }
}

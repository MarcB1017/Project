using Microsoft.EntityFrameworkCore;
using MVC_CarsForSale.Data;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.Repository
{
    public class BikesRepository : IBikesRepository
    {
        private readonly AppDbContext _context;
        public BikesRepository(AppDbContext context){ _context = context;}

        public async Task<IEnumerable<Bikes>> GetAll()
        {
            return await _context.Bikes.ToListAsync();
        }
        public async Task<IEnumerable<Bikes>> GetAllBikesByCity(string city)
        {
            return await _context.Bikes.Where(x => x.Address.City.Contains(city)).ToListAsync();
        }
     
        public async Task<Bikes> GetByIdAsync(int id)
        {
            return await _context.Bikes.Include(x => x.Address).Include(v => v.AppUser).FirstOrDefaultAsync(y => y.Id == id);
        }
        public async Task<Bikes> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Bikes.Include(x => x.Address).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Add(Bikes bike)
        {
            _context.Add(bike);
            return Save();
        }
        public bool Update(Bikes bike)
        {
            _context.Update(bike);
            return Save();
        }
        public bool Delete(Bikes bike)
        {
            _context.Remove(bike);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

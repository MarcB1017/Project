using Microsoft.EntityFrameworkCore;
using MVC_CarsForSale.Data;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.Repository
{
    public class VansRepository : IVansRepository
    {
        private readonly AppDbContext _context;
        public VansRepository(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<Vans>> GetAll()
        {
            return await _context.Vans.ToListAsync();
        }
        public async Task<IEnumerable<Vans>> GetAllVansByCity(string city)
        {
            return await _context.Vans.Where(x => x.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Vans> GetByIdAsync(int id)
        {
            return await _context.Vans.Include(x => x.Address).Include(v => v.AppUser).FirstOrDefaultAsync(y => y.Id == id);
        }
        public async Task<Vans> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Vans.Include(x => x.Address).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Add(Vans vans)
        {
            _context.Add(vans);
            return Save();
        }
        public bool Update(Vans vans)
        {
            _context.Update(vans);
            return Save();
        }
        public bool Delete(Vans vans)
        {
            _context.Remove(vans);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MVC_CarsForSale.Data;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.Repository
{
	public class SearchRepository : ISearchRepository
	{
		private readonly AppDbContext _appDbContext;
		public SearchRepository(AppDbContext appDbContext) { _appDbContext = appDbContext;}

		public async Task<IEnumerable<Cars>> GetCarsByCity(string city)
		{
			return await _appDbContext.Cars.Where(x => x.Address.City.Contains(city)).ToListAsync();
		}
		public async Task<IEnumerable<Cars>> GetAllCarsByBrand(string brand)
		{
			return await _appDbContext.Cars.Where(x => x.CarBrand.Contains(brand)).ToListAsync();
		}
		public async Task<IEnumerable<Cars>> GetAllCarsByYear(string year)
		{
			return await _appDbContext.Cars.Where(x => x.CarYear.ToString().Contains(year)).ToListAsync();
		}
		public async Task<IEnumerable<Cars>> GetAllCarsByPrice(string price)
		{
			return await _appDbContext.Cars.Where(x => x.Price.ToString().Contains(price)).ToListAsync();
		}



		public async Task<IEnumerable<Bikes>> GetBikesByCity(string city)
		{
			return await _appDbContext.Bikes.Where(x => x.Address.City.Contains(city)).ToListAsync();
		}
		public async Task<IEnumerable<Bikes>> GetAllBikesByBrand(string brand)
		{
			return await _appDbContext.Bikes.Where(x => x.BikeBrand.Contains(brand)).ToListAsync();
		}
		public async Task<IEnumerable<Bikes>> GetAllBikesByYear(string year)
		{
			return await _appDbContext.Bikes.Where(x => x.BikeYear.ToString().Contains(year)).ToListAsync();
		}
		public async Task<IEnumerable<Bikes>> GetAllBikesByPrice(string price)
		{
			return await _appDbContext.Bikes.Where(x => x.Price.ToString().Contains(price)).ToListAsync();
		}



		public async Task<IEnumerable<Vans>> GetVansByCity(string city)
		{
			return await _appDbContext.Vans.Where(x => x.Address.City.Contains(city)).ToListAsync();
		}
		public async Task<IEnumerable<Vans>> GetAllVansByBrand(string brand)
		{
			return await _appDbContext.Vans.Where(x=>x.VanBrand.Contains(brand)).ToListAsync();
		}
		public async Task<IEnumerable<Vans>> GetAllVansByYear(string year)
		{
			return await _appDbContext.Vans.Where(x=>x.VanYear.ToString().Contains(year)).ToListAsync();
		}
		public async Task<IEnumerable<Vans>> GetAllVansByPrice(string price)
		{
			return await _appDbContext.Vans.Where(x => x.Price.ToString().Contains(price)).ToListAsync();
		}
	}
}

using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.Interfaces
{
	public interface ISearchRepository
	{
		Task<IEnumerable<Cars>> GetCarsByCity(string city);
		Task<IEnumerable<Cars>> GetAllCarsByBrand(string brand);
		Task<IEnumerable<Cars>> GetAllCarsByPrice(string price);
		Task<IEnumerable<Cars>> GetAllCarsByYear(string year);

		Task<IEnumerable<Bikes>> GetBikesByCity(string city);
		Task<IEnumerable<Bikes>> GetAllBikesByBrand(string brand);
		Task <IEnumerable<Bikes>> GetAllBikesByPrice(string price);
		Task <IEnumerable<Bikes>> GetAllBikesByYear(string year);

		Task<IEnumerable<Vans>> GetVansByCity(string city);
		Task<IEnumerable<Vans>> GetAllVansByBrand(string brand);
		Task <IEnumerable<Vans>> GetAllVansByPrice(string price);
		Task <IEnumerable<Vans>> GetAllVansByYear(string year);
	}
}

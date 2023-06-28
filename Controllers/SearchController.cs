using Microsoft.AspNetCore.Mvc;
using MVC_CarsForSale.Data;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;
using MVC_CarsForSale.ViewModels.Search;

namespace MVC_CarsForSale.Controllers
{
	public class SearchController : Controller
	{
		private readonly AppDbContext _appDbContext;
		private readonly ISearchRepository _searchRepository;
		public SearchController(AppDbContext appDbContext, ISearchRepository searchRepository)
		{
			_appDbContext = appDbContext;
			_searchRepository = searchRepository;
		}


		public async Task<IActionResult> GetAllCarsByCity(string SearchText = "", int pg = 1)
		{
			IEnumerable<Cars> cars;

			if (SearchText != "" && SearchText != null)
			{
				cars = await _searchRepository.GetCarsByCity(SearchText);
			}
			else
		 
			cars = _appDbContext.Cars.ToList();

			const int pageSize = 12;

			if (pg < 1)
				pg = 1;

			int recsCount = cars.Count();
			int recSkip = (pg - 1) * pageSize;

			List<Cars> carsList = cars.Skip(recSkip).Take(pageSize).ToList();
			SPager SearchPager = new SPager(recsCount, pg, pageSize) { Action = "GetAllCarsByCity", Controller = "Search", SearchText = SearchText };

			this.ViewBag.SearchPager = SearchPager;

			return View(carsList);
		}
		public async Task<IActionResult> GetAllCarsByBrand(string SearchText = "", int pg = 1)
		{
			IEnumerable<Cars> cars;

			if (SearchText != "" && SearchText != null)
			{
				cars = await _searchRepository.GetAllCarsByBrand(SearchText);
			}
			else
				cars = _appDbContext.Cars.ToList();

			const int pageSize = 12;

			if (pg < 1)
				pg = 1;

			int recsCount = cars.Count();
			int recSkip = (pg - 1) * pageSize;

			List<Cars> carsList = cars.Skip(recSkip).Take(pageSize).ToList();
			SPager SearchPager = new SPager(recsCount, pg, pageSize) { Action = "GetAllCarsByBrand", Controller = "Search", SearchText = SearchText };

			this.ViewBag.SearchPager = SearchPager;

			return View(carsList);
		}
		public async Task<IActionResult> GetAllCarsByPrice(string SearchText = "", int pg = 1)
		{
			IEnumerable<Cars> cars;

			if (SearchText != "" && SearchText != null)
			{
				cars = await _searchRepository.GetAllCarsByPrice(SearchText);
			}
			else
				cars = _appDbContext.Cars.ToList();

			const int pageSize = 12;

			if (pg < 1)
				pg = 1;

			int recsCount = cars.Count();
			int recSkip = (pg - 1) * pageSize;

			List<Cars> carsList = cars.Skip(recSkip).Take(pageSize).ToList();
			SPager SearchPager = new SPager(recsCount, pg, pageSize) { Action = "GetAllCarsByPrice", Controller = "Search", SearchText = SearchText };

			this.ViewBag.SearchPager = SearchPager;

			return View(carsList);
		}
		public async Task<IActionResult> GetAllCarsByYear(string SearchText = "", int pg = 1)
		{
			IEnumerable<Cars> cars;

			if (SearchText != "" && SearchText != null)
			{
				cars = await _searchRepository.GetAllCarsByYear(SearchText);
			}
			else
				cars = _appDbContext.Cars.ToList();

			const int pageSize = 12;

			if (pg < 1)
				pg = 1;

			int recsCount = cars.Count();
			int recSkip = (pg - 1) * pageSize;

			List<Cars> carsList = cars.Skip(recSkip).Take(pageSize).ToList();
			SPager SearchPager = new SPager(recsCount, pg, pageSize) { Action = "GetAllCarsByYear", Controller = "Search", SearchText = SearchText };

			this.ViewBag.SearchPager = SearchPager;

			return View(carsList);
		}


		public async Task<IActionResult> GetAllBikesByCity(string SearchText = "", int pg = 1)
		{
			IEnumerable<Bikes> bikes;

			if (SearchText != "" && SearchText != null)
			{
				bikes = await _searchRepository.GetBikesByCity(SearchText);
			}
			else
				bikes = _appDbContext.Bikes.ToList();

			const int PageSize = 12;

			if (pg < 1)
				pg = 1;

			int recsCount = bikes.Count();
			int recSkip = (pg - 1) * PageSize;

			List<Bikes> bikesList = bikes.Skip(recSkip).Take(PageSize).ToList();
			SPager searchPager = new SPager(recsCount, pg, PageSize) { Action = "GetAllBikesByCity", Controller = "Search", SearchText = SearchText };

			this.ViewBag.SearchPager = searchPager;

			return View(bikesList);
		}
		public async Task<IActionResult> GetAllBikesByBrand(string SearchText="", int pg=1)
		{
			IEnumerable<Bikes> bikes;

			if (SearchText != "" && SearchText != null)
			{
				bikes = await _searchRepository.GetAllBikesByBrand(SearchText);
			}
			else
				bikes = _appDbContext.Bikes.ToList();

			const int PageSize = 12;

			if (pg < 1)
				pg = 1;

			int recsCount = bikes.Count();
			int recSkip = (pg - 1) * PageSize;

			List<Bikes> bikesList = bikes.Skip(recSkip).Take(PageSize).ToList();
			SPager searchPager = new SPager(recsCount, pg, PageSize) { Action = "GetAllBikesByBrand", Controller = "Search", SearchText = SearchText };

			this.ViewBag.SearchPager = searchPager;

			return View(bikesList);
		}
		public async Task<IActionResult> GetAllBikesByPrice(string SearchText="", int pg=1)
		{
			IEnumerable<Bikes> bikes;

			if (SearchText != "" && SearchText != null)
			{
				bikes = await _searchRepository.GetAllBikesByPrice(SearchText);
			}
			else
				bikes = _appDbContext.Bikes.ToList();

			const int PageSize = 12;

			if (pg < 1)
				pg = 1;

			int recsCount = bikes.Count();
			int recSkip = (pg - 1) * PageSize;

			List<Bikes> bikesList = bikes.Skip(recSkip).Take(PageSize).ToList();
			SPager searchPager = new SPager(recsCount, pg, PageSize) { Action = "GetAllBikesByPrice", Controller = "Search", SearchText = SearchText };

			this.ViewBag.SearchPager = searchPager;

			return View(bikesList);
		}
		public async Task<IActionResult> GetAllBikesByYear(string SearchText="",int pg=1)
		{
			IEnumerable<Bikes> bikes;

			if (SearchText != "" && SearchText != null)
			{
				bikes = await _searchRepository.GetAllBikesByYear(SearchText);
			}
			else
				bikes = _appDbContext.Bikes.ToList();

			const int PageSize = 12;

			if (pg < 1)
				pg = 1;

			int recsCount = bikes.Count();
			int recSkip = (pg - 1) * PageSize;

			List<Bikes> bikesList = bikes.Skip(recSkip).Take(PageSize).ToList();
			SPager searchPager = new SPager(recsCount, pg, PageSize) { Action = "GetAllBikesByYear", Controller = "Search", SearchText = SearchText };

			this.ViewBag.SearchPager = searchPager;

			return View(bikesList);
		}


		public async Task<IActionResult> GetAllVansByCity(string SearchText = "", int pg = 1)
		{
			IEnumerable<Vans> vans;

			if (SearchText != "" && SearchText != null)
			{
				vans = await _searchRepository.GetVansByCity(SearchText);
			}
			else
				vans = _appDbContext.Vans.ToList();

			const int PageSize = 12;

			if (pg < 1)
				pg = 1;

			int recsCount = vans.Count();
			int recSkip = (pg - 1) * PageSize;

			List<Vans> vansList = vans.Skip(recSkip).Take(PageSize).ToList();
			SPager searchPager = new SPager(recsCount, pg, PageSize) { Action = "GetAllVansByCity", Controller = "Search", SearchText = SearchText };

			this.ViewBag.SearchPager = searchPager;

			return View(vansList);
		}
		public async Task<IActionResult> GetAllVansByBrand(string SearchText = "", int pg = 1)
		{
			IEnumerable<Vans> vans;

			if (SearchText != "" && SearchText != null)
			{
				vans = await _searchRepository.GetAllVansByBrand(SearchText);
			}
			else
				vans = _appDbContext.Vans.ToList();

			const int PageSize = 12;

			if (pg < 1)
				pg = 1;

			int recsCount = vans.Count();
			int recSkip = (pg - 1) * PageSize;

			List<Vans> vansList = vans.Skip(recSkip).Take(PageSize).ToList();
			SPager searchPager = new SPager(recsCount, pg, PageSize) { Action = "GetAllVansByCity", Controller = "Search", SearchText = SearchText };

			this.ViewBag.SearchPager = searchPager;

			return View(vansList);
		}
		public async Task<IActionResult> GetAllVansByPrice(string SearchText="",int pg=1)
		{
			IEnumerable<Vans> vans;

			if (SearchText != "" && SearchText != null)
			{
				vans = await _searchRepository.GetAllVansByPrice(SearchText);
			}
			else
				vans = _appDbContext.Vans.ToList();

			const int PageSize = 12;

			if (pg < 1)
				pg = 1;

			int recsCount = vans.Count();
			int recSkip = (pg - 1) * PageSize;

			List<Vans> vansList = vans.Skip(recSkip).Take(PageSize).ToList();
			SPager searchPager = new SPager(recsCount, pg, PageSize) { Action = "GetAllVansByPrice", Controller = "Search", SearchText = SearchText };

			this.ViewBag.SearchPager = searchPager;

			return View(vansList);
		}
		public async Task<IActionResult> GetAllVansByYear(string SearchText="", int pg=1)
		{
			IEnumerable<Vans> vans;

			if (SearchText != "" && SearchText != null)
			{
				vans = await _searchRepository.GetAllVansByYear(SearchText);
			}
			else
				vans = _appDbContext.Vans.ToList();

			const int PageSize = 12;

			if (pg < 1)
				pg = 1;

			int recsCount = vans.Count();
			int recSkip = (pg - 1) * PageSize;

			List<Vans> vansList = vans.Skip(recSkip).Take(PageSize).ToList();
			SPager searchPager = new SPager(recsCount, pg, PageSize) { Action = "GetAllVansByYear", Controller = "Search", SearchText = SearchText };

			this.ViewBag.SearchPager = searchPager;

			return View(vansList);
		}
	}
}

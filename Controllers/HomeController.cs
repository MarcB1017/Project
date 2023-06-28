using Microsoft.AspNetCore.Mvc;
using MVC_CarsForSale.Data;
using MVC_CarsForSale.Helpers;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;
using MVC_CarsForSale.Repository;
using MVC_CarsForSale.ViewModels.Home;
using MVC_CarsForSale.ViewModels.Search;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace MVC_CarsForSale.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarsRepository _carsRepository;

        public HomeController(ILogger<HomeController> logger, ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int pg = 1)
        {
            var ipInfo = new IpInfo();
            var homeViewModel = new HomeViewModel();

            try
            {
                string url = "https://ipinfo.io/66.131.196.202?token=ADD-TOKEN-HERE";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRegion = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRegion.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.Country = ipInfo.Region;

                if (homeViewModel.City != null)
                {
                    homeViewModel.Cars = await _carsRepository.GetAllCarsByCity(homeViewModel.City);   
                }
                else
                {
                    homeViewModel.Cars = null;
                }
                return View(homeViewModel);
            }
            catch (Exception)
            {
                homeViewModel.Cars = null;
            }
            return View(homeViewModel);
        }
       

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
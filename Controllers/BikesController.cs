using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;
using MVC_CarsForSale.Repository;
using MVC_CarsForSale.ViewModels.Bikes;
using MVC_CarsForSale.ViewModels.Cars;
using MVC_CarsForSale.ViewModels.Search;

namespace MVC_CarsForSale.Controllers
{
    public class BikesController : Controller
    {
        private readonly IBikesRepository _bikesRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public BikesController(IBikesRepository bikesRepository, IPhotoService photoService, IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _bikesRepository = bikesRepository;
            _photoService = photoService;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> Index(int pg = 1)
        {
            IEnumerable<Bikes> bikes = await _bikesRepository.GetAll();

            const int pageSize = 6;

            if (pg < 1) pg = 1;

            int recsCount = bikes.Count();
            int recSkip = (pg - 1) * pageSize;

            List<Bikes> bikesList = bikes.Skip(recSkip).Take(pageSize).ToList();

            SPager SearchPager = new SPager(recsCount, pg, pageSize) { Action = "Index", Controller = "Bikes" };

            this.ViewBag.SearchPager = SearchPager;

            return View(bikesList);
        }

        [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]
        public async Task<IActionResult> Create()
        {
            var curUserId = _contextAccessor.HttpContext?.User.GetUserId();
            var createBikeVM = new CreateBikeViewModel { AppUserId = curUserId };

            return await Task.Run(() => View(createBikeVM));
        }

        [HttpPost]
        [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]
        public async Task<IActionResult> Create(CreateBikeViewModel bikeVM)
        {

            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(bikeVM.Image);
                var newBike = new Bikes
                {
                    Description = bikeVM.Description,
                    Price = bikeVM.Price,
                    Milage = bikeVM.Milage,
                    BikeBrand = bikeVM.BikeBrand,
                    BikeModel = bikeVM.BikeModel,
                    BikeYear = bikeVM.BikeYear,
                    Image = result.Url.ToString(),
                    AppUserId = bikeVM.AppUserId,
                    Address = new Address
                    {
                        Province = bikeVM.Address.Province,
                        City = bikeVM.Address.City,
                        Country = bikeVM.Address.Country
                    }
                };   

                    _bikesRepository.Add(newBike);

                    return RedirectToAction("Index", "DashBoard");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed.");
            }
            return View(bikeVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            Bikes bike = await _bikesRepository.GetByIdAsync(id);
            return View(bike);
        }

        [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]
        public async Task<IActionResult> Delete(int id)
        {
            var bikeDetails = await _bikesRepository.GetByIdAsync(id);

            if (bikeDetails == null) return View("Error");

            return View(bikeDetails);
        }

        [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteBike(int id)
        {
            var bikeDetails = await _bikesRepository.GetByIdAsync(id);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            //If Not an Admin , User can only delete their OWN Bikes.
            if (!User.IsInRole("admin"))
            {
                if (bikeDetails?.AppUser?.Id != currentUser.Id) { return RedirectToAction(nameof(Index)); }
            }

            if (bikeDetails == null) return View("Error");

            _bikesRepository.Delete(bikeDetails);

            return RedirectToAction("Index", "DashBoard");
        }
    }
}


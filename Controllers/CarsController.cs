using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;
using MVC_CarsForSale.ViewModels.Cars;
using MVC_CarsForSale.ViewModels.Search;

namespace MVC_CarsForSale.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsRepository _carsRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager; 

        public CarsController(ICarsRepository carsRepository, IPhotoService photoService, IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _carsRepository = carsRepository;
            _photoService = photoService;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> Index(int pg = 1)
        {
            IEnumerable<Cars> cars = await _carsRepository.GetAll();

            const int pageSize = 6;

            if (pg < 1) pg = 1;

            int recsCount = cars.Count();
            int recSkip = (pg - 1) * pageSize;

            List<Cars> carsList = cars.Skip(recSkip).Take(pageSize).ToList();

            SPager SearchPager = new SPager(recsCount, pg, pageSize) { Action = "Index", Controller = "Cars" };

            this.ViewBag.SearchPager = SearchPager;

            return View(carsList);
        }

        [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]
        public async Task<IActionResult> Create()
        {
            var curUserId = _contextAccessor.HttpContext?.User.GetUserId();
            var createCarVM = new CreateCarViewModel { AppUserId = curUserId };
            
            return await Task.Run(() => View(createCarVM));
        }

        [HttpPost]
        [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]
        public async Task<IActionResult> Create(CreateCarViewModel carVM)
        {
            
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(carVM.Image);
                
                var newCar = new Cars
                {
                    Description = carVM.Description,
                    Price = carVM.Price,
                    Milage = carVM.Milage,
                    CarModel = carVM.CarModel,
                    CarBrand = carVM.CarBrand,
                    CarYear = carVM.CarYear,
                    Image = result.Url.ToString(),
                    AppUserId = carVM.AppUserId,
                    
                    Address = new Address
                    {
                        Province = carVM.Address.Province,
                        City = carVM.Address.City,
                        Country = carVM.Address.Country
                    }
                };

                _carsRepository.Add(newCar);

                return RedirectToAction("Index", "DashBoard");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed.");
            }
            return View(carVM);
        }


        public async Task<IActionResult> Details(int id)
        {
            Cars cars = await _carsRepository.GetByIdAsync(id);

            return View(cars);
        }

        [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]
        public async Task<IActionResult> Delete(int id)
        {
            var carDetails = await _carsRepository.GetByIdAsync(id);

            if (carDetails == null) return View("Error");

            return View(carDetails);
        }

        [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var carDetails = await _carsRepository.GetByIdAsync(id);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            //If not an Admin , User can only delete their OWN Cars.
            if (!User.IsInRole("admin"))
            {
                if (carDetails?.AppUser?.Id != currentUser.Id) { return RedirectToAction(nameof(Index)); }
            }

            if (carDetails == null) return View("Error");

            _carsRepository.Delete(carDetails);

            return RedirectToAction("Index", "DashBoard");
        }
    }
}

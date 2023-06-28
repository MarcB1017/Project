using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;
using MVC_CarsForSale.Repository;
using MVC_CarsForSale.ViewModels.DashBoard;
using MVC_CarsForSale.ViewModels.Search;

namespace MVC_CarsForSale.Controllers
{
 
    [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]


    public class DashBoardController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDashBoardRepository _dashBoardRepository;
        private readonly ICarsRepository _carRepository;
        private readonly IVansRepository _vansRepository;
        private readonly IBikesRepository _bikesRepository;
        private readonly UserManager<AppUser> _userManager;


        public DashBoardController(UserManager<AppUser> userManager, IPhotoService photoService, IHttpContextAccessor contextAccessor, IDashBoardRepository dashBoardRepository, ICarsRepository carRepository, IVansRepository vansRepository, IBikesRepository bikesRepository)
        {
            _userManager = userManager;
            _photoService = photoService;
            _contextAccessor = contextAccessor;
            _dashBoardRepository = dashBoardRepository;
            _carRepository = carRepository;
            _vansRepository = vansRepository;
            _bikesRepository = bikesRepository;
        }
        private void MapUserEdit(AppUser user, EditUserViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.FirstName = editVM.FirstName;
            user.LastName = editVM.LastName;
            user.ProfilePictureUrl = photoResult.Url.ToString();
            user.City = editVM.City;
            user.Country = editVM.Country;
            user.PhoneNumber = editVM.PhoneNumber;
            user.PhoneNumberConfirmed = true;
        }
        public  async Task<IActionResult> Index()
        {
            var curUserId =  _contextAccessor?.HttpContext?.User.GetUserId();
            var user = await _dashBoardRepository.GetUserById(curUserId);

            if (user == null) return View("Error");

            var editUserViewModel = new EditUserViewModel()
            {
                Id = curUserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureUrl = user.ProfilePictureUrl,
                City = user.City,
                Country = user.Country,
                PhoneNumber = user.PhoneNumber
            };
            if(editUserViewModel.FirstName == null || editUserViewModel.LastName == null)
            {
                return RedirectToAction("EditUserProfile", "DashBoard");
            }
            return View(editUserViewModel);
        }



        public async Task<IActionResult> ViewCars()
        {
            var userCars = await _dashBoardRepository.GetAllUserCars();

            var dashboardViewModel = new DashBoardViewModel()
            { Cars = userCars };

            return View(dashboardViewModel);

        }

        public async Task<IActionResult> ViewBikes()
        {
            var userBikes = await _dashBoardRepository.GetAllUserBikes();

            var dashboardViewModel = new DashBoardViewModel()
            { Bikes = userBikes };

            return View(dashboardViewModel);
        }

        public async Task<IActionResult> ViewVans()
        {
            var userVans = await _dashBoardRepository.GetAllUserVans();

            var dashboardViewModel = new DashBoardViewModel()
            { Vans = userVans };

            return View(dashboardViewModel);
        }

        public async Task<IActionResult> ViewAll()
        {
            var userCars = await _dashBoardRepository.GetAllUserCars();
            var userBikes = await _dashBoardRepository.GetAllUserBikes();
            var userVans = await _dashBoardRepository.GetAllUserVans();

            var dashboardViewModel = new DashBoardViewModel()
            {
                Cars = userCars,
                Bikes = userBikes,
                Vans = userVans
            };

            return View(dashboardViewModel);
        }



        public async Task<IActionResult> CarDetails(int id)
        {
            Cars cars = await _carRepository.GetByIdAsync(id);
            return View(cars);
        }

        public async Task<IActionResult> BikeDetails(int id)
        {
            Bikes bikes = await _bikesRepository.GetByIdAsync(id);
            return View(bikes);
        }

        public async Task<IActionResult> VanDetails( int id)
        {
            Vans vans = await _vansRepository.GetByIdAsync(id);
            return View(vans);
        }


        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _contextAccessor?.HttpContext?.User.GetUserId();
            var user = await _dashBoardRepository.GetUserById(curUserId);

            if (user == null) return View("Error");

            var editUserViewModel = new EditUserViewModel()
            {
                Id = curUserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureUrl = user.ProfilePictureUrl,
                City = user.City,
                Country = user.Country,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = true
            };

            return View(editUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }

            AppUser user = await _dashBoardRepository.GetUserByIdNoTracking(editVM.Id);

            // Allow the EditProfile to the currentUser Only.
            if (editVM.Id != user.Id) { return RedirectToAction(nameof(Index)); }

            // Optimistic Concurrency - "Tracking Error" - Need to use a AsNoTracking();
            if (user.ProfilePictureUrl == "" || user.ProfilePictureUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);

                _dashBoardRepository.Update(user);

                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfilePictureUrl);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _dashBoardRepository.Update(user);

                return RedirectToAction("Index");
            }
        }


        public async Task<IActionResult> EditUserCars(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);

            if (car == null) return View("Error");

            var carM = new EditCarsViewModel
            {
                AppUserId = car.AppUserId,
                Price = car.Price,
                Milage = car.Milage,
                CarBrand = car.CarBrand,
                CarYear = car.CarYear,
                CarModel = car.CarModel,
                Description = car.Description,
                AddressId = car.AddressId,
                Address = car.Address,
                URL = car.Image

            };

            return View(carM);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserCars(EditCarsViewModel carVM, int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit car");
                return View("EditUserCars", carVM);
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var carDetails = await _carRepository.GetByIdAsyncNoTracking(id);

            //Block User From Updating other user Car if not an Admin.
            if (!User.IsInRole("admin")) { if (carVM.AppUserId != currentUser.Id) { return RedirectToAction(nameof(Index)); } }

            if (carDetails != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(carDetails.Image);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(carVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(carVM.Image);
                var car = new Cars
                {
                    Id = id,
                    AppUserId = carVM.AppUserId,
                    Price = carVM.Price,
                    Milage = carVM.Milage,
                    CarBrand = carVM.CarBrand,
                    CarModel = carVM.CarModel,
                    CarYear = carVM.CarYear,
                    Description = carVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = carVM.AddressId,
                    Address = carVM.Address
                };
                _carRepository.Update(car);

                return RedirectToAction("Index");
            }
            else
            {
                return View(carVM);
            }

        }



        public async Task<IActionResult> EditUserBikes(int id)
        {
            var bike = await _bikesRepository.GetByIdAsync(id);

            if (bike == null) return View("Error");

            var bikeM = new EditBikesViewModel
            {
                AppUserId = bike.AppUserId,
                Price = bike.Price,
                Milage = bike.Milage,
                BikeBrand = bike.BikeBrand,
                BikeModel = bike.BikeModel,
                BikeYear = bike.BikeYear,
                Description = bike.Description,
                AddressId = bike.AddressId,
                Address = bike.Address,
                URL = bike.Image

            };

            return View(bikeM);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserBikes(EditBikesViewModel bikeVM, int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit bike");
                return View("EditUser", bikeVM);
            }

            var bikeDetails = await _bikesRepository.GetByIdAsyncNoTracking(id);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);//++

            //Block User From Updating other user Bike if not an Admin.
            if (!User.IsInRole("admin")) { if (bikeVM.AppUserId != currentUser.Id) { return RedirectToAction(nameof(Index)); } }

            if (bikeDetails != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(bikeDetails.Image);
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Could not delete photo");
                    return View(bikeVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(bikeVM.Image);

                var bike = new Bikes
                {
                    Id = id,
                    AppUserId = bikeVM.AppUserId,
                    Price = bikeVM.Price,
                    Milage = bikeVM.Milage,
                    BikeBrand = bikeVM.BikeBrand,
                    BikeModel = bikeVM.BikeModel,
                    BikeYear = bikeVM.BikeYear,
                    Description = bikeVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = bikeVM.AddressId,
                    Address = bikeVM.Address
                };
                _bikesRepository.Update(bike);

                return RedirectToAction("Index");
            }
            else
            {
                return View(bikeVM);
            }
        }



        public async Task<IActionResult> EditUserVans(int id)
        {
            var van = await _vansRepository.GetByIdAsync(id);

            if (van == null) return View("Error");

            var vanM = new EditVansViewModel
            {
                AppUserId = van.AppUserId,
                Price = van.Price,
                Milage = van.Milage,
                VanBrand = van.VanBrand,
                VanModel = van.VanModel,
                VanYear = van.VanYear,
                Description = van.Description,
                AddressId = van.AddressId,
                Address = van.Address,
                URL = van.Image

            };

            return View(vanM);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserVans(EditVansViewModel vanVM, int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Van");
                return View("EditUserCars", vanVM);
            }
            var vanDetails = await _vansRepository.GetByIdAsyncNoTracking(id);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            //Block User From Updating other user Van if not an Admin.
            if (!User.IsInRole("admin")) { if (vanVM.AppUserId != currentUser.Id) { return RedirectToAction(nameof(Index)); } }

            if (vanDetails != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(vanDetails.Image);
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Could not delete photo");
                    return View(vanVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(vanVM.Image);

                var van = new Vans
                {
                    Id = id,
                    AppUserId = vanVM.AppUserId,
                    Price = vanVM.Price,
                    Milage = vanVM.Milage,
                    VanBrand = vanVM.VanBrand,
                    VanModel = vanVM.VanModel,
                    VanYear = vanVM.VanYear,
                    Description = vanVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = vanVM.AddressId,
                    Address = vanVM.Address
                };
                _vansRepository.Update(van);

                return RedirectToAction("Index");
            }
            else
            {
                return View(vanVM);
            }
        }



        public async Task<IActionResult> ViewUserProfile()
        {
            var curUserId = _contextAccessor?.HttpContext?.User.GetUserId();
            var user = await _dashBoardRepository.GetUserById(curUserId);

            if (user == null) return View("Error");

            var editUserViewModel = new EditUserViewModel()
            {
                Id = curUserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureUrl = user.ProfilePictureUrl,
                City = user.City,
                Country = user.Country,
                PhoneNumber = user.PhoneNumber
            };

            return View(editUserViewModel);
        }
    }
}

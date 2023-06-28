using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;
using MVC_CarsForSale.Repository;
using MVC_CarsForSale.ViewModels.Cars;
using MVC_CarsForSale.ViewModels.Search;
using MVC_CarsForSale.ViewModels.Vans;

namespace MVC_CarsForSale.Controllers
{
    public class VansController : Controller
    {
        private readonly IVansRepository _vansRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;
        public VansController(IVansRepository vansRepository, IPhotoService photoService, IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _vansRepository = vansRepository;
            _photoService = photoService;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> Index(int pg = 1)
        {
            IEnumerable<Vans> vans = await _vansRepository.GetAll();

            const int pageSize = 6;
            if (pg < 1) pg = 1;

            int recsCount = vans.Count();
            int recSkip = (pg - 1) * pageSize;

            List<Vans> vansList = vans.Skip(recSkip).Take(pageSize).ToList();
            SPager SearchPager = new SPager(recsCount, pg, pageSize) { Action = "Index", Controller = "Vans" };
            this.ViewBag.SearchPager = SearchPager;

            return View(vansList);
        }

        [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]
        public async Task<IActionResult> Create()
        {
            var curUserId = _contextAccessor.HttpContext?.User.GetUserId();
            var createVanVM = new CreateVanViewModel { AppUserId = curUserId };

            return await Task.Run(() => View(createVanVM));
        }

        [HttpPost]
        [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]
        public async Task<IActionResult> Create(CreateVanViewModel vanVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(vanVM.Image);

                var newVan = new Vans
                {
                    Description = vanVM.Description,
                    Price = vanVM.Price,
                    Milage = vanVM.Milage,
                    VanBrand = vanVM.VanBrand,
                    VanModel = vanVM.VanModel,
                    VanYear = vanVM.VanYear,
                    Image = result.Url.ToString(),
                    AppUserId = vanVM.AppUserId,
                    Address = new Address
                    {
                        Province = vanVM.Address.Province,
                        City = vanVM.Address.City,
                        Country = vanVM.Address.Country
                    }

                };

                _vansRepository.Add(newVan);

                return RedirectToAction("Index", "DashBoard");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed.");
            }
            return View(vanVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            Vans vans = await _vansRepository.GetByIdAsync(id);
            return View(vans);
        }


        [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]
        public async Task<IActionResult> Delete(int id)
        {
            var vanDetails = await _vansRepository.GetByIdAsync(id);

            if (vanDetails == null) return View("Error");

            return View(vanDetails);
        }


        [Authorize(Roles = Data.UserRoles.Admin + "," + Data.UserRoles.User)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteVan(int id)
        {
            var vanDetails = await _vansRepository.GetByIdAsync(id);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            //If not an admin, User can only delete their OWN Vans.
            if (!User.IsInRole("admin"))
            {
                if (vanDetails?.AppUser?.Id != currentUser.Id) { return RedirectToAction(nameof(Index)); }
            }

            if (vanDetails == null) return View("Error");

            _vansRepository.Delete(vanDetails);

            return RedirectToAction("Index", "DashBoard");
        }
    }
}

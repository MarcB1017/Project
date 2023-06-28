using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_CarsForSale.Interfaces;
using MVC_CarsForSale.Models;
using MVC_CarsForSale.ViewModels.Users;
using EditUserViewModel = MVC_CarsForSale.ViewModels.Users.EditUserViewModel;

namespace MVC_CarsForSale.Controllers
{
    //UserController For Admin Only.
    [Authorize(Roles = Data.UserRoles.Admin)]

    public class UserController : Controller
    {

        private readonly IPhotoService _photoService;
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository, IPhotoService photoService)
        {
            _photoService = photoService;
            _userRepository = userRepository;
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
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();

            List<UserViewModel> result = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userVM = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ProfilePictureUrl = user.ProfilePictureUrl
                };
                result.Add(userVM);
            }

            return View(result);
        }
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userRepository.GetUserById(id);

            var userDetailsVM = new UserDetailsViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureUrl = user.ProfilePictureUrl,
                PhoneNumber = user.PhoneNumber

            };

            return View(userDetailsVM);
        }
        public async Task<IActionResult> EditUserProfile(string id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null) return View("Error");

            var editUserVM = new EditUserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureUrl = user.ProfilePictureUrl,
                City = user.City,
                Country = user.Country,
                PhoneNumber = user.PhoneNumber
            };

            return View(editUserVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }

            AppUser user = await _userRepository.GetUserByIdNoTracking(editVM.Id);
            // Optimistic Concurrency - "Tracking Error" - Need to use a AsNoTracking();

            if (user.ProfilePictureUrl == "" || user.ProfilePictureUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);

                _userRepository.Update(user);
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
                _userRepository.Update(user);

                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> DeleteUserProfile(string id)
        {
            var userDetails = await _userRepository.GetUserByIdAsync(id);

            if (userDetails == null) return View("Error");

            return View(userDetails);
        }
        [HttpPost, ActionName("DeleteUserProfile")]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var userDetails = await _userRepository.GetUserByIdAsync(id);

            if (userDetails == null) return View("Error");

            _userRepository.Delete(userDetails);

            return RedirectToAction("Index");
        }
    }
}

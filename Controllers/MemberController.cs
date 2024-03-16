using AspNetCoreIdentityApp.Web.Extensions;
using AspNetCoreIdentityApp.Web.Models;
using AspNetCoreIdentityApp.Web.Services.Abstract;
using AspNetCoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace AspNetCoreIdentityApp.Web.Controllers
{

    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IFileProvider _fileProvider;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IEmailService emailService, IFileProvider fileProvider)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _fileProvider = fileProvider;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);


            var userViewModel = new UserViewModel
            {
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                UserName = currentUser!.UserName,
                Email = currentUser!.Email,
                PhoneNumber = currentUser!.PhoneNumber,
                EmailConfirmed = currentUser!.EmailConfirmed,
                TwoFactorEnabled = currentUser!.TwoFactorEnabled
            };
            ViewData["PictureUrl"] = currentUser.Picture;

            return View(userViewModel);
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult ChangePassword()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel request)
        {


            if (!ModelState.IsValid)
            {

                return View();
            }

            var currentUser = (await _userManager.FindByNameAsync(User.Identity!.Name!))!;

            var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser, request.OldPassword);

            if (!checkOldPassword)
            {
                ModelState.AddModelError(string.Empty, "Eski şifreniz yanlış");

                return View();
            }

            var resultChangePassword = await _userManager.ChangePasswordAsync(currentUser, request.OldPassword, request.NewPassword);

            if (!resultChangePassword.Succeeded)
            {

                ModelState.AddModelErrorList(resultChangePassword.Errors);
                return View();
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, request.NewPassword, true, false);

            TempData["SuccessMessage"] = true;

            //Send an email 
            await _emailService.SendResetPasswordIsSuccessfulAsync(currentUser.UserName!, currentUser.Email!);

            return View();
        }



        public async Task<IActionResult> EditUser()
        {
            var currentUser = await _userManager.FindByNameAsync(User?.Identity?.Name!);

            var editUserViewModel = new EditUserViewModel()
            {
                UserName = currentUser!.UserName!,
                Email = currentUser!.Email!,
                Phone = currentUser!.PhoneNumber!,
                FirstName = currentUser!.FirstName,
                LastName = currentUser!.LastName
            };

            ViewData["PictureUrl"] = currentUser.Picture;

            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);

            currentUser!.UserName = request.UserName;
            currentUser.Email = request.Email;
            currentUser.PhoneNumber = request.Phone;
            currentUser.FirstName = request.FirstName;
            currentUser.LastName = request.LastName;


            if (request.File != null && request.File.Length > 0)
            {
                var wwwrootFolder = _fileProvider.GetDirectoryContents("wwwroot//uploads");

                var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(request.File.FileName)}";
                //jpg,png vs
                var newPicturePath =
                    Path.Combine(wwwrootFolder.First(x => x.Name == "userPictures").PhysicalPath!, randomFileName);

                using var stream = new FileStream(newPicturePath, FileMode.Create);

                await request.File.CopyToAsync(stream);

                currentUser.Picture = randomFileName;
            }

            var updatedUserResult = await _userManager.UpdateAsync(currentUser);

            if (!updatedUserResult.Succeeded)
            {
                ModelState.AddModelErrorList(updatedUserResult.Errors);

                ViewData["PictureUrl"] = currentUser.Picture;

                return View();
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);

            await _signInManager.SignOutAsync();

            await _signInManager.SignInAsync(currentUser, true);

            TempData["SuccessMessage"] = "Kullanıcı bilgileri güncellendi";

            return RedirectToAction("EditUser", "Member");
        }






    }
}

using AspNetCoreIdentityApp.Web.Areas.Admin.Models;
using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> UserList()
        {
            var userList = await _userManager.Users.ToListAsync();

            var userViewModelList = userList.Select(x => new UserViewModel
            {
                Id = x.Id,
                UserPicture = x.Picture,
                Username = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                FullName = x.FullName,
                IsEmailConfirmed = x.EmailConfirmed,
                IsTwoFactorEnabled = x.TwoFactorEnabled

            }).ToList();

            return View(userViewModelList);
        }


        public async Task<IActionResult> AdminList()
        {
            var adminList = await _userManager.GetUsersInRoleAsync("Admin");

            var userViewModelList = adminList.Select(x => new UserViewModel
            {
                Id = x.Id,
                UserPicture = x.Picture,
                Username = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                FullName = x.FullName,
                IsEmailConfirmed = x.EmailConfirmed,
                IsTwoFactorEnabled = x.TwoFactorEnabled

            }).ToList();

            return View(userViewModelList);
        }
    }
}

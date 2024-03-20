using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.Web.Controllers
{
    public class AccountsController : BaseController
    {


        private readonly UserManager<AppUser> _userManager;

        public AccountsController(UserManager<AppUser> userManager) : base(userManager)
        {
            _userManager = userManager;
        }


        public async Task<IActionResult> AccessDenied(string returnUrl)
        {
            await GetUserPicture();
            return View();
        }
    }
}

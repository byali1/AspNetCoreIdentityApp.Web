using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public BaseController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        protected async Task GetUserPicture()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewData["PictureUrl"] = currentUser.Picture;
            }
        }

    }
}

using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AspNetCoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Controllers
{
    public class HomeController : Controller
    {
        //Identity
        private readonly UserManager<AppUser> _userManager;
        //private readonly RoleManager<AppRole> _roleManager;
        //private readonly SignInManager<> ;


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {



            if (!ModelState.IsValid)
            {
                return View();
            }

            var identityResult = await _userManager.CreateAsync(new AppUser
            {
                UserName = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            }, request.PasswordConfirmed);

            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Kayıt işlemi başarıyla gerçekleştirildi.";
                return RedirectToAction(nameof(HomeController.SignUp));
            }

            foreach (var item in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
                ViewBag.ErrorMessage = item.Description;
            }
            return View();


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
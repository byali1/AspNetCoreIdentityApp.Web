using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AspNetCoreIdentityApp.Web.Extensions;
using AspNetCoreIdentityApp.Web.Services.Abstract;
using AspNetCoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Controllers
{
    public class HomeController : Controller
    {
        //Identity
        private readonly UserManager<AppUser> _userManager;
        //private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel request, string? returnUrl = null)
        {
            // ?? -> null ise
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            var userResult = await _userManager.FindByEmailAsync(request.Email);

            if (userResult == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya şifre yanlış");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(userResult, request.Password, request.RememberMe, true);

            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl);
            }

            if (signInResult.IsLockedOut)
            {
                var serverDateTime = DateTime.UtcNow;
                var lockoutEndDateTimeOffset = await _userManager.GetLockoutEndDateAsync(userResult);

                var lockoutEndDateUtc = lockoutEndDateTimeOffset.Value.UtcDateTime;
                var result = Math.Abs(Convert.ToInt16((serverDateTime - lockoutEndDateUtc).TotalSeconds));

                ModelState.AddModelErrorList(new List<string>() { $"{result} saniye boyunca giriş yapamazsınız." });
                return View();
            }

            ModelState.AddModelErrorList(new List<string>() { "Email veya şifre yanlış!" });

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

        public IActionResult ForgotMyPassword()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotMyPassword(ForgotMyPasswordViewModel request)
        {
            // http://localhost:5185?userId=1=&token=aaaaaaaaaaaa

            bool userFound = false;

            var hasUser = await _userManager.FindByEmailAsync(request.Email);

            if (hasUser == null)
            {
                TempData["UserFound"] = userFound;
                ModelState.AddModelError(string.Empty, "Bu e-mail ile bir kullanıcı bulunamadı.");
                return View();
            }

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);

            var passwordResetLink =
                Url.Action("ResetPassword", "Home", new { userId = hasUser.Id, token = passwordResetToken }, HttpContext.Request.Scheme);

            await _emailService.SendResetPasswordEmailAsync(passwordResetLink, hasUser.Email);

            userFound = true;
            TempData["UserFound"] = userFound;

            return RedirectToAction(nameof(ForgotMyPassword));

        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
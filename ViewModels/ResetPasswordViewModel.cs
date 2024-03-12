using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class ResetPasswordViewModel
    {
        public ResetPasswordViewModel()
        {

        }

        public ResetPasswordViewModel(string password, string passwordConfirmed)
        {
            Password = password;
            PasswordConfirmed = passwordConfirmed;
        }


        [Compare(nameof(PasswordConfirmed), ErrorMessage = "Şifreler uyuşmuyor.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Yeni şifre:")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor.")]

        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Şifre tekrar:")]
        public string PasswordConfirmed { get; set; }
    }
}

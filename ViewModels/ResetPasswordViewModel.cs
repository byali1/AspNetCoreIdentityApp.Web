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

        [DataType(DataType.Password)]
        [Compare(nameof(PasswordConfirmed), ErrorMessage = "Şifreler uyuşmuyor.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Yeni şifre:")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Şifre tekrar:")]
        public string PasswordConfirmed { get; set; }
    }
}

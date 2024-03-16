using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Eski şifre:")]
        public string OldPassword { get; set; }


        [DataType(DataType.Password)]

        [Compare(nameof(ConfirmPassword), ErrorMessage = "Şifreler uyuşmuyor.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Yeni şifre:")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Şifreler uyuşmuyor.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Yeni şifre tekrar:")]
        public string ConfirmPassword { get; set; }






    }
}

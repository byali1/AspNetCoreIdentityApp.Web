using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class ForgotMyPasswordViewModel
    {

        public ForgotMyPasswordViewModel()
        {

        }

        public ForgotMyPasswordViewModel(string email)
        {
            Email = email;
        }

        [EmailAddress(ErrorMessage = "E-mail adresiniz doğru formatta değil.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

    }
}

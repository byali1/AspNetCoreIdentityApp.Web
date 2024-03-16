using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        {

        }
        public SignUpViewModel(string username, string email, string phoneNumber, string password, string passwordConfirmed)
        {
            Username = username;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            PasswordConfirmed = passwordConfirmed;
        }

        [MinLength(2, ErrorMessage = "Bu alan 2 karakterden az olamaz.")]
        [MaxLength(15, ErrorMessage = "Bu alan 15 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Kullanıcı adı:")]
        public string Username { get; set; }

        /*

        [MinLength(2, ErrorMessage = "Bu alan 2 karakterden az olamaz.")]
        [MaxLength(25, ErrorMessage = "Bu alan 25 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "İsim:")]
        public string FirstName { get; set; }

        [MinLength(2, ErrorMessage = "Bu alan 2 karakterden az olamaz.")]
        [MaxLength(25, ErrorMessage = "Bu alan 25 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Soy isim:")]
        public string LastName { get; set; } 

        */

        [EmailAddress(ErrorMessage = "E-mail adresiniz doğru formatta değil.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Email:")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Telefon:")]
        public string PhoneNumber { get; set; }

        [Compare(nameof(PasswordConfirmed), ErrorMessage = "Şifreler uyuşmuyor.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Şifre:")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor.")]

        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Şifre Tekrar:")]
        public string PasswordConfirmed { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class EditUserViewModel
    {
        [MinLength(2, ErrorMessage = "Bu alan 2 karakterden az olamaz.")]
        [MaxLength(25, ErrorMessage = "Bu alan 25 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "İsim:")]
        public string FirstName { get; set; } = null!;

        [MinLength(2, ErrorMessage = "Bu alan 2 karakterden az olamaz.")]
        [MaxLength(25, ErrorMessage = "Bu alan 25 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Soyisim:")]
        public string LastName { get; set; } = null!;


        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email formatı yanlıştır.")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [Display(Name = "Email :")]
        public string Email { get; set; } = null!;

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçersiz telefon numarası formatı.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Bu alan 10 karakter uzunluğunda olmalıdır.")]
        [Required(ErrorMessage = "Bu kısım boş olamaz.")]
        [Display(Name = "Telefon:")]
        public string Phone { get; set; } = null!;

        [Display(Name = "Profil resmi :")]
        public IFormFile? File { get; set; }

    }
}

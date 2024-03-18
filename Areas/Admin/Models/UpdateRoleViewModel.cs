using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Models
{
    public class UpdateRoleViewModel
    {
        public string Id { get; set; } = null!;



        [MinLength(2, ErrorMessage = "Bu alan 2 karakterden az olamaz.")]
        [MaxLength(25, ErrorMessage = "Bu alan 25 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [Display(Name = "Rol adı:")]
        public string Name { get; set; } = null!;
    }
}

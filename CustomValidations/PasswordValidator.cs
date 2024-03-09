using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError
                { Code = "PasswordContainsUsername", Description = "Şifre kullanıcı adınızı içeremez." });
            }

            if (password.ToLower().Contains("1234"))
            {
                errors.Add(new IdentityError
                { Code = "PasswordContains1234", Description = "Şifre ardışık sayılar içeremez." });
            }


            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            //İçine verilen tipi task class'ı ile wraplar
            return Task.FromResult(IdentityResult.Success);
        }
    }
}

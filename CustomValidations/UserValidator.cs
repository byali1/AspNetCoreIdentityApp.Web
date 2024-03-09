using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.CustomValidations
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();

            var isNumber = int.TryParse(user.UserName[0]!.ToString(), out _);

            if (isNumber)
            {
                errors.Add(new() { Code = "UsernameStartsWithNumber", Description = "Kullanıcı adı sayı ile başlayamaz." });
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

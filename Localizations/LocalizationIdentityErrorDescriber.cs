using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Localizations
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new()
            { Code = "DuplicateUserName", Description = $"{userName} kullanıcı adında bir üye zaten mevcut." };
        }


        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DuplicateEmail", Description = "Bu mail hesabı ile bir üye zaten mevcut." };

        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "PasswordTooShort", Description = "Şifreniz en az 12 karakter olmalıdır." };

        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new() { Code = "PasswordRequiresDigit", Description = "Şifreniz en az bir tane rakam içermelidir." };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new() { Code = "PasswordRequiresNonAlphanumeric", Description = "Şifreniz en az bir tane alfanumerik olmayan karakter içermelidir.[@?*!&/#] vb." };
        }
    }
}

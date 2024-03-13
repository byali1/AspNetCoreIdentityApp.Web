namespace AspNetCoreIdentityApp.Web.Services.Abstract
{
    public interface IEmailService
    {
        Task SendResetPasswordEmailAsync(string resetEmailLink, string toEmail, string userName);
        Task SendResetPasswordIsSuccessfulAsync(string userName, string toEmail);
    }
}

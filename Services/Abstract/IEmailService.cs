namespace AspNetCoreIdentityApp.Web.Services.Abstract
{
    public interface IEmailService
    {
        Task SendResetPasswordEmailAsync(string resetEmailLink, string toEmail);
    }
}

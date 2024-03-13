using System.Net;
using System.Net.Mail;
using AspNetCoreIdentityApp.Web.OptionsModels;
using AspNetCoreIdentityApp.Web.Services.Abstract;
using Microsoft.Extensions.Options;

namespace AspNetCoreIdentityApp.Web.Services.Concrete
{

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendResetPasswordEmailAsync(string resetPasswordEmailLink, string toEmail, string userName)
        {
            var smptClient = new SmtpClient();

            smptClient.Host = _emailSettings.Host;
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smptClient.EnableSsl = true;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(toEmail);

            mailMessage.Subject = "IdentityApp | Şifre Sıfırlama";
            mailMessage.Body = $@"
<html>
<head>
    <style>
        body {{ font-family: 'Arial', sans-serif; }}
        .container {{
            width: 100%; 
            max-width: 600px; 
            margin: 0 auto; 
            background-color: #f7f7f7; 
            padding: 20px; 
            text-align: center;
        }}
        .button {{
            display: block;
            width: 200px; 
            margin: 20px auto; 
            padding: 10px; 
            background-color: #000000;
            color: #ffffff; 
            text-decoration: none; 
            border-radius: 5px;
            text-align: center;
        }}
        .footer {{
            margin-top: 20px; 
            font-size: 0.8em; 
            color: #777777; 
            text-align: center;
        }}

#resetButton{{
            text-decoration: none;
            cursor: pointer;
            color: #ffffff;

        }}
    </style>
</head>
<body>
    <div class='container'>
        <h2>Şifreniz Değiştirildi!</h2>
        <p>Merhaba {userName},</p>
        <p>Hesabınız için bir şifre sıfırlama talebinde bulunuldu. Eğer bu işlemi siz başlatmadıysanız, lütfen bu e-postayı dikkate almayın.</p>
        <table cellpadding='0' cellspacing='0' width='100%' border='0'>
            <tr>
                <td>
                    <a href='{resetPasswordEmailLink}' id='resetButton' class='button'>Şifre Sıfırla</a>
                </td>
            </tr>
        </table>
        <p>Ya da aşağıdaki bağlantıyı tarayıcınızda açın:</p>
        <div><a href='{resetPasswordEmailLink}'>{resetPasswordEmailLink}</a></div>
        <p class='footer'>Bu bağlantı sadece 15 dakika geçerlidir.</p>
    </div>
</body>
</html>";

            mailMessage.IsBodyHtml = true;
            smptClient.SendMailAsync(mailMessage);
        }


        public async Task SendResetPasswordIsSuccessfulAsync(string userName, string toEmail)
        {
            var smptClient = new SmtpClient();

            smptClient.Host = _emailSettings.Host;
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smptClient.EnableSsl = true;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(toEmail);

            mailMessage.Subject = "IdentityApp | Şifreniz Değiştirildi";
            mailMessage.Body = $@"
<html>
<head>
    <style>
        body {{ font-family: 'Arial', sans-serif; }}
        .container {{
            width: 100%; 
            max-width: 600px; 
            margin: 0 auto; 
            background-color: #f7f7f7; 
            padding: 20px; 
            text-align: center;
        }}
        .button {{
            display: block;
            width: 200px; 
            margin: 20px auto; 
            padding: 10px; 
            background-color: #000000;
            color: #ffffff; 
            text-decoration: none; 
            border-radius: 5px;
            text-align: center;
        }}
        .footer {{
            margin-top: 20px; 
            font-size: 0.8em; 
            color: #777777; 
            text-align: center;
        }}

#resetButton{{
            text-decoration: none;
            cursor: pointer;
            color: #ffffff;

        }}
    </style>
</head>
<body>
    <div class='container'>
        <h2>Şifreniz Başarıyla Değiştirildi</h2>
        <p>Merhaba {userName},</p>
        <p>IdentityApp hesabınızın şifresi başarıyla değiştirildi.</p>
        
        <p class='footer'>Bu e-posta bilgilendirme amaçlı gönderilmiştir. Lütfen yanıtlamayınız.</p>
    </div>
</body>
</html>";

            mailMessage.IsBodyHtml = true;
            smptClient.SendMailAsync(mailMessage);
        }
    }
}

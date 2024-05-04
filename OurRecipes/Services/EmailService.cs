
using Humanizer;
using MimeKit;
using OurRecipes.Models;
using SendGrid.Helpers.Mail.Model;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;
using System.Net.Mail;
namespace OurRecipes.Services
{
  

    public class EmailService
    {
        public static string GenerateEmailVerificationToken()
        {
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }
        public static async Task SendEmailAsync(string to, string subject, string htmlMessage)
        {
            string fromMail = "deaaaldeen45112@gmail.com";
            string fromPassword = "jjjagamtpjcuekoi";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Test Subject";
            message.To.Add(new MailAddress("deaaaldeen45112@gmail.com"));
            message.Body = htmlMessage;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

           await smtpClient.SendMailAsync(message);
        }
    }


}

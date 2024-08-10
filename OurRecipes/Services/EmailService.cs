
using Humanizer;
using MimeKit;
using OurRecipes.Models;
using SendGrid.Helpers.Mail.Model;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;
using System.Net.Mail;
using Org.BouncyCastle.Utilities.Net;

using Attachment = System.Net.Mail.Attachment;

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
            string fromMail = "discoverourrecipes@gmail.com";
            string fromPassword = "afiochmpiialibhs";


            using (var smtpClient = new SmtpClient("smtp.gmail.com"))
            {

                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(fromMail, fromPassword);
                smtpClient.EnableSsl = true;


                using (var message = new MailMessage())
                {

                    message.From = new MailAddress(fromMail);
                    message.Subject = subject;
                    message.To.Add(new MailAddress(to));
                    message.Body = htmlMessage;
                    message.IsBodyHtml = true;

                    await smtpClient.SendMailAsync(message);
                }



            }


        }
        public static async Task SendEmailWithAttachment(Stream attachmentStream, string to, string subject, string body,string name)
        {




            string fromMail = "discoverourrecipes@gmail.com";
            string fromPassword = "afiochmpiialibhs";


            using (var smtpClient = new SmtpClient("smtp.gmail.com"))
            {

                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(fromMail, fromPassword);
                smtpClient.EnableSsl = true;


                using (var message = new MailMessage())
                {

                    message.From = new MailAddress(fromMail);
                    message.Subject = subject;
                    message.To.Add(new MailAddress(to));
                    message.Body = body;
                    message.IsBodyHtml = true;
                    message.Attachments.Add(new Attachment(attachmentStream, name, "application/pdf"));
                    await smtpClient.SendMailAsync(message);
                }



            }


        }



     
    }
}

//MailMessage message = new MailMessage();
//message.From = new MailAddress(fromMail);
//message.Subject = subject;
//message.To.Add(new MailAddress(to));
//message.Body = htmlMessage;
//message.IsBodyHtml = true;

//var smtpClient = new SmtpClient("smtp.gmail.com")
//{
//    Port = 587,
//    Credentials = new NetworkCredential(fromMail, fromPassword),
//    EnableSsl = true,
//};

//await smtpClient.SendMailAsync(message);
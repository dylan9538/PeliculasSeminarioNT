using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotorDePeliculas.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {

            var myMessage = new SendGrid.SendGridMessage();
            myMessage.AddTo(email);
            myMessage.From = new System.Net.Mail.MailAddress("glondono@gmail.com", "Guillermo Londoño");
            myMessage.Subject = subject;
            myMessage.Text = message;
            myMessage.Html = message;

            var credentials = new System.Net.NetworkCredential("glondono01","Icesi@123");

            var transport = new SendGrid.Web(credentials);
            return transport.DeliverAsync(myMessage);


            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}

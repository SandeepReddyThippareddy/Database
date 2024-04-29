using System.Net.Mail;
using System.Net;
using EmailHandler.Interfaces;
using EmailHandler.Utilities.Enums;
using EmailHandler.Utilities;
using EmailHandler.Models;

namespace EmailHandler.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(string email, string subject, Mailtype mailtype, Event Event)
        {
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("adityareddy2001@hotmail.com", "Aditya@2001")
            };
            string message = string.Empty;

            if(mailtype == Mailtype.UserRegistration)
            {
                message = MailMessageHandler.GetHtmlTemplateForUserregistration(Event);
            }

            if (mailtype == Mailtype.EventRegistration)
            {
                message = MailMessageHandler.GetHtmlTemplateForEventRegistration(Event);
            }

            if (mailtype == Mailtype.EventRegistrationCancellation)
            {
                message = MailMessageHandler.GetHtmlTemplateForEventRegistrationCancellation(Event);
            }

            if (mailtype == Mailtype.EventEnrollment)
            {
                message = MailMessageHandler.GetHtmlTemplateForEventEnrollment(Event);
            }

            MailMessage mail = new MailMessage
            {
                From = new MailAddress("adityareddy2001@hotmail.com")
            };
            mail.To.Add(email);
            mail.Subject = subject;

            string htmlBody = message;

            mail.Body = htmlBody;
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
        }
    }
}

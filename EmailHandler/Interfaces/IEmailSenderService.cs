using EmailHandler.Models;
using EmailHandler.Utilities.Enums;

namespace EmailHandler.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, Mailtype mailtype, Event Event);
    }
}

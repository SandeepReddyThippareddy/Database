using EmailHandler.Interfaces;
using EmailHandler.Models;
using EmailHandler.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EmailHandler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IEmailSenderService emailSender;

        public HomeController(IEmailSenderService emailSender)
        {
            this.emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string subject, Mailtype mailtype, Event Event)
        {
            try
            {
                await emailSender.SendEmailAsync(email, subject, mailtype, Event);
                return Ok(string.Concat("Mail sent to", email));
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Unable to send an email to - ", email,ex.Message));
            }
        }
    }
}
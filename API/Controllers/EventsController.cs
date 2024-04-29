using Application.Events;
using Domain;
using EmailHandler.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class EventsController : BaseApiController
    {
        private readonly IEmailSenderService _emailSenderService;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public EventsController(IEmailSenderService emailSenderService, IHttpContextAccessor httpContextAccessor)
        {
            _emailSenderService = emailSenderService;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] EventParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event Event)
        {
            try
            {
                var uriBuilder = new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port ?? -1);
                if (uriBuilder.Uri.IsDefaultPort)
                {
                    uriBuilder.Port = -1;
                }

                var currentEvent = new EmailHandler.Models.Event()
                {
                    EventDate = Event.Date,
                    EventLocation = Event.Venue,
                    EventName = Event.Title,
                    RedirectUrl = string.Format("{0}events/{1}", uriBuilder.Uri.AbsoluteUri, Event.Id),
                    UserName = User.Identity.Name
                };

                var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

                await _emailSenderService.SendEmailAsync(email, string.Format("New Event -{0}- Registration - Success", Event.Title), EmailHandler.Utilities.Enums.Mailtype.EventEnrollment, currentEvent);

                return HandleResult(await Mediator.Send(new Create.Command { Event = Event }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NoContent();
            }
        }

        [Authorize(Policy = "IsEventHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, Event Event)
        {
            Event.Id = id;

            var uriBuilder = new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }

            var currentEvent = new EmailHandler.Models.Event()
            {
                EventDate = Event.Date,
                EventLocation = Event.Venue,
                EventName = Event.Title,
                RedirectUrl = string.Format("{0}events/{1}", uriBuilder.Uri.AbsoluteUri, Event.Id),
                UserName = User.Identity.Name
            };

            var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

            await _emailSenderService.SendEmailAsync(email, string.Format("Event Updation -{0}- Success", Event.Title), EmailHandler.Utilities.Enums.Mailtype.EventEnrollment, currentEvent);



            return HandleResult(await Mediator.Send(new Edit.Command { Event = Event }));
        }

        [Authorize(Policy = "IsEventHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            var Event = await Mediator.Send(new Details.Query { Id = id });

            var uriBuilder = new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }

            var currentEvent = new EmailHandler.Models.Event()
            {
                EventDate = Event.Value.Date,
                EventLocation = Event.Value.Venue,
                EventName = Event.Value.Title,
                UserName = User.Identity.Name
            };
            var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

            if (Event.Value.Attendees.Where(x => x.Username.ToLower() == User.Identity.Name.ToLower()).Count() > 0)
            {
                currentEvent.RedirectUrl = string.Format("{0}events", uriBuilder.Uri.AbsoluteUri);

                await _emailSenderService.SendEmailAsync(email, string.Format("Event -{0}- De-Registration - Success", Event.Value.Title), EmailHandler.Utilities.Enums.Mailtype.EventRegistrationCancellation, currentEvent);
            }
            else
            {
                currentEvent.RedirectUrl = string.Format("{0}events/{1}", uriBuilder.Uri.AbsoluteUri, Event.Value.Id);

                await _emailSenderService.SendEmailAsync(email, string.Format("Event -{0}- Registration - Success", Event.Value.Title), EmailHandler.Utilities.Enums.Mailtype.EventRegistration, currentEvent);
            }

            return HandleResult(await Mediator.Send(new UpdateAttendance.Command { Id = id }));
        }
    }
}
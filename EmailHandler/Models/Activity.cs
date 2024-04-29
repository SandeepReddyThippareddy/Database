namespace EmailHandler.Models
{
    public class Event
    {
        public string UserName { get; set; }

        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public string EventLocation { get; set; }

        public string RedirectUrl { get; set; }
    }
}

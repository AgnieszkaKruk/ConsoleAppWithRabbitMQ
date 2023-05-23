

namespace AppWithRabbitmq.Producent
{
    [Serializable]
    public class EMailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string? EmailProvider { get; set; }

        public EMailMessage(string from, string to, string subject, string body, string? emailProvider)
        {
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            EmailProvider = emailProvider;
        }
    }

}

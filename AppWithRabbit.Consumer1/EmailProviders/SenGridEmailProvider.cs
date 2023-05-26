using AppWithRabbitmq.Producent;

namespace AppWithRabbitmq.Consumer.SendingEmailProviders
{
    public class SenGridEmailProvider : IEmailProvider
    {
        public void SendEmailWithProvider(EMailMessage emailMessage)
        {
            string apiKey = "your_sendgrid_api_key";
            string apiUrl = "https://api.sendgrid.com/v3/mail/send";

            PrepareAndSendEmailWithProvidersApi.PrepareAndSendingEmail(emailMessage, apiKey, apiUrl);
        }
    }
}

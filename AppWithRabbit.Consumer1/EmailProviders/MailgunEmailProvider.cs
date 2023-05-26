using AppWithRabbitmq.Producent;

namespace AppWithRabbitmq.Consumer.SendingEmailProviders
{
    public class MailgunEmailProvider : IEmailProvider
    {
        public void SendEmailWithProvider(EMailMessage emailMessage)
        {

            string apiKey = "your_mailgun_api_key";
            string apiUrl = "https://api.mailgun.net/v3/your_domain.com/messages";

            PrepareAndSendEmailWithProvidersApi.PrepareAndSendingEmail(emailMessage, apiKey, apiUrl);

        }

    }
}


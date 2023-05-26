using AppWithRabbitmq.Producent;

namespace AppWithRabbitmq.Consumer.SendingEmailProviders
{
    public class MandrillEmailProvider : IEmailProvider
    {
        public void SendEmailWithProvider(EMailMessage emailMessage)
        {
           
            string apiKey = "your_mandrill_api_key";
            string apiUrl = "https://mandrillapp.com/api/1.0/messages/send";

            PrepareAndSendEmailWithProvidersApi.PrepareAndSendingEmail(emailMessage, apiKey, apiUrl);

        }
    }
}

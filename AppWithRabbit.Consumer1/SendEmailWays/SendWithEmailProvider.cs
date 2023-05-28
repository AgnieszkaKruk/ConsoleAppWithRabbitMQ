using AppWithRabbitmq.Producent;


namespace AppWithRabbitmq.Consumer.SendingEmailProviders
{
    public class SendWithEmailProvider : IEmailProvider
    {
        public void SendEmailWithProvider(EMailMessage emailMessage)
        {
            var (apiKey, apiUrl) = PrepareAndSendEmailWithApi.ReadApiKeyUrlFromFile(emailMessage);
            PrepareAndSendEmailWithApi.PreparingAndSendingEmail(emailMessage, apiKey, apiUrl);

        }

    }
}



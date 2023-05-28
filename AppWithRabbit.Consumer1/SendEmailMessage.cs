using AppWithRabbitmq.Consumer.EmailProviders;
using AppWithRabbitmq.Consumer.SendingEmailProviders;
using AppWithRabbitmq.Producent;
namespace AppWithRabbitmq.Consumer
{
    public class SendEmailMessage
    {
        public static async Task SendEmail(EMailMessage emailMessage)
        {
            IEmailProvider provider;

            string emailProvider = emailMessage.EmailProvider.ToLower();

            if (!string.IsNullOrEmpty(emailProvider))
            {
                if (emailProvider == "sendgrid" || emailProvider == "mandrill" || emailProvider == "mailgun")
                {
                    provider = new SendWithEmailProvider();
                }
                else
                {
                    Console.WriteLine("Unsupported provider. Email will be send by StmpClient");
                    provider = new SendEmailBySmtp();
                }
            }
            else
            {
                Console.WriteLine("You don't type provider. Email will be send by StmpClient");
                provider = new SendEmailBySmtp();
            }

            provider.SendEmailWithProvider(emailMessage);
        }

    }
}




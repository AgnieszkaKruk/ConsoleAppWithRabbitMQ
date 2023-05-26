using System.Net;
using System.Net.Mail;
using System.Text;
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
                if (emailProvider == "sendgrid")
                {
                    provider = new SenGridEmailProvider();
                }
                else if (emailProvider == "mailgun")
                {
                    provider = new MailgunEmailProvider();
                }
                else if (emailProvider == "mandrill")
                {
                    provider = new MandrillEmailProvider();
                }

                else
                {
                    Console.WriteLine("Unsupported email provider. Email will be send by StmpClient");
                    provider = new SendingEmailBySmtp();
                }
            }

            else
            {
                provider = new SendingEmailBySmtp();
            }

            provider.SendEmailWithProvider(emailMessage);
        }


    }
}




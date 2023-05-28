using AppWithRabbitmq.Producent;
using System.Net;
using System.Net.Mail;

namespace AppWithRabbitmq.Consumer.EmailProviders
{
    public class SendEmailBySmtp : IEmailProvider
    {
        public void SendEmailWithProvider(EMailMessage emailMessage)
        {

            var infos = ReadInfosFromAppsetingsFile();

            var smtpServer = infos.Item1;
            var smtpUsername = infos.Item2;
            var smtpPassword = infos.Item3;
            var smtpPort = infos.Item4;

            string from = emailMessage.From;
            string to = emailMessage.To;


            MailMessage message = new MailMessage(from, to);
            message.Subject = emailMessage.Subject;
            message.Body = emailMessage.Body;


            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

            try
            {

                smtpClient.Send(message);
                Console.WriteLine("Email sent.");
            }
            catch (Exception ex)
            {
                //spodziewam sie tu błędu poniewaz mam fake'owe dane konfiguracyjne
                Console.WriteLine($"Error while sending email: {ex.Message}"); 
            }
        }

        private (string, string, string, int) ReadInfosFromAppsetingsFile()
        {
            var emailProvider = "smtp";
            var configuration = PrepareAndSendEmailWithApi.BuildConfiguration();

            var emailProviderConfig = configuration.GetSection("EmailProviders").GetSection(emailProvider);

            var smtpServer = emailProviderConfig["smtpServer"];
            var smtpPassword = emailProviderConfig["smtpPassword"];
            int smtpPort = int.Parse(emailProviderConfig["smtpPort"]);
            var smtpUsername = emailProviderConfig["smtpUsername"];

            return (smtpServer,smtpUsername, smtpPassword, smtpPort);
        }
    }
}



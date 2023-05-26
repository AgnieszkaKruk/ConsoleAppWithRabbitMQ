using AppWithRabbitmq.Producent;
using System.Net;
using System.Net.Mail;

namespace AppWithRabbitmq.Consumer.EmailProviders
{
    public class SendingEmailBySmtp : IEmailProvider
    {
        public void SendEmailWithProvider(EMailMessage emailMessage)
        {
            string smtpServer = "smtp.example.com";
            int smtpPort = 587;
            string smtpUsername = "your_username";
            string smtpPassword = "your_password";


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
                Console.WriteLine($"Error while sending email: {ex.Message}");
            }
        }
    }
}

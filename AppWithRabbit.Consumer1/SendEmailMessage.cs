using System.Net;
using System.Net.Mail;
using System.Text;
using AppWithRabbitmq.Producent;
namespace AppWithRabbitmq.Consumer
{
    public class SendEmailMessage
    {

        public static async Task SendEmail(EMailMessage emailMessage)
        {
            string emailProvider = emailMessage.EmailProvider;

            if (!string.IsNullOrEmpty(emailProvider))
            {
                await SendEmailByProvidersApi(emailProvider, emailMessage);
            }

            else
            {
                await SendEmailBySmtp(emailMessage);
            }
        }



        private static async Task SendEmailByProvidersApi(string emailProvider, EMailMessage emailMessage)
        {
            string emailProviderLower = emailProvider.ToLower();
            string apiKey;
            string apiUrl;

            if (emailProviderLower == "sendgrid")
            {
                apiKey = "your_sendgrid_api_key";
                apiUrl = "https://api.sendgrid.com/v3/mail/send";
            }
            else if (emailProviderLower == "mailgun")
            {
                apiKey = "your_mailgun_api_key";
                apiUrl = "https://api.mailgun.net/v3/your_domain.com/messages";
            }
            else if (emailProviderLower == "mandrill")
            {
                apiKey = "your_mandrill_api_key";
                apiUrl = "https://mandrillapp.com/api/1.0/messages/send";
            }
            else
            {
                Console.WriteLine("Unsupported email provider.");
                return;
            }

            var requestData = new
            {
                From = emailMessage.From,
                To = emailMessage.To,
                Subject = emailMessage.Subject,
                Body = emailMessage.Body
            };


            string jsonRequestData = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);

                try
                {
                    var response = await httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Email sent by {emailProvider}'s API");
                    }
                    else
                    {
                        Console.WriteLine($"Error while sending email by {emailProvider}'s API. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while sending email:{ex.Message}");
                }
            }

        }

        private static async Task SendEmailBySmtp(EMailMessage emailMessage)
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




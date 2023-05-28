using AppWithRabbitmq.Producent;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace AppWithRabbitmq.Consumer
{
    public static class PrepareAndSendEmailWithApi 
    {
         public static EMailMessage TypeMeil()
        {
            Console.WriteLine("Type sender e-mail:");
            string from = Console.ReadLine();

            Console.WriteLine("Type recipient e-mail:");
            string to = Console.ReadLine();

            Console.WriteLine("Type e-mail subject:");
            string subject = Console.ReadLine();

            Console.WriteLine("Type e-mail message:");
            string message = Console.ReadLine();

            Console.WriteLine("Type e-mail provider or nothing if you want to send e-mail by SmtpClient:");
            string provider = Console.ReadLine();
            EMailMessage emailMessage = new EMailMessage(from, to, subject, message, provider);

            return emailMessage;

        }


        public static async Task PreparingAndSendingEmail(EMailMessage emailMessage, string apiKey, string apiUrl)
        {
            string emailProvider = emailMessage.EmailProvider;

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
                        //spodziewam sie tu błędu poniewaz nie mam prawdziwych password
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error while sending email by {emailProvider}'s API. Status code: {response.StatusCode}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error while sending email:{ex.Message}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

        }

        public static (string,string) ReadApiKeyUrlFromFile(EMailMessage eMailMessage)
        {
            var emailProvider = eMailMessage.EmailProvider.ToLower();
            var configuration = BuildConfiguration();

            var emailProviderConfig = configuration.GetSection("EmailProviders").GetSection(emailProvider);
            var apiKey = emailProviderConfig["ApiKey"];

            var apiUrl = emailProviderConfig["ApiUrl"];
            return (apiKey,apiUrl);
        }

        public static IConfiguration BuildConfiguration()
        {
            var basePath = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)

                .Build();

            return configuration;
        }
    }
}

using AppWithRabbitmq.Producent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithRabbitmq.Consumer
{
    public static class PrepareAndSendEmailWithProvidersApi
    {
        public static async Task PrepareAndSendingEmail(EMailMessage emailMessage, string apiKey, string apiUrl)
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
    }
}

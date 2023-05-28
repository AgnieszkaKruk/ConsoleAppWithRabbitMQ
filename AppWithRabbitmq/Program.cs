using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using static AppWithRabbitmq.Producent.MakeValidEmail;

class Program
{
    static void Main()
    {
        Console.Title = "Console App with Rabbitmq - Producent";
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "email_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var emailMessage = EnterEmail();

            var messageJson = JsonConvert.SerializeObject(emailMessage);
            var body = Encoding.UTF8.GetBytes(messageJson);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                                 routingKey: "email_queue",
                                 properties,
                                 body: body);

            Console.WriteLine("Email message sent.");
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey(true);

    }
}




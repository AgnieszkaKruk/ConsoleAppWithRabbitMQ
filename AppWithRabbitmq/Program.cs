using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using AppWithRabbitmq.Producent;


class Program
{
    static void Main()
    {
        Console.Title = "Console App with Rabbitmq";
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "email_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);



            var emailMessage = TypeMeil();


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
        Console.ReadLine();




        static EMailMessage TypeMeil()
        {
            Console.WriteLine("Type sender e-mail:");
            string from = Console.ReadLine();

            Console.WriteLine("Type recipient e-mail:");
            string to = Console.ReadLine();

            Console.WriteLine("Type e-mail subject:");
            string subject = Console.ReadLine();

            Console.WriteLine("Type e-mail message:");
            string message = Console.ReadLine();

            EMailMessage emailMessage = new EMailMessage(from, to, subject, message, null);

            return emailMessage;

        }
    }
}




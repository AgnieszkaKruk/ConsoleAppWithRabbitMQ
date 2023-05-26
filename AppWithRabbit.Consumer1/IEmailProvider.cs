using AppWithRabbitmq.Producent;

namespace AppWithRabbitmq.Consumer
{
    interface IEmailProvider
    {
        void SendEmailWithProvider(EMailMessage message);
    }
}

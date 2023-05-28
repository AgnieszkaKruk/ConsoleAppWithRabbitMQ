using System.Text.RegularExpressions;

namespace AppWithRabbitmq.Producent
{
    public static class MakeValidEmail
    {
        public static EMailMessage EnterEmail()
        {
            Console.WriteLine("Enter sender e-mail:");
            string from = Console.ReadLine();

            while (!IsValidEmail(from))
            {
                Console.WriteLine("Invalid sender e-mail. Please enter a valid e-mail address:");
                from = Console.ReadLine();
            }

            Console.WriteLine("Enter recipient e-mail:");
            string to = Console.ReadLine();

            while (!IsValidEmail(to))
            {
                Console.WriteLine("Invalid recipient e-mail. Please enter a valid e-mail address:");
                to = Console.ReadLine();
            }

            Console.WriteLine("Enter e-mail subject:");
            string subject = Console.ReadLine();

            Console.WriteLine("Enter e-mail message:");
            string message = Console.ReadLine();

            Console.WriteLine("Enter e-mail provider or nothing if you want to send e-mail by SmtpClient:");
            string provider = Console.ReadLine();

            EMailMessage emailMessage = new EMailMessage(from, to, subject, message, provider);

            return emailMessage;
        }


        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
                return Regex.IsMatch(email, pattern);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}

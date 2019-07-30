using System;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace WeatherStationApi._01_Common.Utilities
{
    public class LogErrorEmail
    {
        private static string source;
        private static string message;
        private static string stackTrace;
        private static readonly string username = "itrw324.weatherstationapi@gmail.com";
        private static readonly string password = @"^H*A9fd#j5\9qC}q>y?m";

        public static void SendError(Exception e)
        {
            message = e.Message;
            source = e.Source;
            stackTrace = e.StackTrace;
            Thread thread = new Thread(new ThreadStart(runSaveError));
            thread.Start();
        }
        
        public static void SendErrorTest()
        {
            Console.WriteLine("[ OK  ] LogErrorEmail: Sending email test!");
            message = "This is a test!";
            source = "This is a test!";
            stackTrace = "This is a test!";
            Thread thread = new Thread(new ThreadStart(runSaveError));
            thread.Start();
        }

        private static void runSaveError()
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential(username, password);
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress(username),
                    Subject = "WeatherStationAPI - System Error:  " + DateTime.Today.ToLongDateString(),
                    Body = DateTime.Now.ToString("h:mm:ss tt") + "\n\n" + message + "\n\n" + source + "\n\n" + message + "\n\n" + stackTrace + "\n\n-------------------------------------------FIN-------------------------------------------"
                };
                mail.IsBodyHtml = true;
                mail.To.Add("coen.human@gmail.com");
                mail.To.Add("morneventer.mv@gmail.com ");
                mail.To.Add("eonviljoen.ev@gmail.com ");
                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };
                client.Send(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] LogErrorEmail: " + e.Message);
            }
        }
    }
}
using System;
using System.Net;
using System.Net.Mail;

namespace Sandbox.Email
{
    class Program
    {
        static void Main(string[] args)
        {
            MailAddress fromAddress = new MailAddress("fromuser@company.com", "From User");
            const string fromPassword = "xxxxxxxxxxxxxxxx";
            const string subject = "Test Subject";

            const string body = "<htlm><body><h1>Test Body</h1></body></html>";
            const bool isHtml = true;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (MailMessage message = new MailMessage())
            {
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                message.To.Add(new MailAddress("touser@outlook.com", "Outlook User"));
                message.To.Add(new MailAddress("touser@gmail.com", "Gmail User"));

                message.From = fromAddress;

                smtp.Send(message);
                Console.WriteLine("Message Sent");
            }
        }
    }
}

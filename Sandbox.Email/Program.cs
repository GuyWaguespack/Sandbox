using System;
using System.Net;
using System.Net.Mail;

namespace Sandbox.Email
{
    class Program
    {
        static void Main(string[] args)
        {
            MailAddress fromAddress = new MailAddress("xxxxxxxx@company.com", "John Smith");
            MailAddress toAddress = new MailAddress("xxxxxxxx@company.com", "Jane Doe");
            const string fromPassword = "xxxxxxxxxxxxxxxx";
            const string subject = "Test Subject";
            const string body = "Test Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (MailMessage message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}

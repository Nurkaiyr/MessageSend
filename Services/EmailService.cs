using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailService : IEmailService
    {
        MessageContext _context;
        private const string host = "smtp.gmail.com";
        private const int port = 587;
        private const string email = "smtptesteritstep@gmail.com";
        private const string password = "167AEq!!";

        public async Task<IEnumerable<Message>> GetAll()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task SendEmailAsync(string to, string title, string body)
        {
            await Task.Run(() =>
            {
                MailMessage mailMessage = new MailMessage(email, to)
                {
                    Body = body,
                    Subject = title
                };

                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = host;
                    client.Port = port;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(email, password);

                    client.Send(mailMessage);
                }
            });
            //public async Task SendEmailAsync(string email, string subject, string message)
            //{
            //    var emailMessage = new MimeMessage();

            //    emailMessage.From.Add(new MailboxAddress("Администрация сайта", "smtptesteritstep@gmail.com"));
            //    emailMessage.To.Add(new MailboxAddress("", email));
            //    emailMessage.Subject = subject;
            //    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            //    {
            //        Text = message
            //    };

            //    using (var client = new SmtpClient())
            //    {
            //        await client.ConnectAsync("smtp.gmail.com", 465, true);
            //        await client.AuthenticateAsync("smtptesteritstep@gmail.com", "167AEq!!");
            //        await client.SendAsync(emailMessage);

            //        await client.DisconnectAsync(true);
            //    }
        }
    }
}

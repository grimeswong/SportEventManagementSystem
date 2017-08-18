using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

namespace SportEventManagementSystem.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public EmailSettings _emailSettings { get; }
        public AuthMessageSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailData = new MimeMessage();
                emailData.From.Add(new MailboxAddress(_emailSettings.Name,_emailSettings.UsernameEmail));
                emailData.To.Add(new MailboxAddress(email));
                emailData.Subject = subject;
                emailData.Body = new TextPart("plain")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    client.Send(emailData);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
            }
            return Task.FromResult(0);
        }

        public void SendEmail(string email, string subject, string message)
        {
            try
            {
                var emailData = new MimeMessage();
                emailData.From.Add(new MailboxAddress(_emailSettings.Name, _emailSettings.UsernameEmail));
                emailData.To.Add(new MailboxAddress(email));
                emailData.Subject = subject;
                emailData.Body = new TextPart("plain")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    client.Send(emailData);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}

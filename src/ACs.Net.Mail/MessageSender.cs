using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;


namespace ACs.Net.Mail
{
    public class MessageSender : IMessageSender
    {
        private readonly ISmptConfiguration _smtpConfiguration;
        public MessageSender(ISmptConfiguration smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration;
        }

        public Task SendEmailAsync(string email, string subject, IBody body)
        {
            return SendEmailAsync(email, subject, body.ToHtml());
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

    }
}

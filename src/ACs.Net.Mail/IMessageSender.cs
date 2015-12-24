using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACs.Net.Mail
{
    public interface IMessageSender
    {
        Task<bool> SendEmailAsync(IHtmlMessage body);
        Task<bool> SendEmailAsync(string email, string subject, IHtmlMessage body);
        Task<bool> SendEmailAsync(string email, string subject, string message);        
    }
}

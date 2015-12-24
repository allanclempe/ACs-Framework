using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACs.Net.Mail;

namespace ACs.Framework.Web.Infra
{
    public class SmtpConfiguration : ISmtpConfiguration
    {
        public bool Activated { get; set; }

        public string From { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public string Server { get; set; }

        public int Timeout { get; set; }

        public string UserName { get; set; }

        public bool UseSSL { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACs.Net.Mail.Tests
{
    public class SmtpConfiguration : ISmtpConfiguration
    {


        public bool Activated => true;

        public string From { get; set; }

        public string Password { get; set; }

        public int Port => 587;

        public string Server { get; private set; }

        public int Timeout => 5000;

        public string UserName { get; private set; }

        public bool UseSSL { get; private set; }
    }
}

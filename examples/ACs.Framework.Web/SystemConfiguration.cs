using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ACs.Framework.Web.Core.Infra;
using ACs.Net.Mail.Message;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.PlatformAbstractions;

namespace ACs.Framework.Web
{
    public class SystemConfiguration : ISystemConfiguration
    {
        private readonly IApplicationEnvironment _environment;

        public SystemConfiguration(IApplicationEnvironment environment)
        {
            _environment = environment;
        }

        public IHtmlMessage GetMailMessage(EmailTemplate emailTemplate)
        {
            using (var sr = File.OpenText(Path.Combine(_environment.ApplicationBasePath, $"/emails/{emailTemplate}.html")))
              return new HtmlMessage(sr);
        }

      
    }
}

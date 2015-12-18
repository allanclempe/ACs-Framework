using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACs.Net.Mail
{
    public class BodyFormater
    {
        private static IDictionary<string, string> _replaceKeys = new Dictionary<string, string> {
            { "\r", "<br>" },
            { "\r\n", "<br>" }
        };

       
        public static string Replace(string template)
        {
            foreach (var replace in _replaceKeys)
                template = template.Replace(replace.Key, replace.Value);

            return template;
        }


    }
}

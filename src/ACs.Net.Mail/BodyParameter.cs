using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACs.Net.Mail
{
    public static class BodyParameter
    {
        public static string Replace(string template, IDictionary<string, object> parameters)
        {
            foreach (var replace in parameters)
            {
                template = Replace(template, replace.Key, replace.Value);
            }

            return template;
        }

        public static string Replace(string template, string key, string value)
        {
            return Replace(template, key, (object)value);
        }

        public static string Replace(string template, string key, Uri value)
        {
            return Replace(template, key, (object)value);
        }

        private static string Replace(string template, string key, object value)
        {
            var toValue = string.Empty;
            if (value.GetType() == typeof(Uri))
                toValue = string.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", ((Uri)value).ToString());
            else
                toValue = value.ToString();

            template = template.Replace("{{" + key + "}}", toValue);

            return template;
        }
    }
}

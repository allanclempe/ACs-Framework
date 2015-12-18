using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ACs.Net.Mail;

namespace ACs.Net.Mail.Tests
{

    public class BodyParameterTest
    {
        public BodyParameterTest()
        {
        }

        [Fact]
        public void ReplaceUrl()
        {
            var uri = new Uri("Http://somedomain.com/test");
            var result = BodyParameter.Replace("{{url}}", "url", new Uri("Http://somedomain.com/test"));

            Assert.Equal(string.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", uri.ToString()), result);
        }

        [Fact]
        public void ReplaceString()
        {
            var userName = "User Test";
            var result = BodyParameter.Replace("Your name is {{name}}. Other test", "name", userName);

            Assert.Equal(string.Format("Your name is {0}. Other test", userName), result);
        }

        [Fact]
        public void ReplaceByDictionary()
        {
            var p2 = new Uri("http://somedomain.com/test");
            var p1 = "Some test";

            var result = BodyParameter.Replace("Parameter1 {{p1}} and parameter 2 {{p2}}", new Dictionary<string,object>()  {
                { "p1", p1 },
                { "p2", p2 }
            });


            var htmlUri = string.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", p2.ToString());

            Assert.Equal(string.Format("Parameter1 {0} and parameter 2 {1}", p1, htmlUri),  result);
        }

    }
}

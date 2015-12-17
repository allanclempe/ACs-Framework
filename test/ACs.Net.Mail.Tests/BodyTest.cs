using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ACs.Net.Mail.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class BodyTest
    {
        private const string _templatePlainText = "Welcome {{name}}\r\rTo activate your account click link above.\r\r{{link}}\r\rThanks";
        private const string _templateHtml = "<div>Welcome {{name}}<br><br>To activate your account click link above.<br><br>{{link}}<br><br>Thanks</div>";


        [Fact]
        public void CreateBodyAsPlainText()
        {
            var name = "User Name";
            var uri = new Uri("Http://somedomain.com");

            var body = new Body(_templatePlainText, TemplateType.PlainText)
                .SetParam("name", name)
                .SetParam("link", uri);

            var result = body.ToHtml();

            Assert.Contains("<br>", result, StringComparison.InvariantCultureIgnoreCase);
            Assert.Contains(name, result);
            Assert.Contains("a href", result);
            Assert.Contains(uri.ToString(), result);
        }

        [Fact]
        public void CreateBodyAsHtml()
        {
            var name = "User Name";
            var uri = new Uri("Http://somedomain.com");

            var body = new Body(_templateHtml, TemplateType.Html)
                .SetParam("name", name)
                .SetParam("link", uri);

            var result = body.ToHtml();

            Assert.Contains("<br>", result, StringComparison.InvariantCultureIgnoreCase);
            Assert.Contains(name, result);
            Assert.Contains("a href", result);
            Assert.Contains(uri.ToString(), result);

        }

        [Fact]
        public void SetFontFamilyAndSize()
        {
            var body = new Body(_templatePlainText, TemplateType.PlainText);
            body.SetFont("verdana", 11);

            var result = body.ToHtml();

            Assert.Contains("font-family:verdana", result, StringComparison.InvariantCultureIgnoreCase);
            Assert.Contains("font-size:11", result, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}

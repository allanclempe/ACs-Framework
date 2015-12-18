using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ACs.Net.Mail;

namespace ACs.Net.Mail.Tests
{
    public class BodyFormaterTest
    {

        [Fact]
        public void ReplaceBreakLine()
        {
            var text = "Some text\rAnother line";
            var result = BodyFormater.Replace(text);

            Assert.Equal("Some text<br>Another line", result);
        }

      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;

namespace ACs.Net.Mail
{
    public interface IBody
    {
        IBody SetParam(string name, string value);
        IBody SetParam(string name, Uri value);
        IBody SetFont(string fontName, int fontSize);
        string ToHtml();
    }
}

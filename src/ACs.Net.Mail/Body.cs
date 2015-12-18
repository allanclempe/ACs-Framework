using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ACs.Net.Mail
{
    public class Body: IBody
    {    
        private const string _templateDefault = "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional //EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html><head></head><body></body></html>";
        private readonly KeyValuePair<TemplateType, string> _templateBody;
        private IDictionary<string, object> _params;
        private string _fontFamily = "Verdana";
        private int _fontSize = 11;
        

        public Body(string template, TemplateType type)
        {
            if (string.IsNullOrEmpty(template)) throw new ArgumentException(nameof(template));
            _templateBody = new KeyValuePair<TemplateType, string>(type, template);
            _params = new Dictionary<string, object>();
        }

        public IBody SetParam(string name, string value)
        {
            return SetParam(name, (object)value);
        }

        public IBody SetParam(string name, Uri value)
        {
            return SetParam(name, (object)value);
        }

        private IBody SetParam(string name, object value)
        {
            if (!_params.ContainsKey(name))
            {
                _params.Add(name, value);
                return this;
            }

            _params[name] = value;

            return this;
        }

        public IBody SetFont(string fontName, int fontSize)
        {
            _fontFamily = fontName;
            _fontSize = fontSize;
            return this;
        }

        public string ToHtml()
        {
            var documentBody = new HtmlDocument();
            
            var template = _templateBody.Value;
            var mainDiv = HtmlNode.CreateNode("<div></div>");
            
            if (_templateBody.Key == TemplateType.PlainText)
                template = BodyFormater.Replace(template);

            template = BodyParameter.Replace(template, _params);

            mainDiv.Attributes.Add("style", string.Format("font-family:{0};font-size:{1}px", _fontFamily, _fontSize));
            mainDiv.AppendChild(HtmlNode.CreateNode(string.Format("<div>{0}</div>", template)));

            documentBody.LoadHtml(_templateDefault);
            documentBody.DocumentNode
                .SelectSingleNode("//body")
                .AppendChild(mainDiv);

            return documentBody.DocumentNode.OuterHtml;
        }
    }
}

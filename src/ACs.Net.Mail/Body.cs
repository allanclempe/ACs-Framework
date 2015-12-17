using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ACs.Net.Mail
{
    public class Body: IBody
    {    
        private const string _templateDefault = "<html><head></head><body></body>";
        private readonly KeyValuePair<TemplateType, string> _templateBody;
        private IDictionary<string, object> _params;
        private string _fontFamily = "Verdana";
        private int _fontSize = 11;
        private IDictionary<string, string> _plainTextReplacement = new Dictionary<string, string> {
            {"\r", "<br>" }
        };


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

        private string SetPlainText(string template)
        {
            foreach (var replace in _plainTextReplacement)
                template = template.Replace(replace.Key, replace.Value);

            return template;
        }

        private string SetParameters(string template)
        {
            foreach (var replace in _params)
            {
                var toValue = string.Empty;
                if (replace.Value.GetType() == typeof(Uri))
                    toValue = string.Format("<a href=\"{0}\" target=\"blank\">{0}</a>", ((Uri)replace.Value).ToString());
                else
                    toValue = replace.ToString();

                template = template.Replace("{{" + replace.Key + "}}", toValue);
            }

            return template;
        }

        public string ToHtml()
        {
            var documentBody = new HtmlDocument();
            
            var template = _templateBody.Value;
            var mainDiv = HtmlNode.CreateNode("<div></div>");
            
            if (_templateBody.Key == TemplateType.PlainText)
                template = SetPlainText(template);

            template = SetParameters(template);

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

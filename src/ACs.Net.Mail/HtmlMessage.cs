﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ACs.Net.Mail
{
    public class HtmlMessage: IHtmlMessage
    {    
        private const string _templateDefault = "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional //EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html><head></head><body></body></html>";               
        private string _fontFamily = "Verdana";
        private int _fontSize = 11;

        public string MailTo { get; private set; }
        public string Subject { get; private set; }
        public HtmlDocument Html { get; private set; }
        public IDictionary<string, object> Params {get; private set;}
        
        public HtmlMessage(string html)
        {
            if (string.IsNullOrEmpty(html)) throw new ArgumentException(nameof(html));

            Params = new Dictionary<string, object>();
            Html = new HtmlDocument();

            LoadHtml(html);
            
        }

        public IHtmlMessage SetParam(string name, string value)
        {
            Html = HtmlParameterReplacement.Replace(Html, name, value);
            return this;
        }

        public IHtmlMessage SetParam(string name, Uri value)
        {
            Html = HtmlParameterReplacement.Replace(Html, name, value);
            return this;
        }

        public IHtmlMessage SetParams(IDictionary<string,object> values)
        {
            Html = HtmlParameterReplacement.Replace(Html, values);
            return this;
        }

        public IHtmlMessage SetParams(object values)
        {
            Html = HtmlParameterReplacement.Replace(Html, values);
            return this;
        }

        public IHtmlMessage SetFont(string fontName, int fontSize)
        {
            var bodyNode = Html.DocumentNode.SelectNodes("//body")
                .First();

            if (bodyNode.Attributes.Any(x => x.Name.Equals("style", StringComparison.InvariantCultureIgnoreCase))){
                bodyNode.Attributes.Remove("style");
            }

            bodyNode.Attributes.Add("style", string.Format("font-family:'{0}';font-size:{1}px", fontName, fontSize));

            return this;
        }

        public string ToHtml()
        {
            return Html.DocumentNode.OuterHtml;
        }

        private void LoadHtml(string html)
        {
            Html.LoadHtml(html);

            SetMetas(Html);

            var bodyNode = Html.DocumentNode.SelectNodes("//body");
            HtmlNode body;

            if (bodyNode != null && bodyNode.Count > 0)
            {
                body = bodyNode.First();
            }
            else
            {
                body = HtmlNode.CreateNode("<div></div>");
                body.InnerHtml = Html.DocumentNode.InnerHtml;
            }

            var result = new HtmlDocument();
            result.LoadHtml(_templateDefault);
            result.DocumentNode
                .SelectSingleNode("//body")
                .AppendChild(body);

            Html = result;

            SetFont(_fontFamily, _fontSize);
        }

        private void SetMetas(HtmlDocument document)
        {
            var mailTo = document.DocumentNode.SelectNodes("//meta[@name=\"mailto\"]");
            if (mailTo != null && mailTo.Count > 0)
                MailTo = mailTo.First().Attributes["value"].Value;

            var subject = document.DocumentNode.SelectNodes("//meta[@name=\"subject\"]");

            if (subject != null && subject.Count > 0)
                Subject = subject.First().Attributes["value"].Value;

        }
    }
}

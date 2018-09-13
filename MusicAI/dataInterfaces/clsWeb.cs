using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace WebPageReader
{
    public class clsWeb
    {
        /***************************************************************
         * This class is used to request web pages and return
         * a string of either HTML or cleaned human readable text.
         **************************************************************/

        private string _html = null;
        private string _text = null;
        private string _url = null;

        public clsWeb()
        {

        }

        protected void request(string url)
        {
            _html = "";
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                _html = String.Empty;
                using (StreamReader sr = new StreamReader(data))
                {
                    _html = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public string url
        {
            get
            {
                return _url;
            }
        }

        public string html
        {
            get
            {
                return _html;

            }

        }


        public string text
        {
            get
            {
                if (_text == null)
                {
                    try
                    { 
                        // remove markup
                        string cleanHTML = System.Web.HttpUtility.HtmlDecode(this.html);

                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(cleanHTML);

                        // remove script
                        doc.DocumentNode.Descendants()
                        .Where(n => n.Name == "script" || n.Name == "style")
                        .ToList()
                        .ForEach(n => n.Remove());

                        // remove html
                        _text = "";
                        foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//text()"))
                        {
                            _text += node.InnerText;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return _text;
            }
        }

    }
}

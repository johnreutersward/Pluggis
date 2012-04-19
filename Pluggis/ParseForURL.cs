using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Pluggis
{
    class ParseForURL
    {

        private string[] messageLine;

        //Yeah, what madman would have two url's in the same line! 
        private string url;

        public string outMessage;

        public ParseForURL(string[] messageLine)
        {
            this.messageLine = messageLine;
            foreach (string entry in messageLine)
            {
                string entryLower = entry.ToLower();

                //If you post links without http:// then you hate the internet
                if (entryLower.StartsWith("http://") || entryLower.StartsWith("www."))
                {
                    url = entry;
                }
            }

            if (url != null && !url.Equals(""))
            {
                if (url.Contains("ragefac.es/") && url.Length > 18)
                {
                    url = CheckLastSlash(url);
                    int lastIndex = url.LastIndexOf(".es/") + 4;
                    string id = url.Substring(lastIndex, url.Length - lastIndex);
                    Console.WriteLine(id);
                    Ragefaces rage = new Ragefaces(id);
                    outMessage = rage.rageInfo;
                }
            }

        }

        public string CheckLastSlash(string url)
        {
            if (url.EndsWith("/"))
            {
                return url.Substring(0, url.Length - 1);
            }
            return url;
        }

 
    }
}

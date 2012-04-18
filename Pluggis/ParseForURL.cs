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
                if (entryLower.StartsWith("http://") || entryLower.StartsWith("www."))
                {
                    url = entry;
                }
            }

            if (!url.Equals(""))
            {
                if (url.Contains("ragefac.es/"))
                {
                    Ragefaces rage = new Ragefaces(url.Substring(url.LastIndexOf("/"), url.Length+1));
                    outMessage = rage.rageInfo;
                }
            }
        }

 
    }
}

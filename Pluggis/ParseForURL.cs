using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Pluggis.Plugins;

using Meebey.SmartIrc4net;

namespace Pluggis
{
    class ParseForURL : ActionHandler
    {

        // Yeah, what madman would have two url's in the same line! 
        private string url;

        public ParseForURL(Pluggis pluggis, string[] messageLine, Channel channel)
            : base(pluggis, messageLine, channel)
        {
            Handler();
        }


        private void Handler()
        {
            foreach (string entry in messageLine)
            {
                string entryLower = entry.ToLower();

                // If you post links without http:// then you hate the internet
                if (entryLower.StartsWith("http://") || entryLower.StartsWith("www."))
                {
                    url = entry;
                }
            }

            if (url != null && !url.Equals(""))
            {
                if (url.Contains("ragefac.es/") && url.Length > 18)
                {
                    new Ragefaces(url, pluggis, channel);
                }
            }
        }
    }
}

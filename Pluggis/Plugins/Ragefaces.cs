using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Meebey.SmartIrc4net;
using Newtonsoft.Json;


namespace Pluggis.Plugins
{
    class Ragefaces : ActionHandler
    {
        private string rageURL;
        private string rageInfo;

        public Ragefaces(string rageURL, Pluggis pluggis, Channel channel)
            : base(pluggis, channel)
        {
            this.rageURL = rageURL;
            Handler();
        }

        private void Handler()
        {

            rageURL = Utils.CheckLastSlash(rageURL);
            int lastIndex = rageURL.LastIndexOf(".es/") + 4;
            string id = rageURL.Substring(lastIndex, rageURL.Length - lastIndex);
            Console.WriteLine(id);
            WebClient client = new WebClient();
            try
            {
                string rageSON = client.DownloadString("http://ragefac.es/api/id/" + id);

                //This is to get rid of the webcache at the end of the resulting string. 
                string rageSonNoCache = rageSON.Substring(0, rageSON.LastIndexOf("}") + 1);

                Rageface rage = JsonConvert.DeserializeObject<Rageface>(rageSonNoCache);
                if (rage.items.Count > 0)
                {
                    rageInfo = "Rageface tagged as " + rage.items[0].face_tags + " in category " + rage.items[0].face_category + " (" + rage.items[0].face_views + " views)";
                    pluggis.Message(channel.Name, rageInfo);
                }
            }
            catch (WebException e)
            {
                OutputConsole.Print(OutputConsole.LogType.System, "Error on web " + e.Message);
            }
        }


        public class Rageface
        {
            public List<RageItem> items;

            public class RageItem
            {
                public string face_views;
                public string face_category;
                public string face_tags;
            }
        }

    }
}

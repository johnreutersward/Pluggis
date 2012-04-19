using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using System.Net;

namespace Pluggis
{
    class Ragefaces
    {
        private string rageID;
        public string rageInfo;

        public Ragefaces(string rageID)
        {
            this.rageID = rageID;
            WebClient client = new WebClient();
            string rageSON = client.DownloadString("http://ragefac.es/api/id/" + rageID);

            //This is to get rid of the webcache at the end of the resulting string. 
            string rageSonNoCache = rageSON.Substring(0, rageSON.LastIndexOf("}") + 1);

            Rageface rage = JsonConvert.DeserializeObject<Rageface>(rageSonNoCache);
            rageInfo = "Rageface tagged as " + rage.items[0].face_tags + " in category " + rage.items[0].face_category + " (" + rage.items[0].face_views + " views)"; 
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

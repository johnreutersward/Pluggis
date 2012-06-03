using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using Meebey.SmartIrc4net;
using Newtonsoft.Json;

namespace Pluggis.Plugins
{
    class ShortenURL : ActionHandler
    {

        private string shortURL;
        private readonly string outMsg = "Short URL: ";

        public ShortenURL(Pluggis pluggis, string[] messageLine, Channel channel)
            : base(pluggis, messageLine, channel)
        {
            Handler();
        }


        private void Handler()
        {
            if (length > 1)
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/urlshortener/v1/url");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"longUrl\":\"" + messageLine[1] + "\"}";
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    ShortResponse shortResponse = JsonConvert.DeserializeObject<ShortResponse>(responseText);
                    shortURL = shortResponse.id;
                }
                pluggis.Message(channel.Name, outMsg + shortURL);
            }
}

        public class ShortResponse
        {
            public string id;
        }
    }
}

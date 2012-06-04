using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Newtonsoft.Json;

namespace Pluggis
{
    class Settings
    {

        private readonly string path = @"Settings.JSON";
        private JSONSettings settings;

        public Settings()
        {
            ReadSettings();
        }

        private void ReadSettings()
        {
            if (File.Exists(path))
            {
                using (StreamReader streamReader = File.OpenText(path))
                {
                    string file = streamReader.ReadToEnd();
                    settings = JsonConvert.DeserializeObject<JSONSettings>(file);
                    string outMsg = "SETTINGS: " + settings.nick + ", " + settings.connection.server + ", " + settings.connection.channels[0];
                    Console.WriteLine(outMsg);
                    Run();
                }
            }
        }

        private void Run()
        {
            Pluggis pluggis = new Pluggis(settings.admin, settings.nick, settings.user, settings.connection.server, settings.connection.port, settings.connection.channels);
        }

        public class JSONSettings
        {
            public string nick;
            public string user;
            public List<string> plugins;
            public string admin;
            public JSONSettingsConnection connection;

            public class JSONSettingsConnection
            {
                public string server;
                public int port;
                public List<string> channels;
            }

        }
    }
}

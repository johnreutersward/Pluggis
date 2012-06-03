using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Meebey.SmartIrc4net;

namespace Pluggis.Plugins
{
    class VersionInfo : ActionHandler
    {

        private readonly string version = "Pluggis C# IRC-BOT available @ http://github.com/rojters/Pluggis";

        public VersionInfo(Pluggis pluggis, Channel channel)
            : base(pluggis, channel)
        {
            Handler();
        }

        private void Handler()
        {
            pluggis.Message(channel.Name, version);
        }
    }
}

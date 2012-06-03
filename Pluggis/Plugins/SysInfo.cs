using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Meebey.SmartIrc4net;

namespace Pluggis.Plugins
{
    class SysInfo : ActionHandler
    {

        public SysInfo(Pluggis pluggis, Channel channel)
            : base(pluggis, channel)
        {
            Handler();
        }

        private void Handler()
        {
            pluggis.Message(channel.Name, Environment.OSVersion + " " + Environment.ProcessorCount + " CPU(s)");
        }

    }
}

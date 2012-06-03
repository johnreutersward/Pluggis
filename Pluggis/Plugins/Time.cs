using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Meebey.SmartIrc4net;

namespace Pluggis.Plugins
{
    class Time : ActionHandler
    {
        public Time(Pluggis pluggis, Channel channel)
            : base(pluggis, channel)
        {
            Handler();
        }

        private void Handler()
        {
            pluggis.Message(channel.Name, System.DateTime.Now.ToString());
        }
    }
}

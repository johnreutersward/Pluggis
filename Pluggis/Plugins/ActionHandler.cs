using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Meebey.SmartIrc4net;

namespace Pluggis.Plugins
{
    abstract class ActionHandler
    {
        protected Pluggis pluggis;
        protected string[] messageLine;
        protected Channel channel;
        protected int length;

        public ActionHandler(Pluggis pluggis, string[] messageLine, Channel channel)
        {
            this.pluggis = pluggis;
            this.messageLine = messageLine;
            this.channel = channel;
            if (messageLine != null)
            {
                length = messageLine.Length;
            }
        }

        public ActionHandler(Pluggis pluggis, Channel channel) : this(pluggis, null, channel)
        {
        }

    }
}

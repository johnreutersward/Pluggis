using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Meebey.SmartIrc4net;

namespace Pluggis.Plugins
{
    class DiceRoll : ActionHandler
    {

        private Random rand;
        private readonly string outMsg = "Alea iacta est: ";

        public DiceRoll(Pluggis pluggis, string[] messageLine, Channel channel)
            : base(pluggis, messageLine, channel)
        {
            rand = new Random();
            Handler();
        }

        private void Handler()
        {
            int maxDice = 7;
            if (length > 1)
            {
                String _max = messageLine[1];
                int _maxDice;
                bool parseSuccess = Int32.TryParse(_max, out _maxDice);
                if (parseSuccess)
                {
                    if (_maxDice >= 1)
                    {
                        maxDice = _maxDice + 1;
                    }
                }
            }
            pluggis.Message(channel.Name, outMsg + rand.Next(1, maxDice));
        }


    }
}

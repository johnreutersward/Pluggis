using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

using Meebey.SmartIrc4net;

using Pluggis.Plugins;

namespace Pluggis
{
    class Pluggis
    {
        private string adminName;
        private string nick;
        private string user;
        private string server;
        private int port;
        private string channel;
        public bool isConnected;

        private IrcClient irc;


        public Pluggis(string adminName, string nick, string user, string server, int port, string channel)
        {
            this.adminName = adminName;
            this.nick = nick;
            this.user = user;
            this.server = server;
            this.port = port;
            this.channel = channel;
            irc = new IrcClient();
            isConnected = false;
        }

        public void Init()
        {
            Thread.CurrentThread.Name = "Main";
            irc.Encoding = System.Text.Encoding.UTF8;
            irc.SendDelay = 200;
            irc.ActiveChannelSyncing = true;
            irc.OnError += new ErrorEventHandler(OnError);
            irc.OnRawMessage += new IrcEventHandler(OnRawMessage);
            irc.OnChannelMessage += new IrcEventHandler(OnChannelMessage);
        }

        public void Disconnect()
        {
            irc.Disconnect();
            OutputConsole.Print(OutputConsole.LogType.System, "Exiting...");
        }

        public void Connect()
        {
            try
            {
                irc.Connect(server, port);
                isConnected = true;
            }
            catch (ConnectionException e)
            {
                OutputConsole.Print(OutputConsole.LogType.System, "Could not connect to " + server);
            }
        }

        public void Listen()
        {
            irc.Listen();
        }

        public void Login()
        {
            irc.Login(nick, user);
        }

        public void Join(string channel)
        {
            irc.RfcJoin(channel);
        }

        public void Message(string destination, string message)
        {
            irc.SendMessage(SendType.Message, destination, message);
            OutputConsole.Print(OutputConsole.LogType.Out, destination + " " + message);
        }

        public void OnError(object sender, ErrorEventArgs e)
        {
            OutputConsole.Print(OutputConsole.LogType.System, "Error: " + e.ErrorMessage);
        }

        public void OnRawMessage(object sender, IrcEventArgs e)
        {
            OutputConsole.Print(OutputConsole.LogType.In, e.Data.RawMessage);
        }

        public void OnChannelMessage(object sender, IrcEventArgs e)
        {
            Channel channel = irc.GetChannel(e.Data.Channel);
            string[] messageLine = e.Data.MessageArray;
            switch (e.Data.MessageArray[0])
            {
                case "+diceroll":   new DiceRoll(this, messageLine, channel);           break;
                case "+version":    new VersionInfo(this, channel);                     break;
                case "+time":       new Time(this, channel);                            break;
                case "+sysinfo":    new SysInfo(this, channel);                         break;
                default:            new ParseForURL(this, messageLine, channel);        break;
            }
        }

        static void Main(string[] args)
        {
            Pluggis pluggis = new Pluggis("Roybot", "pluggis", "pluggis-bot", "lindbohm.freenode.net", 6667, "#pluggis");
            pluggis.Init();
            pluggis.Connect();
            pluggis.Login();
            pluggis.Join("#pluggis");
            pluggis.Listen();
            Console.ReadLine();
            
        }


    }
}

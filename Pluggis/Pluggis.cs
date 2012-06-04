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
        private string admin;
        private string nick;
        private string user;
        private string server;
        private int port;
        private List<string> channels;
        public bool isConnected;

        private IrcClient irc;


        public Pluggis(string admin, string nick, string user, string server, int port, List<string> channels)
        {
            this.admin = admin;
            this.nick = nick;
            this.user = user;
            this.server = server;
            this.port = port;
            this.channels = channels;
            irc = new IrcClient();
            isConnected = false;
            Run();
        }

        private void Run()
        {
            Init();
            Connect();
            Login();
            foreach (String channel in channels)
            {
                Join(channel);
            }
            Listen();
        }

        private void Init()
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

        private void Connect()
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

        private void Listen()
        {
            irc.Listen();
        }

        private void Login()
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

        private void OnError(object sender, ErrorEventArgs e)
        {
            OutputConsole.Print(OutputConsole.LogType.System, "Error: " + e.ErrorMessage);
        }

        private void OnRawMessage(object sender, IrcEventArgs e)
        {
            OutputConsole.Print(OutputConsole.LogType.In, e.Data.RawMessage);
        }

        private void OnChannelMessage(object sender, IrcEventArgs e)
        {
            Channel channel = irc.GetChannel(e.Data.Channel);
            string[] messageLine = e.Data.MessageArray;            
            switch (e.Data.MessageArray[0])
            {
                case "+diceroll":   new DiceRoll(this, messageLine, channel);           break;
                case "+version":    new VersionInfo(this, channel);                     break;
                case "+time":       new Time(this, channel);                            break;
                case "+sysinfo":    new SysInfo(this, channel);                         break;
                case "+shorten":    new ShortenURL(this, messageLine, channel);         break;
                default:            new ParseForURL(this, messageLine, channel);        break;
            }
        }
    }
}

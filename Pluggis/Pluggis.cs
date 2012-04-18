using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

using Meebey.SmartIrc4net;

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
        private OutputConsole oc;


        public Pluggis(string adminName, string nick, string user, string server, int port, string channel)
        {
            this.adminName = adminName;
            this.nick = nick;
            this.user = user;
            this.server = server;
            this.port = port;
            this.channel = channel;
            irc = new IrcClient();
            oc = new OutputConsole();
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
            oc.Print(OutputConsole.LogType.System, "Exiting...");
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
                oc.Print(OutputConsole.LogType.System,"Could not connect to " + server);
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
            oc.Print(OutputConsole.LogType.Out, destination + " " + message);
        }

        public void OnError(object sender, ErrorEventArgs e)
        {
            oc.Print(OutputConsole.LogType.System, "Error: " + e.ErrorMessage);
        }

        public void OnRawMessage(object sender, IrcEventArgs e)
        {
            oc.Print(OutputConsole.LogType.In, e.Data.RawMessage);
        }

        public void OnChannelMessage(object sender, IrcEventArgs e)
        {
            Channel channel = irc.GetChannel(e.Data.Channel);
            int length = e.Data.MessageArray.Length;
            switch (e.Data.MessageArray[0])
            {
                case "+hi":
                    Message(e.Data.Channel, e.Data.Nick + ": " + "Hi to you aswell!"); 
                    break;
                case "+time":
                    Message(e.Data.Channel, e.Data.Nick + ": " + System.DateTime.Now.ToString());
                    break;
                case "+channel":
                    Message(e.Data.Channel, e.Data.Nick + ": " + channel.Name + " " + channel.Topic);
                    break;
                case "+rage":
                    Ragefaces rage = new Ragefaces(e.Data.MessageArray[1]);
                    Message(e.Data.Channel, rage.rageInfo);
                    break;
                default:
                    ParseForURL parse = new ParseForURL(e.Data.MessageArray);
                    if (!parse.outMessage.Equals(""))
                    {
                        Message(e.Data.Channel, parse.outMessage);
                    }
                    break;
            }
        }

        static void Main(string[] args)
        {
            Pluggis pluggis = new Pluggis("Roybot", "pluggis", "pluggis-bot", "xs4all.nl.quakenet.org", 6667, "#pluggisbot");
            pluggis.Init();
            pluggis.Connect();
            pluggis.Login();
            pluggis.Join("#pluggisbot");
            pluggis.Listen();
            Console.ReadLine();
        }


    }
}

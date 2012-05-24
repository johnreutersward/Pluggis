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
        private Random rand;

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
            rand = new Random();
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
                case "+version":
                    Message(e.Data.Channel, "Pluggis C# IRC-BOT available @ http://github.com/rojters/Pluggis"); 
                    break;
                case "+time":
                    Message(e.Data.Channel, System.DateTime.Now.ToString());
                    break;
                case "+machine":
                    Message(e.Data.Channel, Environment.OSVersion + " " + Environment.ProcessorCount + " CPU(s)");
                    break;
                case "+diceroll":
                    Message(e.Data.Channel, "Alea iacta est: " + rand.Next(1,7));
                    break;
                default:
                    ParseForURL parse = new ParseForURL(e.Data.MessageArray);
                    string outMessage = parse.outMessage;
                    if (outMessage != null && !outMessage.Equals(""))
                    {
                        Message(e.Data.Channel, parse.outMessage);
                    }
                    break;
            }
        }

        static void Main(string[] args)
        {
            Pluggis pluggis = new Pluggis("Roybot", "pluggis", "pluggis-bot", "lindbohm.freenode.net", 6667, "#iluvlinux");
            pluggis.Init();
            pluggis.Connect();
            pluggis.Login();
            pluggis.Join("#iluvlinux");
            pluggis.Listen();
            Console.ReadLine();
        }


    }
}

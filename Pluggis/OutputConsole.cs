using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluggis
{
    class OutputConsole
    {

        public enum LogType
        {
            Out = 1,
            In = 2,
            System = 3
        }

        public void Print(LogType type, string msg)
        {
            string outLine = type + " " + msg;
            Console.WriteLine(outLine);
            Log(outLine);
        }

        public void Log(string msg)
        {
        }

    }
}

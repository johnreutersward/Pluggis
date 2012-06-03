using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluggis
{
    static class OutputConsole
    {

        public enum LogType
        {
            Out = 1,
            In = 2,
            System = 3
        }

        public static void Print(LogType type, string msg)
        {
            string outLine = type + " " + msg;
            Console.WriteLine(outLine);
            Log(outLine);
        }

        public static void Log(string msg)
        {
        }

    }
}

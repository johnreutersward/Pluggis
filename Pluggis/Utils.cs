using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluggis
{
    static class Utils
    {
        public static string CheckLastSlash(string url)
        {
            if (url.EndsWith("/"))
            {
                return url.Substring(0, url.Length - 1);
            }
            return url;
        }
    }
}

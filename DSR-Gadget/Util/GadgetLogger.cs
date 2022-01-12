using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR_Gadget
{
    class GadgetLogger
    {
        private static string LogFile = $@"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}/GadgetLog.txt";

        private static List<string> LogText = new List<string> { $"{DateTime.Now}" }; //Log always starts with Date/Time

        //Add to a list so that writing too fast to disk doesn't crash the program
        public static void Log(string message)
        {
            LogText.Add(message);
        }

        //Add a blank line to the end and append all lines to the log
        public static void Flush()
        {
            LogText.Add("");
            File.AppendAllLines(LogFile, LogText);
        }
    }
}

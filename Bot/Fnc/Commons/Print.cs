using System;
using System.IO;

namespace Zeno
{
    internal partial class Program
    {
        /********************
                Print
        *//////////////////// console log
        private static void Print(string line, bool newLine = true, bool showname = true)
        {
            //Console bot print with timespamp
            string time_stamp, h = "", m = "", s = "", ms = "";
            DateTime n = DateTime.Now;
            //0 on left
            if (n.Hour < 10) { h = "0"; }
            if (n.Minute < 10) { m = "0"; }
            if (n.Second < 10) { s = "0"; }
            if (n.Millisecond < 100) { ms = "0"; }
            if (n.Millisecond < 10) { ms = "00"; }

            //getting values
            h += n.Hour.ToString();
            m += n.Minute.ToString();
            s += n.Second.ToString();
            ms += n.Millisecond.ToString();
            //making timestamp
            time_stamp = h + ":" + m + ":" + s + "." + ms + " "; //format 00:00:00

            if (!Skiplog)
            {
                if (!Directory.Exists($"{Path}\\Logs")) { Directory.CreateDirectory($"{Path}\\Logs"); }
                string f = DateTime.Now.ToString("ddMM");
                File.AppendAllText($"{Path}\\Logs\\{f}", $"{time_stamp}{line}{NL}");
            }
#if !DEBUG
            if (!Config.ConsoleLog) { return; }
#endif
            if (showname) { line = "Zeno♥ - " + line; }
            if (newLine)
            {
                if (showname)
                {
                    Console.WriteLine(time_stamp + " " + line);
                }
                else
                {
                    Console.WriteLine(" " + line);
                }
            }
            else
            {
                if (showname)
                {
                    Console.Write(time_stamp + " " + line);
                }
                else
                {
                    Console.Write(line);
                }
            }
        }
    }
}

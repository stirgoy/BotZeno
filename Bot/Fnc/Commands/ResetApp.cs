using System;
using System.Diagnostics;

namespace Zeno
{
    internal partial class Program
    {
        /********************
            ResetApp
        *//////////////////// app reload
        private static void ResetApp()
        {
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(exePath);
            Environment.Exit(0);
        }
    }
}

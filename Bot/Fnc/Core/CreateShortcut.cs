﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Zeno
{
    internal partial class Program
    {

        private static void CreateShortcut()
        {
            //const int WINDOWSTYLE_NORMAL = 1;
            const int WINDOWSTYLE_MAX = 3;
            //const int WINDOWSTYLE_MIN = 7;

            string GetFName = $"Zeno {Application.ProductVersion}.lnk";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string fullpath = System.IO.Path.Combine(path, GetFName);

            if (HasUpdated(fullpath, path))
            {
                var files = Directory.GetFiles(path);
                foreach (var item in files)
                {
                    if (item.Contains("Zeno")) File.Delete(item);
                }


                dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));
                dynamic shortcut = shell.CreateShortcut(fullpath);
                shortcut.TargetPath = Process.GetCurrentProcess().MainModule.FileName;
                shortcut.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                shortcut.WindowStyle = WINDOWSTYLE_MAX;
                shortcut.Description = "Bot Zeno";
                shortcut.Save();

                shell.Refresh();
            }
        }


    }

}
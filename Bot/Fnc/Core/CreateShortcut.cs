using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Zeno
{
    internal partial class Program
    {
        // used for be sure bot is going be online if laptop restart because win update
        private static void CreateShortcut() 
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            path = System.IO.Path.Combine(path, "Zeno.lnk");
            dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));            
            dynamic shortcut = shell.CreateShortcut(path);
            shortcut.TargetPath = Process.GetCurrentProcess().MainModule.FileName;
            shortcut.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            shortcut.WindowStyle = WINDOWSTYLE_MAX;
            shortcut.Description = "Bot Zeno";
            shortcut.Save();

            shell.Refresh();
        }

        const int WINDOWSTYLE_NORMAL = 1;
        const int WINDOWSTYLE_MAX = 3;
        const int WINDOWSTYLE_MIN = 7;

    }
}

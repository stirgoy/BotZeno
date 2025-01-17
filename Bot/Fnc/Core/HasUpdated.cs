using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zeno
{
    internal partial class Program
    {
        static bool HasUpdated(string fullpath, string folderpath)
        {
            if (File.Exists(fullpath))
            {
                Print($"Zeno Version: {Application.ProductVersion}");
                return false;
            }
            else
            {
                Print($"An update has detected => Version: {Application.ProductVersion}");
                var files = Directory.GetFiles(folderpath);
                foreach (var item in files)
                {
                    if (item.Contains("Zeno")) File.Delete(item);
                }

                return true;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private static Task WBL_Load()
        {
            if (!Directory.Exists(Path)) { Directory.CreateDirectory(Path); }
            string fullpath = $"{Path}\\{Words_file}";

            if (!File.Exists(fullpath))
            {
                var f = File.Create(fullpath);
                f.Close();

                Print("New default word black list file was created.");
            }
            else
            {
                WBL_List = File.ReadAllLines(fullpath).ToList();

                foreach (string item in WBL_List)
                {
                    if (item == "" || item == " ") { WBL_List.Remove(item); }
                }
            }

            return Task.CompletedTask;
        }
    }
}

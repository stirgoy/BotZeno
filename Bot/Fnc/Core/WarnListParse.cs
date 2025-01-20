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
        private List<string[]> WarnListParse()
        {
            string fullpath = System.IO.Path.Combine(Path, Warns_file);
            List<string[]> warnList = new List<string[]>();

            if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
            if (!File.Exists(fullpath))
            {
                var f = File.Create(fullpath);
                f.Close();
            }
            else
            {
                var warnings = File.ReadAllLines(fullpath).ToList();

                foreach (var item in warnings)
                {
                    warnList.Add(item.Split(','));
                }
                return warnList;
            }

            return warnList; // no results

        }


        private List<string> WarnListParse(List<string[]> WarnList)
        {
            List<string> warnList = new List<string>();

            foreach (var item in WarnList)
            {
                warnList.Add(string.Join(",", item));
            }

            return warnList;
        }
    }
}

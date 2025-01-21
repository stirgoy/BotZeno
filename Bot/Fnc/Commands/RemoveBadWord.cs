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
        private static Task RemoveBadWord(string word) // currently using slash command
        {
            bool deny = false;
            
            if (string.IsNullOrEmpty(word)) deny = true;
            if (word == " ") deny = true;
            if (!WBL_List.Contains(word)) deny = true;

            if (!deny)
            {
                WBL_List.Remove(word);
                string fullpath = $"{Path}\\{Words_file}";
                File.Delete(fullpath);
                File.WriteAllLines(fullpath, WBL_List);
                //log
            }
            else
            {
                //log
            }

            return Task.CompletedTask;
        }
    }
}

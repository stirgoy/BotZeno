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
        private static Task AddBadWord(string badWord) // currently using slash command
        {
            bool deny = false;
            if (string.IsNullOrEmpty(badWord)) { deny = true; }
            if (badWord == " ") { deny = true; }
            if (WBL_List.Contains(badWord)) { deny = true; }

            if (!deny)
            {
                WBL_List.Add(badWord);
                File.AppendAllLines($"{Path}\\{Words_file}", new string[] { badWord });
                //log it

            }
            else
            {
                //log it
            }

            return Task.CompletedTask;

        }
    }
}

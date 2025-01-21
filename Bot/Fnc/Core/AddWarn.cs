using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /*
         * type means reason:
         * 0 = uid
         * 1 = bad word
         * 2 = admin
         * 3 = 
         * 4 = 
         * 5 =
         */
        private Task AddWarn(ulong uid, int type)
        {

            //await Kuru.DownloadUsersAsync();
            var server_user = Kuru.GetUser(uid);
            bool exit = false;
            if (server_user == null) { exit = true; }
            if (type <= 0 || type >= 6) { exit = true; }

            if (exit)
            {
                Print($"ERROR: saving warning. User: {uid}  type: {type}"); 
                return Task.CompletedTask;
            }


            string suid = uid.ToString();
            string[] def = new string[] { uid.ToString(), "0", "0", "0", "0", "0" };
            List<string[]> WarnList = WarnListParse();


            if (WarnList.Count == 0) //1st warn on file
            {
                WarnList.Add(def);
            }

            bool saved = false;
            for (int i = 0; i < WarnList.Count; i++) //try update
            {
                if (WarnList[i][0] == suid)
                {
                    int numwarns = int.Parse(WarnList[i][type]);
                    numwarns++;
                    WarnList[i][type] = numwarns.ToString();
                    saved = true;
                }
            }

            if (!saved) //add new if not exists
            {                
                def[type] = "1";
                WarnList.Add(def);
            }

            var listtosave = WarnListParse(WarnList);
            
            string fullpath = System.IO.Path.Combine(Path, Warns_file);
            if (File.Exists(fullpath)) File.Delete(fullpath);
            File.WriteAllLines(fullpath, listtosave.ToArray());

            Print($"{uid}: has warned, type: {type}");

            return Task.CompletedTask;
        }

    }
}

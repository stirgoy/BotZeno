using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Command_badwordslist(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: true);

            string ms = "";
            if (WBL_List.Count == 0)
            {
                ms = "Nothing stored";
            }
            else
            {
                foreach (var item in WBL_List)
                {
                    ms += item + NL;
                }
            }

            await command.FollowupAsync("Bad words:" + NL + ms);

        }
    }
}

using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Command_warnlist(SocketSlashCommand command)
        {

            await command.DeferAsync(ephemeral: true);
            string desc = "I have no one on my warn list.";

            List<string[]> warns = WarnListParse();

            if ( warns.Count > 0 )
            {
                desc = "";
                //await Kuru.DownloadUsersAsync();

                foreach (var item in warns)
                {
                    var user = Kuru.GetUser(ulong.Parse(item[0]));
                    desc += $"{user.Mention} Bad words: {item[1]}, Admin: {item[2]}, x:{item[3]}, x:{item[4]}, x:{item[5]}{NL}";
                }
            }

            var emb = CreateEmbed("Warned users", desc, color: Color.Orange);
            
            await command.FollowupAsync("", embed: emb);


        }
    }
}

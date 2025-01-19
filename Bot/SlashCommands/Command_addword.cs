using Discord.WebSocket;
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
        private async Task Command_addword(SocketSlashCommand command)
        {

            var param = command.Data.Options.FirstOrDefault(opt => opt.Name == "newword");
            if (param?.Value is string badWord) { } else { return; } //exit on fail

            await command.DeferAsync(ephemeral:true);

            bool deny = false;
            if (string.IsNullOrEmpty(badWord)) { deny = true; }
            if (badWord == " ") { deny = true; }
            if (WBL_List.Contains(badWord)) { deny = true; }

            if (!deny)
            {
                WBL_List.Add(badWord);
                File.AppendAllLines($"{Path}\\{Words_file}", new string[] { badWord });
                

                var emb = CreateEmbed("Added", $"The word {badWord} has added to list.", color: Discord.Color.Green);
                var del = await command.FollowupAsync("", embed: emb);
                BorrarMsg(del);

            }
            else
            {
                var emb = CreateEmbed("Error", $"The word {badWord} was not added to list.",color: Discord.Color.Red);
                var del = await command.FollowupAsync("",embed:emb);
                BorrarMsg(del);

            }

        }
    }
}

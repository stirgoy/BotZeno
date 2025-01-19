using Discord.WebSocket;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Command_removeword(SocketSlashCommand command)
        {

            var param = command.Data.Options.FirstOrDefault(opt => opt.Name == "word");
            if (param?.Value is string badWord) { } else { return; } //exit on fail

            await command.DeferAsync(ephemeral: true);

            bool deny = false;
            if (string.IsNullOrEmpty(badWord)) { deny = true; }
            if (badWord == " ") { deny = true; }
            if (!WBL_List.Contains(badWord)) { deny = true; }

            if (!deny)
            {
                WBL_List.Remove(badWord);
                string fullpath = $"{Path}\\{Words_file}";
                File.Delete(fullpath);
                File.WriteAllLines(fullpath, WBL_List);


                var emb = CreateEmbed("Removed", $"The word {badWord} has removed from list.", color: Discord.Color.Green);
                var del = await command.FollowupAsync("", embed: emb);
                BorrarMsg(del);

            }
            else
            {
                var emb = CreateEmbed("Error", $"The word {badWord} was not removed from list.", color: Discord.Color.Red);
                var del = await command.FollowupAsync("", embed: emb);
                BorrarMsg(del);

            }

        }

    }
}

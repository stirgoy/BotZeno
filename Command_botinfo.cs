using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        async Task Command_botinfo(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: true);

            var suser = Kuru.GetUser(command.User.Id);
            string desc = $"I'm {Bot_Zeno.CurrentUser.GlobalName} is made with C# for **{Kuru.Name}**.";
            desc += $"Using [**Discord.net**](<https://github.com/discord-net/Discord.Net>) library.{NL}";
            desc += $"You can see my source code on [**BotZeno-Code**](<https://github.com/stirgoy/BotZeno>).{NL}";
            desc += $"Also i have a little guide of my commands and slash commands on [**BotZeno-Wiki**](<https://github.com/stirgoy/BotZeno/wiki>).{NL}";
            desc += $"My work on {Kuru.Name} is bring help to members and take care of new ones, also:{NL} * Can bring some information of mounts/minions of FFXIV{NL}* Send my guides via DM.{NL}* Post FFXIV Lodestone news.{NL}* Send a reminder to all interested on weekly cacpot {Emote.Bot.Cactuar}.";
            desc += $"* Send a DM at start of any server event to all interested members.{NL}";
            desc += $"-# Tell us something if you wants help or share any idea.";
            var emb = CreateEmbed($"Hi {suser.Nickname}!", desc, color: Color.Blue);
            var msg = await command.FollowupAsync("", embed: emb, ephemeral: true);
            BorrarMsg(msg, 60 * 2);
        }

    }
}

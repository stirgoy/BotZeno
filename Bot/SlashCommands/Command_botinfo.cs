using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zeno
{
    internal partial class Program
    {
        async Task Command_botinfo(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: true);

            var suser = Kuru.GetUser(command.User.Id);
            string desc = $"I'm {Bot_Zeno.CurrentUser.GlobalName} is made exlusively for **{Kuru.Name}** with:{NL}";
            desc += $"* [**C#**](<https://learn.microsoft.com/en-us/dotnet/csharp/>) {Emote.Bot.CSharp}.{NL}";
            desc += $"* [**FFXIV Collect API**](<https://documenter.getpostman.com/view/1779678/TzXzDHM1>){NL}";
            desc += $"* [**FFXIV Lodestone News API**](<https://documenter.getpostman.com/view/1779678/TzXzDHVk>){NL}";
            desc += $"* [**Discord.net library**](<https://docs.discordnet.dev/index.html>){NL}";
            desc += $"{NL}";
            desc += $"You can see my source code on [**BotZeno-Code**](<https://github.com/stirgoy/BotZeno>).{NL}";
            desc += $"Also i have a little guide of my commands and slash commands on [**BotZeno-Wiki**](<https://github.com/stirgoy/BotZeno/wiki>).{NL}";
            desc += $"My work on {Kuru.Name} is bring help to members and take care of new ones, also:{NL}";
            desc += $"* Can bring some information of mounts/minions of FFXIV{NL}";
            desc += $"* Send my guides via DM.{NL}";
            desc += $"* Post FFXIV Lodestone news.{NL}";
            desc += $"* Send a DM remembering to all interested on weekly cacpot {Emote.Bot.Cactuar}.{NL}";
            desc += $"* Send a DM at start of any server event to all interested members. {NL}";
            desc += $"-# Tell us something if you wants help with bot or share any idea.{NL}-# Ver. {Application.ProductVersion}";

            var emb = CreateEmbed($"Hi {suser.Nickname}!", desc, color: Color.Blue);
            var msg = await command.FollowupAsync("", embed: emb, ephemeral: true);
            BorrarMsg(msg, 60 * 2);
        }

    }
}

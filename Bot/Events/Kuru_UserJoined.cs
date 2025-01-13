using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Kuru_UserJoined(SocketGuildUser user)
        {

            Print($"{user.GlobalName} join server");
            string zenolog = $"Display name: {user.DisplayName + NL}Discord name: {user.Username + NL}Global name: {user.GlobalName + NL}Server name: {user.Nickname}";
            await ZenoLog($"{Emote.Bot.Sproud} NEW MEMBER!! {Emote.Bot.Sproud}", zenolog, user.GetAvatarUrl());

            var sproud = Kuru.GetRole(Role_sproud);
            await user.AddRoleAsync(sproud);

            await ZenoLog($"{Emote.Bot.LFP}Auto-role{Emote.Bot.LFP}", $"{user.Mention} has been added to {sproud.Mention}", user.GetAvatarUrl());
            Print($"{user.GlobalName} have new Role: {sproud.Name}");

#if DEBUG
            string rules = "https://discord.com/channels/1284428695309910016/1307843815830454352";
            string welcome = "https://discord.com/channels/1284428695309910016/";
#else
            string rules = "https://discord.com/channels/1181272231477780571/1181272232442478664";
            string welcome = "https://discord.com/channels/1181272231477780571/";
#endif

            welcome += Channel_hi;

            string t = $"Welcome to {Kuru.Name} discord server!!";
            string d = $"Hello {user.Mention}!! I'm {Bot_Zeno.CurrentUser.Mention} {Emote.Bot.Pepelove + NL}Its a pleasure have you with us, keep a look on {rules}.{NL}When you are done type **!hi** on {welcome} {Emote.Bot.Happytuff}";
            Embed embd = CreateEmbed(t, d);
            var channel = await user.CreateDMChannelAsync();
            await channel.SendMessageAsync("", embed: embd);

        }

    }
}

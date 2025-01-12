using Discord.WebSocket;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task UserLeftHandler(SocketGuild server, SocketUser user)
        {
            string zenolog = $"{Emote.Bot.Disconnecting} Discord name: {user.Username + NL}Global name: {user.GlobalName + NL} {Emote.Bot.Disconnecting}";
            Print($"{user.GlobalName} leave server.");
            await ZenoLog($"{Emote.Bot.Disconnecting} We lose a member..... {Emote.Bot.Disconnecting}", zenolog, user.GetAvatarUrl());

        }
    }
}

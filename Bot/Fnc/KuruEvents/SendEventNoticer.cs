using Discord;
using Discord.WebSocket;

namespace Zeno
{
    internal partial class Program
    {
        /********************
                SendEventNotice
        *////////////////////
        private async void SendEventNotice(string uid, SocketGuildEvent server_event)
        {
            await Kuru.DownloadUsersAsync();
            var user = Kuru.GetUser(ulong.Parse(uid));
            IDMChannel dm = await user.CreateDMChannelAsync();
            Embed emb = CreateEmbed($"Hey! {user.DisplayName} Wake up!!", $"{Emote.Bot.Online} The event: **{server_event.Name}**{NL}Just start now!!!! {Emote.Bot.Online}", miniImage: Kuru.IconUrl);
            await dm.SendMessageAsync("", embed: emb);

        }
    }
}

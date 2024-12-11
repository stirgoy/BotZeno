using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        private async Task Command_a_set_news(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var embD2 = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... " + Emote.Bot.Boss)
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: embD2, ephemeral: true);
                BorrarMsg(m);
                return;
            }

            Properties.Settings.Default.news_channel = selectedChannel.Id;
            Properties.Settings.Default.Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as ff news Channel");
            var t = Kuru.GetTextChannel(selectedChannel.Id);

            var embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly for ff news. " + Emote.Bot.Boss)
                .AddField("Channel", selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            var m2 = await command.FollowupAsync("", embed: embD, ephemeral: true);
            await ZenoLog($"{command.User.Mention} sets {t.Mention} as ff news channel.");
            BorrarMsg(m2);
        }
    }
}

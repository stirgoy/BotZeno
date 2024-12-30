using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Command_a_set_topics(SocketSlashCommand command)
        {
            var channelNc = command.Data.Options.FirstOrDefault(opt => opt.Name == "newsc");
            if (channelNc?.Value is SocketChannel selectedChannel) { } else { return; }//exit on fail
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var embD2 = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... " + Emote.Bot.Boss)
                .WithColor(Color.Red)
                .Build();
                var m = await command.FollowupAsync("", embed: embD2, ephemeral: true);
                BorrarMsg(m);
                return;
            }

            Config.Channels.Topics_channel = selectedChannel.Id;
            Config.Channels.Maintenance_channel = selectedChannel.Id;
            Config.Channels.Notices_channel = selectedChannel.Id;
            Config.Channels.Status_channel = selectedChannel.Id;
            Config.Channels.Update_channel = selectedChannel.Id;
            await Config_Save();

            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as ff news Channel");
            var t = Kuru.GetTextChannel(selectedChannel.Id);

            var embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly for ff news. " + Emote.Bot.Boss)
                .AddField("Channel", selectedChannel.ToString())
                .WithColor(Color.Green)
                .Build();
            var m2 = await command.FollowupAsync("", embed: embD, ephemeral: true);
            await ZenoLog($"{command.User.Mention} sets {t.Mention} as ff news channel.");
            BorrarMsg(m2);
        }
    }
}

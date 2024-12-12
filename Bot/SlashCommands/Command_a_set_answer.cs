using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {

        private async Task Command_a_set_answer(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            bool can = false;
            StringCollection channels = Properties.Settings.Default.TalkChannel;
            if (channels == null) can = false;
            foreach (var item in channels)
            {
                if (selectedChannel.Id == ulong.Parse(item))
                {
                    can = true;
                }
            }

            //EXISTE
            if (can)
            {

                RemoveTalkChannel(selectedChannel.Id.ToString());
                //msg 
                var talkc_embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription("So now i will ignore " + selectedChannel.ToString() + Emote.Bot.Boss)
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: talkc_embD, ephemeral: true);
                BorrarMsg(m);
                await ZenoLog($"{command.User.Mention} removes {selectedChannel} as talk channel.");
                return;
            }

            AddTalkChannel(selectedChannel.Id.ToString());

            Print("Channel set as talk channel: " + selectedChannel.ToString() + " - " + selectedChannel.Id.ToString());

            var talkc_emb = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Now i going answer on: " + selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            await command.FollowupAsync("", embed: talkc_emb, ephemeral: true);
            await ZenoLog($"{command.User.Mention} sets {selectedChannel} as talk channel.");

        }
    }
}

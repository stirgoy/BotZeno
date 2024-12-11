﻿using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        async Task Command_a_set_notices(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var logc_embD = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... " + Emote.Bot.Boss)
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: logc_embD, ephemeral: true);
                BorrarMsg(m);
                return;
            }

            Properties.Settings.Default.notices_channel = selectedChannel.Id;
            Properties.Settings.Default.Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as Notices Channel");
            var t = Kuru.GetTextChannel(selectedChannel.Id);

            var talkc_embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly. " + Emote.Bot.Boss)
                .AddField("Channel", t.Mention)
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            var m2 = await command.FollowupAsync("", embed: talkc_embD, ephemeral: true);
            BorrarMsg(m2);
            await ZenoLog($"{command.User.Mention} sets {t.Mention} as Notices channel.");
        }

    }
}
using Discord.WebSocket;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        private async Task Command_a_commands(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: true);

            var admin_emb = new EmbedBuilder()
            .WithTitle("Admin commands. " + Emote.Bot.Mentor)
            .WithDescription(Emote.Bot.Sproud + " Because you looks lost. " + Emote.Bot.Sproud)
            .WithColor(Color.Orange)
            .AddField("Sends a embeded message to the channel you says.", "/a_sendmsg", false)
            .AddField("I react to a message", "/a_react", false)
            .AddField("Show allowed answer channels", "/a_answer", false)
            .AddField("Edit my answer channels", "/a_set_answer", false)
            .AddField("Edit log channel", "/a_set_log", false)
            .AddField("Edit my ff news channel", "/a_set_news", false)
            .AddField("Edit my ff notices channel", "/a_set_notices", false)
            .AddField("Edit my ff status channel", "/a_set_status", false)
            .AddField("Edit my ff updates channel", "/a_set_update", false)
            .AddField("Edit my ff maintenance channel", "/a_set_maintenance", false)
            .AddField("Shows server info from user", "/a_userinfo", false)
            .AddField("Shows stored data on bot", "/a_show_stored", false)
            .WithFooter("Take care.")
            .WithTimestamp(DateTimeOffset.Now)
            .Build();
            var sm = await command.FollowupAsync("", embed: admin_emb, ephemeral: true);
            BorrarMsg(sm, 20);
        }
    }
}

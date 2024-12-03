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
            .AddField("Edit my answer channel", "/talkc", false)
            .AddField("Edit log channel", "/logc", false)
            .AddField("Edit my ff news channel", "/newsc", false)
            .AddField("Edit my ff status channel", "/statusc", false)
            .AddField("Edit my ff updates channel", "/updatec", false)
            .AddField("Edit my ff maintenance", "/maintenancec", false)
            .AddField("Show allowed answer channels", "/listc", false)
            .AddField("Shows server info from user", "/useri", false)
            .WithFooter("Take care.")
            .WithTimestamp(DateTimeOffset.Now)
            .Build();
            var sm = await command.FollowupAsync("", embed: admin_emb, ephemeral: true);
            BorrarMsg(sm, 20);
        }
    }
}

using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        private async Task Command_a_show_stored(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: true);

            string tc = "";
            StringCollection val = Properties.Settings.Default.TalkChannel;
            foreach (string item in val)
            {
                tc += item;
                if (val[val.Count - 1] != item)
                { tc += ", "; }
                else
                { tc += "."; }
            }


            Embed emb1 = new EmbedBuilder()
                        .WithTitle($"Persistent data stored from {Kuru.Name} server.:")
                        .AddField("TalkChannel", tc, true)
                        .AddField("log_channel", $"{Properties.Settings.Default.LogChannel}.", false)
                        .AddField("news_channel", $"{Properties.Settings.Default.news_channel}.", false)
                        .AddField("notices_channel", $"{Properties.Settings.Default.notices_channel}.", false)
                        .AddField("update_channel", $"{Properties.Settings.Default.update_channel}.", false)
                        .AddField("status_channel", $"{Properties.Settings.Default.status_channel}.", false)
                        .AddField("maintenance_channel", $"{Properties.Settings.Default.maintenance_channel}.", false)
                        .WithFooter("Wind-up Zeno♥ by Lulure Lure.")
                        .WithColor(Color.Orange)
                        .Build();

            Embed emb2 = new EmbedBuilder()
                        .WithTitle($"Persistent data stored from lodestonenews.com:")
                        .AddField("news_last_id", $"{Properties.Settings.Default.news_last_id}.", false)
                        .AddField("update_last_id", $"{Properties.Settings.Default.update_last_id}.", false)
                        .AddField("status_last_id", $"{Properties.Settings.Default.status_last_id}.", false)
                        .AddField("maintenance_last_id", $"{Properties.Settings.Default.maintenance_last_id}.", false)
                        .AddField("maintenance_last_game_id", $"{Properties.Settings.Default.maintenance_last_game_id}.", false)
                        .AddField("maintenance_last_mog_id", $"{Properties.Settings.Default.maintenance_last_mog_id}.", false)
                        .AddField("maintenance_last_lodestone_id", $"{Properties.Settings.Default.maintenance_last_lodestone_id}.", false)
                        .AddField("maintenance_last_companion_id", $"{Properties.Settings.Default.maintenance_last_companion_id}.", false)
                        .WithFooter("Wind-up Zeno♥ by Lulure Lure.")
                        .WithColor(Color.Orange)
                        .Build();


            var m1 = await command.FollowupAsync("", embed: emb1, ephemeral: true);
            var m2 = await command.FollowupAsync("", embed: emb2, ephemeral: true);
            
            BorrarMsg(m1, 60);
            BorrarMsg(m2, 60);


        }
    }
}

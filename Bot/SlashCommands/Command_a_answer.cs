using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Command_a_answer(SocketSlashCommand command)
        {
            try
            {
                await command.DeferAsync(ephemeral: true);

                string cnls = "";

                if (Config.Channels.TalkChannel == null)
                { Config.Channels.TalkChannel = new StringCollection(); }

                //setting texts
                if (Config.Channels.TalkChannel.Count != 0)
                {

                    foreach (string item in Config.Channels.TalkChannel)
                    {
                        var t = Kuru.GetTextChannel(ulong.Parse(item));
                        cnls += t.Mention;

                        if (item == Config.Channels.TalkChannel[Config.Channels.TalkChannel.Count - 1])
                        {
                            cnls += ".";
                        }
                        else
                        {
                            cnls += ", ";
                        }
                    }
                }
                else
                {
                    cnls = "No channel, use `/talkc` for set.";
                }



                string logc1 = $"Current log channel/s: {((Config.Channels.LogChannel == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}";
                string logc2 = (Config.Channels.LogChannel == 0) ? "No channel, use `/a_set_log` for set." : (Kuru.GetTextChannel(Config.Channels.LogChannel)).Mention;

                string newsc1 = $"Current ff news channel: {((Config.Channels.Topics_channel == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}";
                string newsc2 = (Config.Channels.Topics_channel == 0) ? "No channel, use `/a_set_news` for set." : (Kuru.GetTextChannel(Config.Channels.Topics_channel)).Mention;

                string updatec1 = $"Current ff update channel: {((Config.Channels.Update_channel == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}";
                string updatec2 = (Config.Channels.Update_channel == 0) ? "No channel, use `/a_set_update` for set." : (Kuru.GetTextChannel(Config.Channels.Update_channel)).Mention;

                string status1 = $"Current ff status channel: {((Config.Channels.Status_channel == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}";
                string status2 = (Config.Channels.Status_channel == 0) ? "No channel, use `/a_set_status` for set." : (Kuru.GetTextChannel(Config.Channels.Status_channel)).Mention;

                string maint1 = $"Current ff maintenance channel: {((Config.Channels.Maintenance_channel == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}";
                string maint2 = (Config.Channels.Maintenance_channel == 0) ? "No channel, use `/a_set_maintenance` for set." : (Kuru.GetTextChannel(Config.Channels.Maintenance_channel)).Mention;

                string notic1 = $"Current ff notices channel: {((Config.Channels.Notices_channel == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}";
                string notic2 = (Config.Channels.Notices_channel == 0) ? "No channel, use `/a_set_notices` for set." : (Kuru.GetTextChannel(Config.Channels.Notices_channel)).Mention;

                //embed
                var admin_embc = new EmbedBuilder()
                .WithTitle($"Admin settings status.")
                .WithDescription(Emote.Bot.Sproud + " Because you looks lost. " + Emote.Bot.Sproud)
                .WithColor(Color.Orange)
                .AddField($"Current answer channel/s: {((Config.Channels.TalkChannel.Count == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}", cnls, false)
                .AddField(logc1, logc2, false)
                .AddField(newsc1, newsc2, false)
                .AddField(notic1, notic2, false)
                .AddField(status1, status2, false)
                .AddField(updatec1, updatec2, false)
                .AddField(maint1, maint2, false)
                .WithFooter("Take care.")
                .Build();


                var m = await command.FollowupAsync("", embed: admin_embc, ephemeral: true);
                BorrarMsg(m, 20);

            }
            catch (Exception ex)
            {
                Print(ex.Message + " " + ex.Source + ex.TargetSite);
            }
        }
    }
}

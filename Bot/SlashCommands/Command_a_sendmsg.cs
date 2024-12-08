using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        private async Task Command_a_sendmsg(SocketSlashCommand command, string title, string desc, string picture, SocketTextChannel selectedChannel)
        {

            if (string.IsNullOrEmpty(title)) { return; }
            if (string.IsNullOrEmpty(desc)) { return; }


            try
            {

                await command.DeferAsync(ephemeral: false);

                //if (desc.Contains("\\n")) { desc = desc.Replace("\\n", Environment.NewLine); }
                desc = desc.Replace("\\n", Environment.NewLine);
                //string ico = _client.Guilds.First().IconUrl;

                //SocketGuildUser usr = null;
                SocketGuildUser usr = Kuru.Users.FirstOrDefault(item => item.Id == command.User.Id);
                /*
                foreach (var item in Kuru.Users)
                {
                    if (item.Id == command.User.Id)
                    {
                        usr = item;
                        break;
                    }
                }
                */
                string ico = command.User.GetDisplayAvatarUrl(size: 64);
                Embed talkc_embD;

                if (string.IsNullOrEmpty(picture))
                {
                    talkc_embD = new EmbedBuilder()
                        .WithTitle(title)
                        .WithDescription($"{desc}")
                        .WithColor(Color.Green)
                        .WithThumbnailUrl(ico)
                        .WithFooter(usr.DisplayName)
                        .Build();
                }
                else
                {
                    talkc_embD = new EmbedBuilder()
                        .WithTitle(title)
                        .WithDescription($"{desc}")
                        .WithColor(Color.Green)
                        .WithImageUrl(picture)
                        .WithThumbnailUrl(ico)
                        .WithFooter(usr.DisplayName)
                        .Build();
                }


                if (selectedChannel != null)
                {
                    await command.FollowupAsync($"Message sent to: {selectedChannel.Mention}", ephemeral: true);
                    await selectedChannel.SendMessageAsync("", embed: talkc_embD);
                }
                else
                {
                    await command.FollowupAsync("", embed: talkc_embD, ephemeral: false);
                }

            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }



        }
    }
}

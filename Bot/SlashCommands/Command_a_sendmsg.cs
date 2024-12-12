using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Command_a_sendmsg(SocketSlashCommand command, string title, string desc, string picture, SocketTextChannel selectedChannel)
        {

            if (string.IsNullOrEmpty(title)) { return; }
            if (string.IsNullOrEmpty(desc)) { return; }


            try
            {

                await command.DeferAsync(ephemeral: true);

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
                //string ico = command.User.GetDisplayAvatarUrl(size: 64);
                string ico = _client.CurrentUser.GetDisplayAvatarUrl(size: 64);
                Embed talkc_embD;

                if (string.IsNullOrEmpty(picture))
                {
                    talkc_embD = new EmbedBuilder()
                        .WithTitle(title)
                        .WithDescription($"{desc}")
                        .WithColor(Color.Green)
                        .WithThumbnailUrl(ico)
                        //.WithFooter(usr.DisplayName)
                        .WithFooter("Wind-UpZeno♥")
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
                        //.WithFooter(usr.DisplayName)
                        .WithFooter("Wind-UpZeno♥")
                        .Build();
                }


                if (selectedChannel != null)
                {
                    RestFollowupMessage m1 = await command.FollowupAsync($"Im typing on: {selectedChannel.Mention}", ephemeral: true);
                    //TODO haz magia
                    await selectedChannel.TriggerTypingAsync();
                    EditIt(m1, selectedChannel, talkc_embD);
                }
                else
                {
                    var del = await command.FollowupAsync("", embed: talkc_embD, ephemeral: true);
                    BorrarMsg(del, 15);
                }

            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }



        }


        async void EditIt(RestFollowupMessage mesg, SocketTextChannel selectedChannel, Embed talkc_embD)
        {
            await Task.Delay(4000);
            var mess = await selectedChannel.SendMessageAsync("", embed: talkc_embD);
            await mesg.ModifyAsync(msg => msg.Content = $"There is your message: https://discord.com/channels/{Kuru.Id}/{selectedChannel.Id}/{mess.Id} :smiling_imp:");
            BorrarMsg(mesg);
        }




    }
}

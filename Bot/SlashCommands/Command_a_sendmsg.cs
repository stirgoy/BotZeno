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
        private async Task Command_a_sendmsg(SocketSlashCommand command)
        {
            var t_title = command.Data.Options.FirstOrDefault(opt => opt.Name == "title");
            var t_desc = command.Data.Options.FirstOrDefault(opt => opt.Name == "msg");
            var t_pic = command.Data.Options.FirstOrDefault(opt => opt.Name == "picture");
            var t_channel = command.Data.Options.FirstOrDefault(opt => opt.Name == "channel");
            var t_foot = command.Data.Options.FirstOrDefault(opt => opt.Name == "mention");

            if (t_title?.Value is string title) { } else { return; }//exit on fail
            if (t_desc?.Value is string desc) { } else { return; }//exit on fail 
            if (t_pic?.Value is string picture) { } else { picture = ""; }
            if (t_channel?.Value is SocketTextChannel selectedChannel) { } else { selectedChannel = null; }
            if (t_foot?.Value is bool addFooter) { } else { addFooter = false; }

            if (string.IsNullOrEmpty(title)) { return; }
            if (string.IsNullOrEmpty(desc)) { return; }
            string c = (selectedChannel != null) ? selectedChannel.Name : "NoChannel";
            string p = (!string.IsNullOrEmpty(picture)) ? picture : "NoPicture";
            string footer = "Wind-UpZeno♥";
            string desFooter = "";
            string ico = Bot_Zeno.CurrentUser.GetDisplayAvatarUrl(size: 64);
            Print($"{command.User.Username} => a_sendmsg {title} - {desc} - {p} - {c} - Mention:{addFooter}");

            if (addFooter)
            {
#if DEBUG
                ulong[] roles = { 1285694137189797959, 1310733528660709398 };
#else
                ulong[] roles = { 1181272231695896672, 1215301157434687548, 1181272231695896668 };
#endif


                footer = "";
                foreach (ulong item in roles)
                {
                    var role = Kuru.GetRole(item);
                    desFooter += role.Mention;
                    desFooter += (item != roles.Last()) ? ", " : ".";
                }

                desFooter = $"{NL} {NL}-# " + desFooter;
                desc += desFooter;

            }

            ico = Kuru.IconUrl;

            try
            {
                await command.DeferAsync(ephemeral: true);

                desc = desc.Replace("\\n", NL);
                Embed embd;
                SocketGuildUser usr = Kuru.GetUser(command.User.Id);

                if (string.IsNullOrEmpty(picture))
                {
                    embd = CreateEmbed(title, desc, footer, ico, color: Color.Green);
                }
                else
                {
                    embd = CreateEmbed(title, desc, footer, ico, picture, Color.Green);
                }




                if (selectedChannel != null)
                {
                    RestFollowupMessage m1 = await command.FollowupAsync($"Im typing on: {selectedChannel.Mention}", ephemeral: true);
                    await selectedChannel.TriggerTypingAsync();
                    SendMsgDelayed(m1, selectedChannel, embd);
                }
                else
                {
                    var del = await command.FollowupAsync("", embed: embd, ephemeral: true);
                    BorrarMsg(del, 15);
                }

            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }



        }


        async void SendMsgDelayed(RestFollowupMessage mesg, SocketTextChannel selectedChannel, Embed talkc_embD)
        {
            await Task.Delay(4000);
            var mess = await selectedChannel.SendMessageAsync("", embed: talkc_embD);
            await mesg.ModifyAsync(msg => msg.Content = $"There is your message: https://discord.com/channels/{Kuru.Id}/{selectedChannel.Id}/{mess.Id} :smiling_imp:");
            BorrarMsg(mesg);
        }




    }
}

using Discord;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Command_a_set_log(SocketSlashCommand command)
        {
            var channelL = command.Data.Options.FirstOrDefault(opt => opt.Name == "channell");
            if (channelL?.Value is SocketChannel selectedChannel) { } else { return; }//exit on fail

            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                Embed logc_embD = CreateEmbed("Settings Error", "Something is wrong... " + Emote.Bot.Boss,color: Color.Red);
                
                var m = await command.FollowupAsync("", embed: logc_embD, ephemeral: true);
                BorrarMsg(m);
                return;
            }

            Config.Channels.LogChannel = selectedChannel.Id;
            await Config_Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as Log Channel");
            var t = Kuru.GetTextChannel(selectedChannel.Id);

            Embed talkc_embD = CreateEmbedField_1("Settings", "Channel saved correctly. " + Emote.Bot.Boss, "Channel",t.Mention , color: Color.Red);
                        
            var m2 = await command.FollowupAsync("", embed: talkc_embD, ephemeral: true);
            BorrarMsg(m2);
            await ZenoLog($"{command.User.Mention} sets {t.Mention} as log channel.");
        }
    }
}

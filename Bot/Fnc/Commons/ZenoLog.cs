using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /********************
            ZenosLog
        *//////////////////// server log, requieres channel id on config
        private static async Task ZenoLog(string message)
        {
            if (Config.ZenoLog)
            {
                SocketTextChannel canal = Kuru.GetTextChannel(Config.Channels.LogChannel);
                if (canal != null)
                {
                    await canal.SendMessageAsync(message);
                }
            }
        }


        //////////////////// same but with embed
        private static async Task ZenoLog(string title, string message, string img = "")
        {
            if (Config.ZenoLog)
            {
                SocketTextChannel canal = Kuru.GetTextChannel(Config.Channels.LogChannel);
                if (canal != null)
                {
                    Embed emb = CreateEmbed(
                        title,
                        message,
                        miniImage: img,
                        color: Color.Orange);

                    await canal.SendMessageAsync("", embed: emb);
                }
            }
        }
    }
}

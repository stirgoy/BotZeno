using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /********************
                MassDelete
        *//////////////////// be careful
        private static async void MassDelete(SocketMessage message, int howmany)
        {
            var channel = message.Channel;
            var mensajes = await channel.GetMessagesAsync(howmany).FlattenAsync();
            foreach (var item in mensajes)
            {
                await item.DeleteAsync();
                await Task.Delay(750);
            }

            string log = $"{message.Author.Mention} used mass delete {howmany} times on {Kuru.GetTextChannel(channel.Id).Mention}";
            Print(log);
            await ZenoLog(log);

        }
    }
}

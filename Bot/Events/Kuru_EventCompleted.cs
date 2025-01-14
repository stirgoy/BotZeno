using Discord.WebSocket;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Kuru_EventCompleted(SocketGuildEvent server_event)
        {

            Print("Event completed");
            await DeleteEvent(server_event);
        }
    }
}

using Discord.WebSocket;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Kuru_EventCanceled(SocketGuildEvent server_event)
        {
            Print("Event canceled");
            await DeleteEvent(server_event);

        }
    }
}

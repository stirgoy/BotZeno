using Discord.WebSocket;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Kuru_EventCanceled(SocketGuildEvent server_event)
        {
            Print("Event canceled");
            await Delete_Event(server_event);

        }
    }
}

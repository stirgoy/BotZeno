using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Kuru_EventStarted(SocketGuildEvent server_event)
        {
            Print("Event started");

            var data = GetEvent(server_event.Id.ToString());

            foreach (string item in data)
            {
                if (item != data.First()) //need skip 1st because its event id
                {
                    SendEventNotice(item, server_event);
                    await Task.Delay(100);
                }
            }
        }
    }
}

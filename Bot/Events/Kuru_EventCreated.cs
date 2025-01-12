using Discord.WebSocket;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Kuru_EventCreated(SocketGuildEvent server_event)
        {
            Print("New event added on server");

            string[] newEvent = new string[] { server_event.Id.ToString() };
            Config.Events_Noticed.Add(newEvent);
            await Config_Save();

        }
    }
}

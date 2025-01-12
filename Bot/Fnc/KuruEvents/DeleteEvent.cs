using Discord.WebSocket;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /********************
                Delete_Event
        *////////////////////
        private async Task Delete_Event(SocketGuildEvent server_event)
        {
            Print("Event deletd from config");

            foreach (var item in Config.Events_Noticed)
            {
                if (item[0] == server_event.Id.ToString())
                {
                    Config.Events_Noticed.Remove(item);
                    await Config_Save();
                    break;
                }
            }
        }
    }

}

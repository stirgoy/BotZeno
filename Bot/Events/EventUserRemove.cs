using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task EventUserRemove(Cacheable<SocketUser, RestUser, IUser, ulong> user, SocketGuildEvent server_event)
        {
            Print($"New user removed {user.Id} on event {server_event.Id}");

            List<string[]> evnts = Config.Events_Noticed;

            for (int i = 0; i < evnts.Count; i++)
            {
                if (evnts[i][0] == server_event.Id.ToString())
                {
                    List<string> newlist = evnts[i].ToList();
                    if (newlist.Contains(user.Id.ToString()))
                    {
                        newlist.Remove(user.Id.ToString());
                        evnts[i] = newlist.ToArray();
                        Config.Events_Noticed = evnts;
                        await Config_Save();
                        break;
                    }
                }
            }
        }
    }
}

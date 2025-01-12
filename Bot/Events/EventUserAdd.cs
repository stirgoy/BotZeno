using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task EventUserAdd(Cacheable<SocketUser, RestUser, IUser, ulong> user, SocketGuildEvent server_event)
        {
            Print($"New user added {user.Id} on event {server_event.Id}");

            string[] event_data = GetEvent(server_event.Id.ToString());
            List<string[]> evnts = Config.Events_Noticed;

            if (!event_data.Contains(user.Id.ToString()))
            {
                for (int i = 0; i < evnts.Count; i++)
                {
                    if (evnts[i][0] == server_event.Id.ToString())
                    {
                        List<string> newlist = evnts[i].ToList();
                        newlist.Add(user.Id.ToString());
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

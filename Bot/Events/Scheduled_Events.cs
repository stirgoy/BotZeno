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

        private async Task Kuru_EventCreated(SocketGuildEvent server_event)
        {
            Print("New event added on server");

            string[] newEvent = new string[] { server_event.Id.ToString() };
            Config.Events_Noticed.Add(newEvent);
            await Config_Save();

        }




        private async Task EventUserRemove(Cacheable<SocketUser, RestUser, IUser, ulong> user, SocketGuildEvent server_event)
        {
            Print($"New user removed on event {user.Id}");

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

        private async Task Kuru_EventCanceled(SocketGuildEvent server_event)
        {
            Print("Event canceled");
            await Delete_Event(server_event);

        }

        private async Task Kuru_EventCompleted(SocketGuildEvent server_event)
        {

            Print("Event completed");
            await Delete_Event(server_event);
        }


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

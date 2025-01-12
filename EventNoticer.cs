using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        internal async void EventNoticer()
        {
            //wait for server connection
            while (Bot_Zeno.ConnectionState != Discord.ConnectionState.Connected)
            {
                await Task.Delay(5000);
            }

            while (true) //i hate perma loops
            {
                if (Kuru.Events.Count > 0)
                { //have event/s

                    foreach (var item in Kuru.Events)
                    {
                        /*
                        if (!Config.Events_Noticed.Contains(item.Id.ToString())) //check if i noticed this event
                        {
                            if (item.Status == Discord.GuildScheduledEventStatus.Active) //check if is active
                            {
                                var cevent = Kuru.GetEvent(item.Id);
                                int numusers = 0;
                                if (cevent.UserCount != null) { numusers = (int)item.UserCount; } //get number of users sub

                                if (numusers > 0)
                                {
                                    await Kuru.DownloadUsersAsync();

                                    var sendto = await item.GetUsersAsync(numusers); //get users

                                    foreach (var user in sendto)
                                    {
                                        var dm_channel = await user.CreateDMChannelAsync(); //get dm
                                        await dm_channel.SendMessageAsync("need put something here"); //send notices
                                        await Task.Delay(200); //server will appreciate this
                                    }
                                }

                                Config.Events_Noticed.Add(item.Id.ToString()); //add to noticed events list
                                await Config_Save();

                            }
                        }
                        */
                    }
                }
                else if (Kuru.Events.Count == 0)
                {
                /*
                    Config.Events_Noticed.Clear();
                    await Config_Save();
                */
                }



                await Task.Delay(5000); //check each MIN * 60 * 1000
            }


        }
    }
}

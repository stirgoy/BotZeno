using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {

        const int hour = 20;
        const DayOfWeek dow = DayOfWeek.Saturday;

        public async void Cacpot()
        {
            if (Config.LastCacpot == "") //no data, lets take same day
            {
                DateTime now = DateTime.Now;
                string snow = now.ToString("dd/MM/yyyy HH:mm");
                await Config_Save();
                Config.LastCacpot = snow;
            }

            while (true) //hate you
            {
                DateTime now = DateTime.Now;
                if (now.DayOfWeek == dow && now.Hour == hour)
                { //is saturday and its 20:00

                    DateTime last = DateTime.Parse(Config.LastCacpot);

                    if (last.Date < now.Date)
                    {
                        Print("CACPOT READY SENDING MESSAGES!!!!");
                        // last notice < today for prevent multi advise
                        await Kuru.DownloadUsersAsync(); //download all server users

                        foreach (string user in Config.CacpotIds)
                        {
                            ulong uid = ulong.Parse(user);
                            Kuru.GetUsersAsync(); //hmmm...
                            SocketGuildUser bdy = Kuru.GetUser(uid);
                            IDMChannel dm = await bdy.CreateDMChannelAsync();
                            string ce = Emote.Bot.Cactuar;

                            Embed emb = CreateEmbed(
                                $"{ce + ce} CACPOT TIME!!!!! {ce + ce}",
                                $"I wish you luck with {ce + NL}But something tells me you only going get consolation prize {Emote.Bot.Pepeshookt}",
                                miniImage: "https://i.postimg.cc/13dZCL3P/zenosxD.png",
                                color: Color.Green);
                                

                            await dm.SendMessageAsync($"", embed: emb);
                            await dm.SendMessageAsync($"{Emote.Bot.Pepo_laugh}");

                            Print($"Cacpot DM sent to: {bdy.GlobalName}");
                        }

                        Config.LastCacpot = now.ToString("dd/MM/yyyy HH:mm");
                        await Config_Save();

                    }

                }

                await Task.Delay(1 * 60 * 1000); //MIN * 60 * 1000
            }

        }
    }
}

using Discord.Rest;
using Discord.WebSocket;
using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace Zeno
{
    internal partial class Program
    {

        /********************
            GetLastNewsIds
        *//////////////////// Used for make new default config file and don't bulk all news
        private static async Task GetLastNewsIds()
        {
            Config.Ids.Topics_last_id = await GetLNId(XIVLN.APIs.Topics);
            Config.Ids.Notices_last_id = await GetLNId(XIVLN.APIs.Notices);
            Config.Ids.Status_last_id = await GetLNId(XIVLN.APIs.Status);
            Config.Ids.Update_last_id = await GetLNId(XIVLN.APIs.Updates);
            Config.Ids.Maintenance_last_id = await GetLNId(XIVLN.APIs.Maintenance);
            string current = await GetLNId(XIVLN.APIs.MaintenanceCurrent, true);
            string[] arr_curr = current.Split(',');
            Config.Ids.Maintenance_last_game_id = arr_curr[0];
            Config.Ids.Maintenance_last_lodestone_id = arr_curr[1];
            Config.Ids.Maintenance_last_mog_id = arr_curr[2];
            Config.Ids.Maintenance_last_companion_id = arr_curr[3];

        }

        /********************
                GetLNId
        *//////////////////// retrieves last id from lodestonenews api
        private static async Task<string> GetLNId(string api, bool maintenance = false)
        {
            HttpClient client = new HttpClient();
            string jsonCommon = await client.GetStringAsync(api);

            if (maintenance)
            {
                MaintenanceRoot newsListD = JsonConvert.DeserializeObject<MaintenanceRoot>(jsonCommon);
                string game = (newsListD.Game.Count == 0) ? "0" : newsListD.Game[0].Id;
                string lode = (newsListD.Lodestone.Count == 0) ? "0" : newsListD.Lodestone[0].Id;
                string mog = (newsListD.Mog.Count == 0) ? "0" : newsListD.Mog[0].Id;
                string comp = (newsListD.Companion.Count == 0) ? "0" : newsListD.Companion[0].Id;
                string ret = $"{game},{lode},{mog},{comp}";
                return ret;

            }
            else
            {
                var newsListD = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonCommon);
                if (newsListD.Count > 0)
                {
                    return newsListD[0].Id;
                }
                else
                {
                    return "0";
                }

            }

        }

        /********************
                UnixTime
        *//////////////////// returns cord timestamp
        private static string UnixTime(DateTime date, string mode = "R")
        {
            long unixTimestamp = new DateTimeOffset(date).ToUnixTimeSeconds();
            return $"<t:{unixTimestamp}:{mode}>";

        }

        /********************
                Print
        *//////////////////// console log
        private static void Print(string line, bool newLine = true, bool showname = true)
        {
            //Console bot print with timespamp
            string time_stamp, h = "", m = "", s = "", ms = "";
            DateTime n = DateTime.Now;
            //0 on left
            if (n.Hour < 10) { h = "0"; }
            if (n.Minute < 10) { m = "0"; }
            if (n.Second < 10) { s = "0"; }
            if (n.Millisecond < 100) { ms = "0"; }
            if (n.Millisecond < 10) { ms = "00"; }

            //getting values
            h += n.Hour.ToString();
            m += n.Minute.ToString();
            s += n.Second.ToString();
            ms += n.Millisecond.ToString();
            //making timestamp
            time_stamp = h + ":" + m + ":" + s + "." + ms + " "; //format 00:00:00

            if (!Skiplog)
            {
                if (!Directory.Exists($"{Path}\\Logs")) { Directory.CreateDirectory($"{Path}\\Logs"); }
                string f = DateTime.Now.ToString("ddMM");
                File.AppendAllText($"{Path}\\Logs\\{f}", $"{time_stamp}{line}{NL}");
            }
#if !DEBUG
            if (!Config.ConsoleLog) { return; }
#endif
            if (showname) { line = "Zeno♥ - " + line; }
            if (newLine)
            {
                if (showname)
                {
                    Console.WriteLine(time_stamp + " " + line);
                }
                else
                {
                    Console.WriteLine(" " + line);
                }
            }
            else
            {
                if (showname)
                {
                    Console.Write(time_stamp + " " + line);
                }
                else
                {
                    Console.Write(line);
                }
            }
        }

        /********************
                MassDelete
        *//////////////////// be careful
        private static async void MassDelete(SocketMessage message, int howmany)
        {
            var channel = message.Channel;
            var mensajes = await channel.GetMessagesAsync(howmany).FlattenAsync();
            foreach (var item in mensajes)
            {
                await item.DeleteAsync();
                await Task.Delay(750);
            }

            string log = $"{message.Author.Mention} used mass delete {howmany} times on {Kuru.GetTextChannel(channel.Id).Mention}";
            Print(log);
            await ZenoLog(log);

        }


        /********************
            ResetApp
        *//////////////////// app reload
        private static void ResetApp()
        {
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(exePath);
            Environment.Exit(0);
        }

        /********************
            Reconnect
        *//////////////////// client reconnection
        private static async void Reconnect()
        {
            try
            {
                await Bot_Zeno.StopAsync();
                await Task.Delay(5000);
                await Bot_Zeno.StartAsync();
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

        /********************
            SendBotEmotes
        *//////////////////// bot emotes bulk
        private static async void SendBotEmotes(ISocketMessageChannel channel)
        {
            string emot = "";
            List<string> msgs = new List<string>();
            var properties = typeof(Emote.Bot).GetProperties(BindingFlags.Public | BindingFlags.Static);

            int c = 1;
            int m = 20;

            foreach (var property in properties)
            {
                var value = property.GetValue(null);
                //StringCollection anims = new StringCollection { "Pepo_laugh" };

                emot += value.ToString();
                if (property != properties.Last()) { emot += " "; }
                if (c == m)
                {
                    msgs.Add(emot);
                    c = 0;
                    emot = "";

                }
                c++;
            }

            if (msgs.Last() != emot) { msgs.Add(emot); }

            foreach (var item in msgs)
            {
                if (item != "")
                {
                    await channel.SendMessageAsync(item);
                }

            }
        }

    }
}

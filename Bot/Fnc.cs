using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        private async Task SetDefKuru()
        {
            //TODO retrieve last id
            //lodestone ids
            if (Properties.Settings.Default.news_last_id == "0") { Properties.Settings.Default.news_last_id = await GetLNId(XIV.APIs.Topics); }
            if (Properties.Settings.Default.notices_last_id == "0") { Properties.Settings.Default.notices_last_id = await GetLNId(XIV.APIs.Notices); }
            if (Properties.Settings.Default.status_last_id == "0") { Properties.Settings.Default.status_last_id = await GetLNId(XIV.APIs.Status); }
            if (Properties.Settings.Default.update_last_id == "0") { Properties.Settings.Default.update_last_id = await GetLNId(XIV.APIs.Updates); }
            if (Properties.Settings.Default.maintenance_last_id == "0") { Properties.Settings.Default.maintenance_last_id = await GetLNId(XIV.APIs.Maintenance); }
            if (Properties.Settings.Default.maintenance_last_game_id == "0") { Properties.Settings.Default.maintenance_last_game_id = "0"; }
            if (Properties.Settings.Default.maintenance_last_mog_id == "0") { Properties.Settings.Default.maintenance_last_mog_id = "0"; }
            if (Properties.Settings.Default.maintenance_last_lodestone_id == "0") { Properties.Settings.Default.maintenance_last_lodestone_id = "0"; }
            if (Properties.Settings.Default.maintenance_last_companion_id == "0") { Properties.Settings.Default.maintenance_last_companion_id = "0"; }

            //channels
#if !DEBUG
            /********************
            Kuru default settings
            *////////////////////
            /*
            Properties.Settings.Default.news_channel = 1205502111979151420;
            Properties.Settings.Default.notices_channel = 1205502111979151420;
            Properties.Settings.Default.status_channel = 1205502111979151420;
            Properties.Settings.Default.update_channel = 1205502111979151420;
            Properties.Settings.Default.maintenance_channel = 1205502111979151420;
            Properties.Settings.Default.LogChannel = 1181272233260368010;
            */
            if (Properties.Settings.Default.news_channel == 0) { Properties.Settings.Default.news_channel = 1205502111979151420; }
            if(Properties.Settings.Default.notices_channel == 0) { Properties.Settings.Default.notices_channel = 1205502111979151420; }
            if(Properties.Settings.Default.status_channel == 0) { Properties.Settings.Default.status_channel = 1205502111979151420; }
            if(Properties.Settings.Default.update_channel == 0) { Properties.Settings.Default.update_channel = 1205502111979151420; }
            if(Properties.Settings.Default.maintenance_channel == 0) { Properties.Settings.Default.maintenance_channel = 1205502111979151420; }
            if(Properties.Settings.Default.LogChannel == 0) { Properties.Settings.Default.LogChannel = 1181272233260368010; }
            if(Properties.Settings.Default.TalkChannel == null) { Properties.Settings.Default.TalkChannel = new StringCollection { "1181272233260368010", "1205502111979151420", "1315324417475219518" }; }
            if(Properties.Settings.Default.TalkChannel.Count == 0) { Properties.Settings.Default.TalkChannel = new StringCollection{ "1181272233260368010", "1205502111979151420", "1315324417475219518" }; }
#endif
            Properties.Settings.Default.Save();
        }

        /********************
                MassDelete
        *////////////////////
        private async void MassDelete(SocketMessage message, int howmany)
        {
            var channel = message.Channel;
            var mensajes = await channel.GetMessagesAsync(howmany).FlattenAsync();
            foreach (var item in mensajes)
            {
                await item.DeleteAsync();
                await Task.Delay(500);
            }

        }

        /********************
                GetLNId
        *////////////////////
        private async Task<string> GetLNId(string api, bool curr_maintenance = false)
        {
            HttpClient client = new HttpClient();
            string jsonCommon = await client.GetStringAsync(api);
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

        /********************
                Reconnect
        *////////////////////
        private async void Reconnect()
        {
            try
            {
                await _client.StopAsync();
                await Task.Delay(5000);
                await _client.StartAsync();
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

        /********************
                UnixTime
        *////////////////////
        private static string UnixTime(DateTime date, string mode = "R")
        {
            long unixTimestamp = new DateTimeOffset(date).ToUnixTimeSeconds();
            return $"<t:{unixTimestamp}:{mode}>";

        }
        /********************
         Check_Allowed_Channel
        *////////////////////
        private bool Check_Allowed_Channel(ISocketMessageChannel channel_to_check)
        {
            //Chech if can talk on channel

            StringCollection channels = Properties.Settings.Default.TalkChannel;
            if (channels == null) return false;
            foreach (var item in channels)
            {
                if (channel_to_check.Id == ulong.Parse(item))
                {
                    return true;
                }
            }

            return false;
        }


        /********************
         Check_Allowed_Channel
        *////////////////////
        private bool Check_Allowed_Channel(SocketChannel channel_to_check)
        {
            //Chech if can talk on channel

            StringCollection channels = Properties.Settings.Default.TalkChannel;
            if (channels == null) return false;
            foreach (var item in channels)
            {
                if (channel_to_check.Id == ulong.Parse(item))
                {
                    return true;
                }
            }


            return false;
        }


        /********************
                Print
        *////////////////////
        private static void Print(string line, bool newLine = true, bool showname = true)
        {
            if (!_consolePrint) { return; }
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
            AddTalkChannel
        *////////////////////
        private void AddTalkChannel(String channel)
        {
            //Add allowed talk channel
            StringCollection channels = Properties.Settings.Default.TalkChannel;
            if (channels == null) channels = new StringCollection();
            channels.Add(channel);
            Properties.Settings.Default.TalkChannel = channels;
            Properties.Settings.Default.Save();
        }

        /********************
            RemoveTalkChannel
        *////////////////////
        private void RemoveTalkChannel(String channel)
        {
            //Remove allowed talk channel
            StringCollection channels = Properties.Settings.Default.TalkChannel;
            StringCollection new_channels = new StringCollection();
            foreach (var item in channels)
            {
                if (item != channel)
                {
                    new_channels.Add(item);
                }
            }

            Properties.Settings.Default.TalkChannel = new_channels;
            Properties.Settings.Default.Save();
        }


        /********************
            ZenosLog
        *////////////////////
        private async Task ZenoLog(string message)
        {
            if (_ZenoLog)
            {
                SocketTextChannel canal = Kuru.GetTextChannel(Properties.Settings.Default.LogChannel);
                await canal.SendMessageAsync(message);
            }
        }


        /********************
            borrar_msg
        *////////////////////
        private void BorrarMsg(RestFollowupMessage botMessage, int tiempo = 8)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(tiempo * 1000);
                try { await botMessage.DeleteAsync(); } catch (Exception ex) { Print(ex.Message); }

            });
        }

    }
}

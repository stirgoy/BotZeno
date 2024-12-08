using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        /********************
                FFXIVModeHandler
        *////////////////////
        //Handler for singe seeing of news
        private async Task FFXIVModeHandler(SocketSlashCommand command, string mode)
        {
            await command.DeferAsync(ephemeral: false);

            try
            {
                int def = 1; int min = 1; int max = 5;

                var cantidadOption = command.Data.Options.FirstOrDefault(opt => opt.Name == "number");
                int cantidad = cantidadOption?.Value is long value ? (int)value : def;
                if (cantidad > max) { cantidad = max; }
                if (cantidad <= 0) { cantidad = min; }

                //maintenance >> need struct
                //news
                //status
                //updates
                List<Embed> news = await LodestoneHandler(cantidad, mode);

                string lst;
                switch (mode)
                {
                    case "news":                        
                        lst = "News " + Emote.Bot.LTopics;
                        break;
                    case "status":
                        lst = "Status "+ Emote.Bot.LStatus;
                        break;
                    case "update":
                        lst = "Update " + Emote.Bot.LUpdate;
                        break;
                    case "maintenance":
                        lst = "Maintenance " + Emote.Bot.LMaintenance;
                        break;
                    default: 
                        lst = "News " + Emote.Bot.LTopics;
                        break;
                }

                await command.FollowupAsync(" Final Fantasy XIV - " + lst, embeds: news.ToArray(), ephemeral: false);

            }
            catch (Exception ex)
            {
                Print($"Error: {ex.Message}");
            }
        }

        /********************
                UnixTime
        *////////////////////
        private string UnixTime(DateTime date, string mode = "R")
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
        private void Print(string line, bool showname = true)
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
            Console.WriteLine(time_stamp + " " + line);
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
        /*
        private void GZeno()
        {
            _ = Task.Run(async () =>
            {
            
                await _client.StopAsync();
                await _client.LogoutAsync();
                await Task.Delay(3000);
                await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("ZenosT", EnvironmentVariableTarget.User));
                await _client.StartAsync();
            
            });
        }
        */


    }
}

using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Begu
{
    //TODO Clean this ****
    internal partial class Program
    {
        private void Check_FF_updates()
        {


            Updater = Task.Run(async () =>
            {

                int trys = 0;
                int maxTrys = 5;
                int time_multipler = 60000;
                int each = _ffNewsUpdaterTimer;
                bool run = _ffNewsUpdater;


                if (!run)
                {
                    Print("Check_FF_updates Disabled.");
                }

                while (run)
                {
                    each = _ffNewsUpdaterTimer; //keep for retry
                    time_multipler = 60000; //keep for retry
                    //time_multipler = 6000; //TEST

                    if (Properties.Settings.Default.news_channel == 0 || Properties.Settings.Default.status_channel == 0 || Properties.Settings.Default.update_channel == 0 || Properties.Settings.Default.maintenance_channel == 0)
                    {
                        try
                        {
                            SocketTextChannel _channel = (SocketTextChannel)Kuru.GetChannel(Properties.Settings.Default.LogChannel);
                            await _channel.SendMessageAsync($"Final Fantasy XIV News is stoped because i miss where i can put the news, trying again on {each} minutes." +
                                $"{Environment.NewLine} Set it usung `/a_set_news` `/a_set_status` `/a_set_maintenance` `/a_set_status`");

                        }
                        catch (Exception ex)
                        {
                            Print(ex.Message);
                        }
                    }
                    else
                    {

                        try
                        {
                            string cmsg = "";
                            string mmsg = "";
                            string apiUrl = "https://lodestonenews.com/news/topics?locale=eu";
                            HttpClient client = new HttpClient();
                            string jsonCommon = "";
                            bool empty = true;

                            //LodestoneNews
                            List<LodestoneNews> newsListD = new List<LodestoneNews>();
                            jsonCommon = await client.GetStringAsync(apiUrl);
                            if (string.IsNullOrEmpty(jsonCommon)) { Print("NEWS ARE EMPTY OR NULL"); }
                            newsListD = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonCommon);
                            empty = (newsListD == null || newsListD.Count == 0);
                            bool havenews = !(Properties.Settings.Default.news_last_id == newsListD.First().Id);

                            if (!empty && havenews && Properties.Settings.Default.news_last_id.Length < 5)// first time :D lets don't spam channels
                            {
                                if (newsListD.Count >= 2)
                                {
                                    Properties.Settings.Default.news_last_id = newsListD[1].Id;
                                    Properties.Settings.Default.Save();
                                    
                                }
                            }

                            cmsg += $"News:{!empty && havenews} | ";
                            if (!empty && havenews)
                            {

                                SocketTextChannel news_channel = Kuru.GetTextChannel(Properties.Settings.Default.news_channel);


                                bool first = true;
                                string bera = "";
                                
                                foreach (var item in newsListD)
                                {
                                    
                                    if (bera == item.Id) {  break; } // don't show again
                                    if (first)
                                    {
                                        bera = Properties.Settings.Default.news_last_id; // i need that xD
                                        first = false;
                                        Properties.Settings.Default.news_last_id = item.Id;
                                        Properties.Settings.Default.Save();
                                    }

                                    var embed = new EmbedBuilder()
                                        .WithTitle(item.Title)
                                        .WithUrl(item.Url)
                                        .WithTimestamp(item.Time)
                                        .WithImageUrl(item.Image)
                                        .WithDescription(item.Description)
                                        .WithColor(Color.Blue)
                                        .WithFooter("From: Lodestone")
                                        .Build();

                                    if (!_ffNewsUpdaterPrepareIDS)
                                    {
                                    }
                                        await news_channel.SendMessageAsync("Final Fantasy XIV - News " + Emote.Bot.LTopics, embed: embed);
                                }

                            }


                            //status
                            apiUrl = "https://lodestonenews.com/news/status?locale=eu";
                            client = new HttpClient();
                            jsonCommon = "";
                            empty = true;
                            newsListD = new List<LodestoneNews>();

                            jsonCommon = await client.GetStringAsync(apiUrl);
                            if (string.IsNullOrEmpty(jsonCommon)) { Print("NEWS ARE EMPTY OR NULL"); }
                            newsListD = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonCommon);
                            empty = (newsListD == null || newsListD.Count == 0);
                            havenews = !(Properties.Settings.Default.status_last_id == newsListD.First().Id);

                            if (!empty && havenews && Properties.Settings.Default.status_last_id.Length < 5)// first time :D lets don't spam channels
                            {
                                if (newsListD.Count >= 2)
                                {
                                    Properties.Settings.Default.status_last_id = newsListD[1].Id;
                                    Properties.Settings.Default.Save();
                                }
                            }

                            cmsg += $"Status:{!empty && havenews} | ";

                            if (!empty && havenews)
                            {

                                SocketTextChannel news_channel = Kuru.GetTextChannel(Properties.Settings.Default.status_channel);
                                bool first = true;
                                string bera = "";

                                foreach (var item in newsListD)
                                {
                                    if (bera == item.Id) { break; } // don't show again
                                    if (first)
                                    {
                                        bera = Properties.Settings.Default.status_last_id;
                                        first = false;
                                        Properties.Settings.Default.status_last_id = item.Id;
                                        Properties.Settings.Default.Save();
                                    }

                                    var embed = new EmbedBuilder()
                                        .WithTitle(item.Title)
                                        .WithUrl(item.Url)
                                        .WithTimestamp(item.Time)
                                        .WithImageUrl(item.Image)
                                        .WithDescription(item.Description)
                                        .WithColor(Color.Blue)
                                        .WithFooter("From: Lodestone")
                                        .Build();

                                    if (!_ffNewsUpdaterPrepareIDS)
                                    {
                                    }
                                        await news_channel.SendMessageAsync("Final Fantasy XIV - Status " + Emote.Bot.LStatus, embed: embed);
                                }

                            }




                            //update
                            apiUrl = "https://lodestonenews.com/news/updates?locale=eu";
                            client = new HttpClient();
                            jsonCommon = "";
                            empty = true;
                            newsListD = new List<LodestoneNews>();


                            jsonCommon = await client.GetStringAsync(apiUrl);
                            if (string.IsNullOrEmpty(jsonCommon)) { Print("NEWS ARE EMPTY OR NULL"); }
                            newsListD = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonCommon);
                            empty = (newsListD == null || newsListD.Count == 0);
                            havenews = !(Properties.Settings.Default.update_last_id == newsListD.First().Id);
                            //Print($"Updates updates? = {!empty && havenews}  ");

                            if (!empty && havenews && Properties.Settings.Default.update_last_id.Length < 5)// first time :D lets don't spam channels
                            {
                                if (newsListD.Count >= 2)
                                {
                                    Properties.Settings.Default.update_last_id = newsListD[1].Id;
                                    Properties.Settings.Default.Save();
                                }
                            }

                            cmsg += $"Update:{!empty && havenews}";

                            if (!empty && havenews)
                            {

                                SocketTextChannel news_channel = Kuru.GetTextChannel(Properties.Settings.Default.update_channel);
                                bool first = true;
                                string hold_new = "";

                                foreach (var item in newsListD)
                                {
                                    if (hold_new == item.Id) { break; }

                                    if (first)
                                    {
                                        first = false;
                                        hold_new = Properties.Settings.Default.update_last_id;
                                        Properties.Settings.Default.update_last_id = item.Id;
                                        Properties.Settings.Default.Save();
                                    }

                                    var embed = new EmbedBuilder()
                                        .WithTitle(item.Title)
                                        .WithUrl(item.Url)
                                        .WithTimestamp(item.Time)
                                        .WithImageUrl(item.Image)
                                        .WithDescription(item.Description)
                                        .WithColor(Color.Blue)
                                        .WithFooter("From: Lodestone")
                                        .Build();

                                    if (!_ffNewsUpdaterPrepareIDS)
                                    {
                                    }
                                        await news_channel.SendMessageAsync("Final Fantasy XIV - Updates " + Emote.Bot.LUpdate, embed: embed);
                                }

                            }


                            //maintenance
                            //Game
                            apiUrl = "https://lodestonenews.com/news/maintenance/current?locale=eu";
                            client = new HttpClient();
                            jsonCommon = "";
                            MaintenanceRoot data = null;
                            empty = true;
                            newsListD = new List<LodestoneNews>();

                            jsonCommon = await client.GetStringAsync(apiUrl);
                            if (string.IsNullOrEmpty(jsonCommon)) { Print("NEWS ARE EMPTY OR NULL"); }
                            data = JsonConvert.DeserializeObject<MaintenanceRoot>(jsonCommon);
                            empty = (data == null || data.Game.Count == 0);
                            //Print($"Maintenance game updates? = {!empty && Properties.Settings.Default.maintenance_last_game_id != data.Game.First().Id}  ");

                            if (!empty && havenews && Properties.Settings.Default.maintenance_last_game_id.Length < 5)// first time :D lets don't spam channels
                            {
                                if (newsListD.Count >= 2)
                                {
                                    Properties.Settings.Default.maintenance_last_game_id = newsListD[1].Id;
                                    Properties.Settings.Default.Save();
                                }
                            }

                            mmsg += $"M/Game:{!empty && Properties.Settings.Default.maintenance_last_game_id != data.Game.First().Id} | ";

                            if (empty)
                            {
                                Properties.Settings.Default.maintenance_last_game_id = "0";
                                Properties.Settings.Default.Save();
                            }

                            if ((!empty) && Properties.Settings.Default.maintenance_last_game_id != data.Game.First().Id)
                            {

                                SocketTextChannel news_channel = Kuru.GetTextChannel(Properties.Settings.Default.maintenance_channel);
                                bool first = true;
                                string hold_new = "";

                                foreach (var news in data.Game) //should be one
                                {
                                    if (first)
                                    {
                                        first = false;
                                        hold_new = Properties.Settings.Default.maintenance_last_game_id;
                                        Properties.Settings.Default.maintenance_last_game_id = news.Id;
                                        Properties.Settings.Default.Save();
                                    }

                                    string st = UnixTime(DateTime.Parse(news.Start));
                                    string et = UnixTime(DateTime.Parse(news.End));
                                    string tt = UnixTime(DateTime.Parse(news.Time));

                                    var embed = new EmbedBuilder()
                                        .WithTitle(news.Title)
                                        .WithUrl(news.Url)
                                        .AddField($"Start time: {Environment.NewLine + UnixTime(DateTime.Parse(news.Start), "d") + Environment.NewLine + UnixTime(DateTime.Parse(news.Start), "t")}", st, true)
                                        .AddField($"End time: {Environment.NewLine + UnixTime(DateTime.Parse(news.End), "d") + Environment.NewLine + UnixTime(DateTime.Parse(news.End), "t")}", et, true)
                                        //.WithTimestamp(DateTime.Parse(news.Time))
                                        //.WithDescription(news.Description)
                                        .WithColor(Color.Blue)
                                        .WithFooter($"From: Lodestone")
                                        .Build();

                                    if (!_ffNewsUpdaterPrepareIDS)
                                    {
                                    }
                                        await news_channel.SendMessageAsync("Final Fantasy XIV - Game maintenance " + Emote.Bot.LMaintenance, embed: embed);
                                }
                            }

                            //Mog station
                            empty = (data == null || data.Mog.Count == 0);
                            //Print($"Maintenance mog updates? = {!empty && Properties.Settings.Default.maintenance_last_mog_id != data.Mog.First().Id}  ");
                            mmsg += $"M/Mog:{!empty && Properties.Settings.Default.maintenance_last_mog_id != data.Mog.First().Id} | ";

                            if (empty)
                            {
                                Properties.Settings.Default.maintenance_last_mog_id = "0";
                                Properties.Settings.Default.Save();
                            }
                            //Properties.Settings.Default.maintenance_last_mog_id = "";

                            if ((!empty) && Properties.Settings.Default.maintenance_last_mog_id != data.Mog.First().Id)
                            {
                                SocketTextChannel news_channel = Kuru.GetTextChannel(Properties.Settings.Default.maintenance_channel);
                                bool first = true;
                                string hold_new = "";

                                foreach (var news in data.Mog) //should be one
                                {
                                    if (first)
                                    {
                                        first = false;
                                        hold_new = Properties.Settings.Default.maintenance_last_mog_id;
                                        Properties.Settings.Default.maintenance_last_mog_id = news.Id;
                                        Properties.Settings.Default.Save();
                                    }

                                    string st = UnixTime(DateTime.Parse(news.Start));
                                    string et = UnixTime(DateTime.Parse(news.End));
                                    string tt = UnixTime(DateTime.Parse(news.Time));

                                    var embed = new EmbedBuilder()
                                        .WithTitle(news.Title)
                                        .WithUrl(news.Url)
                                        .AddField($"Start time: {Environment.NewLine + UnixTime(DateTime.Parse(news.Start), "d") + Environment.NewLine + UnixTime(DateTime.Parse(news.Start), "t")}", st, true)
                                        .AddField($"End time: {Environment.NewLine + UnixTime(DateTime.Parse(news.End), "d") + Environment.NewLine + UnixTime(DateTime.Parse(news.End), "t")}", et, true)
                                        //.WithTimestamp(DateTime.Parse(news.Time))
                                        //.WithDescription(news.Description)
                                        .WithColor(Color.Blue)
                                        .WithFooter($"From: Lodestone")
                                        .Build();

                                    if (!_ffNewsUpdaterPrepareIDS)
                                    {
                                    }
                                        await news_channel.SendMessageAsync("Final Fantasy XIV - Mog Station maintenance " + Emote.Bot.LMaintenance, embed: embed);
                                }
                            }



                            //Lodestone
                            empty = (data == null || data.Lodestone.Count == 0);
                            //Print($"Maintenance lodestone updates? = {!empty && Properties.Settings.Default.maintenance_last_lodestone_id != data.Lodestone.First().Id}  ");
                            mmsg += $"M/Lodestone:{!empty && Properties.Settings.Default.maintenance_last_lodestone_id != data.Lodestone.First().Id} | ";

                            if (empty)
                            {
                                Properties.Settings.Default.maintenance_last_lodestone_id = "0";
                                Properties.Settings.Default.Save();
                            }
                            //Properties.Settings.Default.maintenance_last_lodestone_id = "";

                            if ((!empty) && Properties.Settings.Default.maintenance_last_lodestone_id != data.Lodestone.First().Id)
                            {
                                SocketTextChannel news_channel = Kuru.GetTextChannel(Properties.Settings.Default.maintenance_channel);
                                bool first = true;
                                string hold_new = "";

                                foreach (var news in data.Lodestone) //should be one
                                {
                                    if (first)
                                    {
                                        first = false;
                                        hold_new = Properties.Settings.Default.maintenance_last_lodestone_id;
                                        Properties.Settings.Default.maintenance_last_lodestone_id = news.Id;
                                        Properties.Settings.Default.Save();
                                    }

                                    string st = UnixTime(DateTime.Parse(news.Start));
                                    string et = UnixTime(DateTime.Parse(news.End));
                                    string tt = UnixTime(DateTime.Parse(news.Time));

                                    var embed = new EmbedBuilder()
                                        .WithTitle(news.Title)
                                        .WithUrl(news.Url)
                                        .AddField($"Start time: {Environment.NewLine + UnixTime(DateTime.Parse(news.Start), "d") + Environment.NewLine + UnixTime(DateTime.Parse(news.Start), "t")}", st, true)
                                        .AddField($"End time: {Environment.NewLine + UnixTime(DateTime.Parse(news.End), "d") + Environment.NewLine + UnixTime(DateTime.Parse(news.End), "t")}", et, true)
                                        //.WithTimestamp(DateTime.Parse(news.Time))
                                        //.WithDescription(news.Description)
                                        .WithColor(Color.Blue)
                                        .WithFooter($"From: Lodestone")
                                        .Build();

                                    if (!_ffNewsUpdaterPrepareIDS)
                                    {
                                    }
                                        await news_channel.SendMessageAsync("Final Fantasy XIV - Lodestone maintenance " + Emote.Bot.LMaintenance, embed: embed);
                                }

                            }



                            //Companion
                            empty = (data == null || data.Companion.Count == 0);
                            //Print($"Maintenance companion updates? = {!empty && Properties.Settings.Default.maintenance_last_companion_id != data.Companion.First().Id}  ");
                            mmsg += $"M/Companion:{!empty && Properties.Settings.Default.maintenance_last_companion_id != data.Companion.First().Id}";

                            if (empty)
                            {
                                Properties.Settings.Default.maintenance_last_companion_id = "0";
                                Properties.Settings.Default.Save();
                            }
                            //TEST
                            //Properties.Settings.Default.maintenance_last_companion_id = "";

                            if ((!empty) && Properties.Settings.Default.maintenance_last_companion_id != data.Companion.First().Id)
                            {
                                SocketTextChannel news_channel = Kuru.GetTextChannel(Properties.Settings.Default.maintenance_channel);
                                bool first = true;
                                string hold_new = "";

                                foreach (var news in data.Companion) //should be one
                                {
                                    if (first)
                                    {
                                        first = false;
                                        hold_new = Properties.Settings.Default.maintenance_last_companion_id;
                                        Properties.Settings.Default.maintenance_last_companion_id = news.Id;
                                        Properties.Settings.Default.Save();
                                    }


                                    string st = UnixTime(DateTime.Parse(news.Start));
                                    string et = UnixTime(DateTime.Parse(news.End));
                                    string tt = UnixTime(DateTime.Parse(news.Time));

                                    var embed = new EmbedBuilder()
                                        .WithTitle(news.Title)
                                        .WithUrl(news.Url)
                                        .AddField($"Start time: {Environment.NewLine + UnixTime(DateTime.Parse(news.Start), "d") + Environment.NewLine + UnixTime(DateTime.Parse(news.Start), "t")}", st, true)
                                        .AddField($"End time: {Environment.NewLine + UnixTime(DateTime.Parse(news.End), "d") + Environment.NewLine + UnixTime(DateTime.Parse(news.End), "t")}", et, true)
                                        //.WithTimestamp(DateTime.Parse(news.Time))
                                        //.WithDescription(news.Description)
                                        .WithColor(Color.Blue)
                                        .WithFooter($"From: Lodestone")
                                        .Build();

                                    //TEST
                                    //news_channel = (SocketTextChannel)_kuru.GetChannel(1310199196233760858); //test-bot channel
                                    //await news_channel.SendMessageAsync("Final Fantasy XIV - Companion maintenance", embed: embed);
                                    if (!_ffNewsUpdaterPrepareIDS)
                                    {
                                    }
                                        await news_channel.SendMessageAsync("Final Fantasy XIV - Companion maintenance " + Emote.Bot.LMaintenance, embed: embed);
                                }

                            }

                            if (string.IsNullOrEmpty(Properties.Settings.Default.maintenance_last_game_id))
                            {
                                Properties.Settings.Default.maintenance_last_game_id = "0";
                                Properties.Settings.Default.Save();
                            }

                            if (string.IsNullOrEmpty(Properties.Settings.Default.maintenance_last_mog_id))
                            {
                                Properties.Settings.Default.maintenance_last_mog_id = "0";
                                Properties.Settings.Default.Save();
                            }

                            if (string.IsNullOrEmpty(Properties.Settings.Default.maintenance_last_lodestone_id))
                            {
                                Properties.Settings.Default.maintenance_last_lodestone_id = "0";
                                Properties.Settings.Default.Save();
                            }

                            if (string.IsNullOrEmpty(Properties.Settings.Default.maintenance_last_companion_id))
                            {
                                Properties.Settings.Default.maintenance_last_companion_id = "0";
                                Properties.Settings.Default.Save();
                            }

                            if (!_silentTrys) //console log
                            {

                                bool c1 = false, c2 = false;
                                if (cmsg.Contains(":true")) { Print(cmsg); c1 = true; }
                                if (mmsg.Contains(":true")) { Print(mmsg); c2 = true; }
                                if (c1 || c2)
                                {
                                    Print($"Cheching again in {each} minutes...");
                                }
                                else
                                {
                                    Print($"No updates, cheching again in {each} minutes...");
                                }
                            }
                            else
                            {
                                Print($"FFNewsUpdater running.");
                            }

                        }
                        catch (Exception ex)
                        {


                            if (!_keeptrying)
                            {
                                string exType = ex.GetType().ToString();
                                int i = exType.LastIndexOf('.');
                                exType = exType.Substring(i + 1);
                                each = (trys < maxTrys) ? 5 : _ffNewsUpdaterTimer;
                                time_multipler = (trys < maxTrys) ? 1000 : 60000;
                                trys++;

                                Print(exType + " -> " + ex.InnerException.Message);

                                if (trys > maxTrys)
                                {
                                    Print($"Stoping -> Check_FF_updates: Max trys reached, can't connect with server.");
                                    run = false; //rip
                                }
                                else
                                {
                                    Print($"Trying again in {(time_multipler * each) / 1000} seconds...");
                                    if (!(trys > maxTrys)) { Print($"Next try is: {trys}/{maxTrys}"); }
                                }
                            }
                            else
                            {
                                Print("End of loop, restarting...");
                            }



                        }
                    }

                    //should be stored
                    _ffNewsUpdaterPrepareIDS = false;
                    //check again in...
                    await Task.Delay(time_multipler * each);


                }
            });
        }


    }

}

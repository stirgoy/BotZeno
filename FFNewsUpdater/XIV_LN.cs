using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        static Timer XIV_Timer;

        static private void XIV_LN()
        {
            if (XIV_LN_enabled)
            {
                XIV_Timer = new Timer(XIV_LN_Loop, null, 10, XIV.Config.TimerInterval); //timer
            }
        }

        static async void XIV_LN_Loop(object state)
        {
            try
            {
                XIV_Timer.Change(Timeout.Infinite, Timeout.Infinite); //stop timer

                HttpClient H_Client = new HttpClient();

                //lets call api each 2 sec
                Print("XIV_LN_Loop -> Start");
                Print("API Call.", false, true);
                string jsonTopics = await H_Client.GetStringAsync(XIV.APIs.Topics);
                await Task.Delay(XIV.Config.APIDelay);
                Print(".", false, false);
                string jsonStatus = await H_Client.GetStringAsync(XIV.APIs.Status);
                await Task.Delay(XIV.Config.APIDelay);
                Print(".", false, false);
                string jsonUpdates = await H_Client.GetStringAsync(XIV.APIs.Updates);
                await Task.Delay(XIV.Config.APIDelay);
                Print(".", false, false);
                string json_Notices = await H_Client.GetStringAsync(XIV.APIs.Notices);
                await Task.Delay(XIV.Config.APIDelay);
                Print(".", false, false);
                string jsonMaintenance = await H_Client.GetStringAsync(XIV.APIs.Maintenance);
                await Task.Delay(XIV.Config.APIDelay);
                Print(".", false, false);
                Print(" ", true, false);
                string jsonMaintenance_Current = await H_Client.GetStringAsync(XIV.APIs.MaintenanceCurrent);

                Print("Working...");

                bool have_Topics = !(string.IsNullOrEmpty(jsonTopics));
                bool have_Status = !(string.IsNullOrEmpty(jsonStatus));
                bool have_Updates = !(string.IsNullOrEmpty(jsonUpdates));
                bool have_Notices = !(string.IsNullOrEmpty(json_Notices));
                bool have_Maintenance = !(string.IsNullOrEmpty(jsonMaintenance));
                bool have_Maintenance_Current = !(string.IsNullOrEmpty(jsonMaintenance_Current));

                //  --Topics--
                if (have_Topics)
                {
                    List<LodestoneNews> List_Topics = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonTopics);

                    if (Properties.Settings.Default.news_last_id == "0")
                    {
                        Properties.Settings.Default.news_last_id = List_Topics[XIV.Config.MaxNewsOnFirst].Id;
                        Properties.Settings.Default.Save();
                    }

                    if (Properties.Settings.Default.news_last_id != List_Topics.First().Id)
                    {

                        int howMany = 0;

                        foreach (LodestoneNews item in List_Topics)
                        {
                            if (item.Id == Properties.Settings.Default.news_last_id) { break; } else { howMany++; }
                        }

                        SocketTextChannel _channel = Kuru.GetTextChannel(Properties.Settings.Default.news_channel);

                        for (int i = 0; i < howMany; i++)
                        {

                            string end_desc = Environment.NewLine + Environment.NewLine + "-# ";
                            end_desc += UnixTime(List_Topics[i].Time);
                            string start_desc = $"### [{List_Topics[i].Title}]({List_Topics[i].Url})" + Environment.NewLine + Environment.NewLine;

                            Embed embed = new EmbedBuilder()
                                            .WithTitle($"{Emote.Bot.LTopics} Tpoics")
                                            .WithImageUrl(List_Topics[i].Image)
                                            .WithDescription(start_desc + List_Topics[i].Description + end_desc)
                                            .WithThumbnailUrl(XIV.Config.FFLogo)
                                            .WithColor(Color.Blue)
                                            .WithFooter("From: Lodestone News")
                                            .Build();
                            if (_channel != null)
                            {
                                await _channel.SendMessageAsync("", embed: embed);
                            }
                        }

                        Properties.Settings.Default.news_last_id = List_Topics[0].Id;
                        Properties.Settings.Default.Save();

                    }

                }

                //  --Status--
                if (have_Status)
                {
                    List<LodestoneNews> List_Status = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonStatus);

                    if (Properties.Settings.Default.status_last_id == "0")
                    {
                        Properties.Settings.Default.status_last_id = List_Status[XIV.Config.MaxNewsOnFirst].Id;
                        Properties.Settings.Default.Save();
                    }
                    if (Properties.Settings.Default.status_last_id != List_Status.First().Id)
                    {

                        int howMany = 0;

                        foreach (LodestoneNews item in List_Status)
                        {
                            if (item.Id == Properties.Settings.Default.status_last_id) { break; } else { howMany++; }
                        }

                        SocketTextChannel _channel = Kuru.GetTextChannel(Properties.Settings.Default.status_channel);

                        for (int i = 0; i < howMany; i++)
                        {
                            string end_desc = Environment.NewLine + Environment.NewLine + "-# ";
                            string start_desc = $"### [{List_Status[i].Title}]({List_Status[i].Url})" + Environment.NewLine + Environment.NewLine;
                            end_desc += UnixTime(List_Status[i].Time);

                            Embed embed = new EmbedBuilder()
                                            .WithTitle($"{Emote.Bot.LStatus} Status")
                                            .WithDescription(start_desc + List_Status[i].Description + end_desc)
                                            .WithThumbnailUrl(XIV.Config.FFLogo)
                                            .WithColor(Color.Blue)
                                            .WithFooter("From: Lodestone News")
                            .Build();

                            if (_channel != null)
                            {
                                await _channel.SendMessageAsync("", embed: embed);
                            }
                        }

                        Properties.Settings.Default.status_last_id = List_Status[0].Id;
                        Properties.Settings.Default.Save();
                    }
                }

                //  --Updates--
                if (have_Updates)
                {
                    List<LodestoneNews> List_Updates = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonUpdates);

                    if (Properties.Settings.Default.update_last_id == "0")
                    {
                        Properties.Settings.Default.update_last_id = List_Updates[XIV.Config.MaxNewsOnFirst].Id;
                        Properties.Settings.Default.Save();
                    }

                    if (Properties.Settings.Default.update_last_id != List_Updates.First().Id)
                    {

                        int howMany = 0;

                        foreach (LodestoneNews item in List_Updates)
                        {
                            if (item.Id == Properties.Settings.Default.update_last_id) { break; } else { howMany++; }
                        }

                        SocketTextChannel _channel = Kuru.GetTextChannel(Properties.Settings.Default.update_channel);

                        for (int i = 0; i < howMany; i++)
                        {
                            string end_desc = Environment.NewLine + Environment.NewLine + "-# ";
                            string start_desc = $"### [{List_Updates[i].Title}]({List_Updates[i].Url})" + Environment.NewLine + Environment.NewLine;
                            end_desc += UnixTime(List_Updates[i].Time);

                            Embed embed = new EmbedBuilder()
                                            .WithTitle($"{Emote.Bot.LUpdate} Updates")
                                            .WithDescription(start_desc + List_Updates[i].Description + end_desc)
                                            .WithThumbnailUrl(XIV.Config.FFLogo)
                                            .WithColor(Color.Blue)
                                            .WithFooter("From: Lodestone News")
                            .Build();

                            if (_channel != null)
                            {
                                await _channel.SendMessageAsync("", embed: embed);
                            }
                        }

                        Properties.Settings.Default.update_last_id = List_Updates[0].Id;
                        Properties.Settings.Default.Save();
                    }

                }

                //  --Notices--
                if (have_Notices)
                {
                    List<LodestoneNews> List_Notices = JsonConvert.DeserializeObject<List<LodestoneNews>>(json_Notices);

                    if (Properties.Settings.Default.notices_last_id == "0")
                    {
                        Properties.Settings.Default.notices_last_id = List_Notices[XIV.Config.MaxNewsOnFirst].Id;
                        Properties.Settings.Default.Save();
                    }

                    if (Properties.Settings.Default.notices_last_id != List_Notices.First().Id)
                    {

                        int howMany = 0;

                        foreach (LodestoneNews item in List_Notices)
                        {
                            if (item.Id == Properties.Settings.Default.notices_last_id) { break; } else { howMany++; }
                        }

                        SocketTextChannel _channel = Kuru.GetTextChannel(Properties.Settings.Default.notices_channel);


                        for (int i = 0; i < howMany; i++)
                        {
                            string end_desc = Environment.NewLine + Environment.NewLine + "-# ";
                            string start_desc = $"### [{List_Notices[i].Title}]({List_Notices[i].Url})" + Environment.NewLine + Environment.NewLine;
                            end_desc += UnixTime(List_Notices[i].Time);

                            Embed embed = new EmbedBuilder()
                                            .WithTitle($"{Emote.Bot.Lnotices} Notices")
                                            .WithDescription(start_desc + List_Notices[i].Description + end_desc)
                                            .WithThumbnailUrl(XIV.Config.FFLogo)
                                            .WithColor(Color.Blue)
                                            .WithFooter("From: Lodestone News")
                            .Build();

                            if (_channel != null)
                            {
                                await _channel.SendMessageAsync("", embed: embed);
                            }
                        }

                        Properties.Settings.Default.notices_last_id = List_Notices[0].Id;
                        Properties.Settings.Default.Save();
                    }
                }

                //  --Maintenance--
                if (have_Maintenance)
                {
                    List<LodestoneNews> List_Maintenance = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonMaintenance);

                    if (Properties.Settings.Default.maintenance_last_id == "0")
                    {
                        Properties.Settings.Default.maintenance_last_id = List_Maintenance[XIV.Config.MaxNewsOnFirst].Id;
                        Properties.Settings.Default.Save();
                    }

                    if (Properties.Settings.Default.maintenance_last_id != List_Maintenance.First().Id)
                    {

                        int howMany = 0;

                        foreach (LodestoneNews item in List_Maintenance)
                        {
                            if (item.Id == Properties.Settings.Default.maintenance_last_id) { break; } else { howMany++; }
                        }

                        SocketTextChannel _channel = Kuru.GetTextChannel(Properties.Settings.Default.maintenance_channel);

                        for (int i = 0; i < howMany; i++)
                        {
                            string end_desc = Environment.NewLine + Environment.NewLine + "-# ";
                            string start_desc = $"### [{List_Maintenance[i].Title}]({List_Maintenance[i].Url})" + Environment.NewLine + Environment.NewLine;
                            end_desc += UnixTime(List_Maintenance[i].Time);

                            Embed embed = new EmbedBuilder()
                                            .WithTitle($"{Emote.Bot.LMaintenance} Maintenance")
                                            .WithDescription(start_desc + List_Maintenance[i].Description + end_desc)
                                            .WithThumbnailUrl(XIV.Config.FFLogo)
                                            .WithColor(Color.Blue)
                                            .WithFooter("From: Lodestone News")
                            .Build();

                            if (_channel != null)
                            {
                                await _channel.SendMessageAsync("", embed: embed);
                            }
                        }

                        Properties.Settings.Default.maintenance_last_id = List_Maintenance[0].Id;
                        Properties.Settings.Default.Save();
                    }
                }

                //-------------------------------------- Maintenance Current
                //  --Game--
                if (have_Maintenance_Current)
                {
                    MaintenanceRoot List_Maintenance_Current = JsonConvert.DeserializeObject<MaintenanceRoot>(jsonMaintenance_Current);
                    //Print($"RootMant {List_Maintenance_Current.Game.Count.ToString()}");
                    SocketTextChannel _channel = Kuru.GetTextChannel(Properties.Settings.Default.maintenance_channel);

                    if (List_Maintenance_Current.Game.Count > 0)
                    {


                        if (Properties.Settings.Default.maintenance_last_game_id != List_Maintenance_Current.Game.First().Id)
                        {

                            int howMany = 0;

                            foreach (MaintenanceEvent item in List_Maintenance_Current.Game)
                            {
                                if (item.Id == Properties.Settings.Default.maintenance_last_game_id) { break; } else { howMany++; }
                            }

                            for (int i = 0; i < howMany; i++)
                            {
                                string st = UnixTime(DateTime.Parse(List_Maintenance_Current.Game[i].Start));
                                string et = UnixTime(DateTime.Parse(List_Maintenance_Current.Game[i].End));
                                string tt = UnixTime(DateTime.Parse(List_Maintenance_Current.Game[i].Time));

                                var embed = new EmbedBuilder()
                                    .WithTitle($"{Emote.Bot.LMaintenance} Game maintenance.")
                                    .WithUrl(List_Maintenance_Current.Game[i].Url)
                                    .WithDescription("### " + List_Maintenance_Current.Game[i].Title + Environment.NewLine + Environment.NewLine + $"-# {tt}")
                                    .AddField($"Start time: {Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Game[i].Start), "d") + Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Game[i].Start), "t")}", st, true)
                                    .AddField($"End time: {Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Game[i].End), "d") + Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Game[i].End), "t")}", et, true)
                                    .WithColor(Color.Blue)
                                    .WithFooter($"From: Lodestone News")
                                    .Build();

                                if (_channel != null)
                                {
                                    await _channel.SendMessageAsync("", embed: embed);
                                }
                            }

                            Properties.Settings.Default.maintenance_last_game_id = List_Maintenance_Current.Game[0].Id;
                            Properties.Settings.Default.Save();
                        }
                    }
                    else
                    {
                        Properties.Settings.Default.maintenance_last_game_id = "0";
                        Properties.Settings.Default.Save();
                    }

                    //  --Lodestone--
                    if (List_Maintenance_Current.Lodestone.Count > 0)
                    {
                        if (Properties.Settings.Default.maintenance_last_lodestone_id != List_Maintenance_Current.Lodestone.First().Id)
                        {

                            int howMany = 0;

                            foreach (MaintenanceEvent item in List_Maintenance_Current.Lodestone)
                            {
                                if (item.Id == Properties.Settings.Default.maintenance_last_lodestone_id) { break; } else { howMany++; }
                            }

                            for (int i = 0; i < howMany; i++)
                            {
                                string st = UnixTime(DateTime.Parse(List_Maintenance_Current.Lodestone[i].Start));
                                string et = UnixTime(DateTime.Parse(List_Maintenance_Current.Lodestone[i].End));
                                string tt = UnixTime(DateTime.Parse(List_Maintenance_Current.Lodestone[i].Time));

                                var embed = new EmbedBuilder()
                                    .WithTitle($"{Emote.Bot.LMaintenance} Lodestone maintenance.")
                                    .WithUrl(List_Maintenance_Current.Lodestone[i].Url)
                                    .WithDescription("### " + List_Maintenance_Current.Lodestone[i].Title + Environment.NewLine + Environment.NewLine + $"-# {tt}")
                                    .AddField($"Start time: {Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Lodestone[i].Start), "d") + Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Lodestone[i].Start), "t")}", st, true)
                                    .AddField($"End time: {Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Lodestone[i].End), "d") + Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Lodestone[i].End), "t")}", et, true)
                                    .WithColor(Color.Blue)
                                    .WithFooter($"From: Lodestone News")
                                    .Build();

                                if (_channel != null)
                                {
                                    await _channel.SendMessageAsync("", embed: embed);
                                }
                            }

                            Properties.Settings.Default.maintenance_last_lodestone_id = List_Maintenance_Current.Lodestone[0].Id;
                            Properties.Settings.Default.Save();
                        }
                    }
                    else
                    {
                        Properties.Settings.Default.maintenance_last_lodestone_id = "0";
                        Properties.Settings.Default.Save();
                    }


                    //  --Mog--
                    if (List_Maintenance_Current.Mog.Count > 0)
                    {
                        if (Properties.Settings.Default.maintenance_last_mog_id != List_Maintenance_Current.Mog.First().Id)
                        {

                            int howMany = 0;

                            foreach (MaintenanceEvent item in List_Maintenance_Current.Mog)
                            {
                                if (item.Id == Properties.Settings.Default.maintenance_last_mog_id) { break; } else { howMany++; }
                            }

                            for (int i = 0; i < howMany; i++)
                            {
                                string st = UnixTime(DateTime.Parse(List_Maintenance_Current.Mog[i].Start));
                                string et = UnixTime(DateTime.Parse(List_Maintenance_Current.Mog[i].End));
                                string tt = UnixTime(DateTime.Parse(List_Maintenance_Current.Mog[i].Time));

                                var embed = new EmbedBuilder()
                                    .WithTitle($"{Emote.Bot.LMaintenance} Mog maintenance.")
                                    .WithUrl(List_Maintenance_Current.Mog[i].Url)
                                    .WithDescription("### " + List_Maintenance_Current.Mog[i].Title + Environment.NewLine + Environment.NewLine + $"-# {tt}")
                                    .AddField($"Start time: {Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Mog[i].Start), "d") + Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Mog[i].Start), "t")}", st, true)
                                    .AddField($"End time: {Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Mog[i].End), "d") + Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Mog[i].End), "t")}", et, true)
                                    .WithColor(Color.Blue)
                                    .WithFooter($"From: Lodestone News")
                                    .Build();

                                if (_channel != null)
                                {
                                    await _channel.SendMessageAsync("", embed: embed);
                                }
                            }

                            Properties.Settings.Default.maintenance_last_mog_id = List_Maintenance_Current.Mog[0].Id;
                            Properties.Settings.Default.Save();
                        }
                    }
                    else
                    {
                        Properties.Settings.Default.maintenance_last_mog_id = "0";
                        Properties.Settings.Default.Save();

                    }


                    //  --Companion--
                    if (List_Maintenance_Current.Companion.Count > 0)
                    {
                        if (Properties.Settings.Default.maintenance_last_companion_id != List_Maintenance_Current.Companion.First().Id)
                        {

                            int howMany = 0;

                            foreach (MaintenanceEvent item in List_Maintenance_Current.Companion)
                            {
                                if (item.Id == Properties.Settings.Default.maintenance_last_companion_id) { break; } else { howMany++; }
                            }

                            for (int i = 0; i < howMany; i++)
                            {
                                string st = UnixTime(DateTime.Parse(List_Maintenance_Current.Companion[i].Start));
                                string et = UnixTime(DateTime.Parse(List_Maintenance_Current.Companion[i].End));
                                string tt = UnixTime(DateTime.Parse(List_Maintenance_Current.Companion[i].Time));

                                var embed = new EmbedBuilder()
                                    .WithTitle($"{Emote.Bot.LMaintenance} Companion maintenance.")
                                    .WithUrl(List_Maintenance_Current.Companion[i].Url)
                                    .WithDescription("### " + List_Maintenance_Current.Companion[i].Title + Environment.NewLine + Environment.NewLine + $"-# {tt}")
                                    .AddField($"Start time: {Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Companion[i].Start), "d") + Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Companion[i].Start), "t")}", st, true)
                                    .AddField($"End time: {Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Companion[i].End), "d") + Environment.NewLine + UnixTime(DateTime.Parse(List_Maintenance_Current.Companion[i].End), "t")}", et, true)
                                    .WithColor(Color.Blue)
                                    .WithFooter($"From: Lodestone News")
                                    .Build();

                                if (_channel != null)
                                {
                                    await _channel.SendMessageAsync("", embed: embed);
                                }
                            }

                            Properties.Settings.Default.maintenance_last_companion_id = List_Maintenance_Current.Companion[0].Id;
                            Properties.Settings.Default.Save();
                        }
                    }
                    else
                    {

                        Properties.Settings.Default.maintenance_last_companion_id = "0";
                        Properties.Settings.Default.Save();

                    }

                }

                Print("XIV_LN_Loop -> Stop");
                XIV_Timer.Change(XIV.Config.TimerInterval, Timeout.Infinite); //stop timer

            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

    }

}
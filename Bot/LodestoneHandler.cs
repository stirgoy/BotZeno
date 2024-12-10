using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        /********************
          LodestoneHandler
        *////////////////////
        private async Task<List<Embed>> LodestoneHandler(int cantidad, string kindOf)
        {
            //SOURCE https://documenter.getpostman.com/view/1779678/TzXzDHVk#5bd3a0a5-43b1-408d-bb7a-1788f22662a8
            //  topics
            string apiUrl;
            string title;

            switch (kindOf)
            {
                case "news":
                    apiUrl = "https://lodestonenews.com/news/topics?locale=eu";
                    title = $"{Emote.Bot.LTopics} Tpoics";
                    break;

                case "maintenance_c":
                    apiUrl = "https://lodestonenews.com/news/maintenance/current?locale=eu";
                    title = $"{Emote.Bot.LMaintenance} Maintenance";
                    break;

                case "maintenance":
                    apiUrl = "https://lodestonenews.com/news/maintenance?locale=eu";
                    title = $"{Emote.Bot.LMaintenance} Maintenance";
                    break;

                case "updates":
                    apiUrl = "https://lodestonenews.com/news/updates?locale=eu";
                    title = $"{Emote.Bot.LUpdate} Updates";
                    break;

                case "status":
                    apiUrl = "https://lodestonenews.com/news/status?locale=eu";
                    title = $"{Emote.Bot.LStatus} Status";
                    break;

                case "notices":
                    apiUrl = "https://lodestonenews.com/news/notices?locale=eu";
                    title = $"{Emote.Bot.Lnotices} Notices";
                    break;

                default:
                    apiUrl = "https://lodestonenews.com/news/topics?locale=eu";
                    title = $"{Emote.Bot.LTopics} Tpoics";
                    break;
            }

            List<Embed> ret = new List<Embed>();

            HttpClient client = new HttpClient();
            string jsonMaintenance, jsonCommon;
            MaintenanceRoot data = null;
            List<LodestoneNews> newsListD = new List<LodestoneNews>();
            bool empty = true;

            try
            {


                //LodestoneMaintenance
                //List<LodestoneMaintenance> newsListM = new List<LodestoneMaintenance>();

                if (kindOf == "maintenance_c")
                {
                    //newsListM = JsonConvert.DeserializeObject<List<LodestoneMaintenance>>(jsonResponse);
                    jsonMaintenance = await client.GetStringAsync(apiUrl);
                    if (string.IsNullOrEmpty(jsonMaintenance)) { Print("NEWS ARE EMPTY OR NULL"); }
                    data = JsonConvert.DeserializeObject<MaintenanceRoot>(jsonMaintenance);
                    empty = (data == null || data.Game.Count == 0);
                    cantidad = 1; //plz xD

                }
                else
                {
                    jsonCommon = await client.GetStringAsync(apiUrl);
                    if (string.IsNullOrEmpty(jsonCommon)) { Print("NEWS ARE EMPTY OR NULL"); }
                    newsListD = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonCommon);
                    empty = (newsListD == null || newsListD.Count == 0);
                }

            }
            catch (Exception ex)
            {
                Print("Begu.LodestoneHandler() Error");
                Print(ex.Message);
                Print(ex.Source);
                Print(ex.InnerException.ToString());
            }


            if (empty)
            {
                string tit = (kindOf == "maintenance_c") ? "I got nothing... " + Emote.Bot.Disconnecting_party : " Error " + Emote.Bot.Disconnecting_party;
                string str = (kindOf == "maintenance_c") ? "Is game on maintenance??? " + Emote.Bot.Disconnecting_party : "No data recieved. " + Emote.Bot.Disconnecting_party;

                Print("NEW LIST IS NULL");
                //await command.FollowupAsync("Something went wrong...");
                var embed = new EmbedBuilder()
                    .WithTitle(tit)
                    .WithDescription(str)
                    //.WithTimestamp(DateTimeOffset.Now)
                    .WithColor(Color.Red)
                    .Build();
                return new List<Embed> { embed };
            }


            if (kindOf == "maintenance_c")
            {
                foreach (var news in data.Game)
                {
                    string st = UnixTime(DateTime.Parse(news.Start));
                    string et = UnixTime(DateTime.Parse(news.End));
                    string tt = UnixTime(DateTime.Parse(news.Time));

                    var embed = new EmbedBuilder()
                        .WithTitle(title)
                        .WithUrl(news.Url)
                        .AddField($"Start time: {Environment.NewLine + UnixTime(DateTime.Parse(news.Start), "d") + Environment.NewLine + UnixTime(DateTime.Parse(news.Start), "t")}", st, true)
                        .AddField($"End time: {Environment.NewLine + UnixTime(DateTime.Parse(news.End), "d") + Environment.NewLine + UnixTime(DateTime.Parse(news.End), "t")}", et, true)
                        .WithDescription("### " + news.Title + Environment.NewLine + Environment.NewLine + $"-# {tt}")
                        .WithThumbnailUrl(XIV.Config.FFLogo)
                        .WithColor(Color.Blue)
                        .WithFooter("From: Lodestone News")
                        .Build();
                    //await command.FollowupAsync(embed: embed);

                    ret.Add(embed);
                }
            }
            else
            {
                foreach (var news in newsListD.Take(cantidad))
                {
                    string start_desc = $"### [{news.Title}]({news.Url})" + Environment.NewLine + Environment.NewLine;
                    string end_desc = Environment.NewLine + Environment.NewLine + "-# " + UnixTime(news.Time);
                    var embed = new EmbedBuilder()
                        .WithTitle(title)
                        .WithUrl(news.Url)
                        //.WithTimestamp(news.Time)
                        .WithImageUrl(news.Image)
                        .WithDescription(start_desc + news.Description + end_desc)
                        .WithColor(Color.Blue)
                        .WithThumbnailUrl(XIV.Config.FFLogo)
                        .WithFooter("From: Lodestone News")
                        .Build();
                    //await command.FollowupAsync(embed: embed);

                    ret.Add(embed);
                }

            }

            //Print(ret.Count.ToString());
            return ret;

        }
    }
}

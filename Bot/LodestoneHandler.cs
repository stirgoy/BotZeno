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

            switch (kindOf)
            {
                case "news":
                    apiUrl = "https://lodestonenews.com/news/topics?locale=eu";
                    break;

                case "maintenance":
                    apiUrl = "https://lodestonenews.com/news/maintenance/current?locale=eu";
                    break;

                case "updates":
                    apiUrl = "https://lodestonenews.com/news/updates?locale=eu";
                    break;

                case "status":
                    apiUrl = "https://lodestonenews.com/news/status?locale=eu";
                    break;

                default:
                    apiUrl = "https://lodestonenews.com/news/topics?locale=eu";
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

                if (kindOf == "maintenance")
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
                string tit = (kindOf == "maintenance") ? "I got nothing... " + Emote.Bot.Disconnecting_party : " Error " + Emote.Bot.Disconnecting_party;
                string str = (kindOf == "maintenance") ? "Is game on maintenance??? " + Emote.Bot.Disconnecting_party : "No data recieved. " + Emote.Bot.Disconnecting_party;

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


            if (kindOf == "maintenance")
            {
                foreach (var news in data.Game)
                {
                    var embed = new EmbedBuilder()
                        .WithTitle(news.Title)
                        .WithUrl(news.Url)
                        .WithTimestamp(DateTime.Parse(news.Time))
                        .WithTimestamp(DateTime.Parse(news.Start))
                        .WithTimestamp(DateTime.Parse(news.End))
                        //.WithDescription(news.Description)
                        .WithColor(Color.Blue)
                        .WithFooter("From: Lodestone")
                        .Build();
                    //await command.FollowupAsync(embed: embed);

                    ret.Add(embed);
                }
            }
            else
            {
                foreach (var news in newsListD.Take(cantidad))
                {
                    var embed = new EmbedBuilder()
                        .WithTitle(news.Title)
                        .WithUrl(news.Url)
                        .WithTimestamp(news.Time)
                        .WithImageUrl(news.Image)
                        .WithDescription(news.Description)
                        .WithColor(Color.Blue)
                        .WithFooter("From: Lodestone")
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

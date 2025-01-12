using Discord;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /********************
            FFMaintTalk
        *//////////////////// 
        async Task<Embed> FFMaintenanceTalk()
        {
            HttpClient client = new HttpClient();
            Embed embed = null;
            MaintenanceRoot data = null;
            string title = $"{Emote.Bot.LMaintenance} Current Maintenance";

            string jsonMaintenance = await client.GetStringAsync(XIVLN.APIs.MaintenanceCurrent);
            bool empty = true;

            if (!string.IsNullOrEmpty(jsonMaintenance))
            {
                data = JsonConvert.DeserializeObject<MaintenanceRoot>(jsonMaintenance);
                empty = (data == null || data.Game.Count == 0);
            }

            if (!empty)
            {
                foreach (var item in data.Game)
                {
                    string st = UnixTime(DateTime.Parse(item.Start));
                    string et = UnixTime(DateTime.Parse(item.End));
                    string tt = UnixTime(DateTime.Parse(item.Time));

                    embed = CreateEmbedField_2(
                        title,
                        "### " + item.Title + NL + NL + $"-# {tt}",
                        $"Start time: {NL + UnixTime(DateTime.Parse(item.Start), "d") + NL + UnixTime(DateTime.Parse(item.Start), "t")}",
                        st,
                        $"End time: {NL + UnixTime(DateTime.Parse(item.End), "d") + NL + UnixTime(DateTime.Parse(item.End), "t")}",
                        et,
                        "From: Lodestone News",
                        XIVLN.Config.FFLogo,
                        item.Url,
                        Color.Blue);
                }
            }
            else
            {
                embed = CreateEmbed(
                    "I got nothing... " + Emote.Bot.Disconnecting_party,
                    "Is game on maintenance??? " + Emote.Bot.Disconnecting_party,
                    color: Color.Red);
            }

            return embed;

        }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Begu
{

    internal partial class Program
    {
        private async Task<string> GetLNId(string api, bool curr_maintenance = false)
        {
            HttpClient client = new HttpClient();
            string jsonCommon = await client.GetStringAsync(api);
            var newsListD = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonCommon);
            if (newsListD.Count > 0 )
            {
                return newsListD[0].Id;
            }
            else
            {
                return "0";
            }

        }
    }

    internal static class XIV
    {

        internal static class Config
        {
            internal static bool Enabled { get; set; } = true;
            internal static int MaxNewsOnFirst { get => 1; }
            internal static int Interval { get; set; } = 60 * 5; //S
            internal static int APIDelay { get; set; } = 2000; //MS
            internal static int TimerInterval { get => (Interval * 1000); }
            internal static string FFLogo { get => "https://cdn.discordapp.com/attachments/1303018554303451267/1315735962017337465/logoff.png?ex=67587e1a&is=67572c9a&hm=a8f8520741e13aaa619009fe29168aef9ff609ef566bf0409ae79e99d4e52d00&"; }

        }

        internal static class APIs
        {
            internal static string Topics => "https://lodestonenews.com/news/topics?locale=eu";
            internal static string Notices => "https://lodestonenews.com/news/notices?locale=eu";
            internal static string Updates => "https://lodestonenews.com/news/updates?locale=eu";
            internal static string Status => "https://lodestonenews.com/news/status?locale=eu";
            internal static string Maintenance => "https://lodestonenews.com/news/maintenance?locale=eu";
            internal static string MaintenanceCurrent => "https://lodestonenews.com/news/maintenance/current?locale=eu";

        }


    }
}

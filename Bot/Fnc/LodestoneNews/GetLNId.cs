using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
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
    }
}

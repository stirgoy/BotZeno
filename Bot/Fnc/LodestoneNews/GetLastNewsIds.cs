using System.Threading.Tasks;

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
    }
}

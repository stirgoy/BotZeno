using System;
using System.Collections.Generic;

namespace Begu
{
    internal class Structs { }

    //Lodestone structs
    //
    /********************
            news?
    *////////////////////

    public static class Emote
    {
        public static class Bot
        {
            public static string Online { get { return "<:Online:1313425723435782145>"; } }
            public static string GS { get { return "<:GS:1313427614106648596>"; } }
            public static string Roulette { get { return "<:Roulette:1313425986544599100>"; } }
            public static string TH { get { return "<:TH:1313425962020503645>"; } }
            public static string MSQ { get { return "<:MSQ:1313425932958171167>"; } }
            public static string Fisher { get { return "<:Fisher:1313425900662030346>"; } }
            public static string Fishing { get { return "<:Fishing:1313425859406987284>"; } }
            public static string DD { get { return "<:DD:1313425829295947826>"; } }
            public static string Gc { get { return "<:Gc:1313425797809176576>"; } }
            public static string FFXIV { get { return "<:FFXIV:1313425760295325696>"; } }
            public static string Maintenance { get { return "<:Maintenance:1313425674983444520>"; } }
            public static string LFP { get { return "<:LFP:1313425639621001258>"; } }
            public static string Returned { get { return "<:Returned:1313425603713695775>"; } }
            public static string Mentor { get { return "<:Mentor:1313425481273573376>"; } }
            public static string Sproud { get { return "<:Sproud:1313425390819344455>"; } }
            public static string Disconnecting_party { get { return "<a:Disconnecting_party:1313425258006712372>"; } }
            public static string Cactuar { get { return "<:Cactuar:1311094440215056445>"; } }
            public static string Boss { get { return "<:Boss:1311094313714974812>"; } }
            public static string Disconnecting { get { return "<:Disconnecting:1311089532527054909>"; } }

        }

        // 🟢 green
        //🔴 red
        public static class XD
        {
            public static string RedCircle { get { return "🔴"; } }
            public static string GeenCircle { get { return "🟢"; } }

        }
    }
    public class LodestoneNews
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }

    public class MaintenanceRoot
    {
        public List<MaintenanceEvent> Companion { get; set; }
        public List<MaintenanceEvent> Game { get; set; }
        public List<MaintenanceEvent> Lodestone { get; set; }
        public List<MaintenanceEvent> Mog { get; set; }
        public List<object> Psn { get; set; }
    }

    public class MaintenanceEvent
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool Current { get; set; }
    }


}

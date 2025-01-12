using System;

namespace Zeno
{
    public class XIVCollect_User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Portrait { get; set; }
        public string Avatar { get; set; }
        public DateTime Last_parsed { get; set; }
        public bool Verifed { get; set; }
        public XIVCollectUser_sc Mounts { get; set; }
        public XIVCollectUser_sc Minions { get; set; }
        public XIVCollectUser_sc Orchestrions { get; set; }
        public XIVCollectUser_sc Spells { get; set; }
        public XIVCollectUser_sc Emotes { get; set; }
        public XIVCollectUser_sc Bardings { get; set; }
        public XIVCollectUser_sc Hairstyles { get; set; }
        public XIVCollectUser_sc Armoires { get; set; }
        public XIVCollectUser_sc Fashions { get; set; }
        public XIVCollectUser_sc Records { get; set; }
        public XIVCollectUser_relics Relics { get; set; }

    }


    public class XIVCollectUser_sc
    {
        public int Count { get; set; }
        public int Total { get; set; }
    }

    public class XIVCollectUser_relics
    {
        public XIVCollectUser_sc Weapons { get; set; }
        public XIVCollectUser_sc Armor { get; set; }
        public XIVCollectUser_sc Tools { get; set; }
    }
}
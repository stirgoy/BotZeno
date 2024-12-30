using System;

namespace Zeno
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Portrait { get; set; }
        public string Avatar { get; set; }
        public DateTime Last_parsed { get; set; }
        public bool Verifed { get; set; }
        public User_sc Mounts { get; set; }
        public User_sc Minions { get; set; }
        public User_sc Orchestrions { get; set; }
        public User_sc Spells { get; set; }
        public User_sc Emotes { get; set; }
        public User_sc Bardings { get; set; }
        public User_sc Hairstyles { get; set; }
        public User_sc Armoires { get; set; }
        public User_sc Fashions { get; set; }
        public User_sc Records { get; set; }
        public User_relics Relics { get; set; }

    }


    public class User_sc
    {
        public int Count { get; set; }
        public int Total { get; set; }
    }

    public class User_relics
    {
        public User_sc Weapons { get; set; }
        public User_sc Armor { get; set; }
        public User_sc Tools { get; set; }
    }
}
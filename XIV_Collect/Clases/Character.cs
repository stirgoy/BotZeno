using System;

namespace Zeno
{

    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Data_center { get; set; }
        public string Portrait { get; set; }
        public string Avatar { get; set; }
        public DateTime Last_parsed { get; set; }
        public bool Verifed { get; set; }
        public Module_1 Achievements { get; set; }
        public Module_2 Mounts { get; set; }
        public Module_2 Minions { get; set; }
        public Module_3 Orchestrions { get; set; }
        public Module_3 Spells { get; set; }
        public Module_3 Emotes { get; set; }
        public Module_3 Bardings { get; set; }
        public Module_3 Hairstyles { get; set; }
        public Module_3 Armoires { get; set; }
        public Module_3 Fashions { get; set; }
        public Module_3 Records { get; set; }
        public Module_3 Survey_records { get; set; }
        public Module_3 Cards { get; set; }
        public Module_3 Npcs { get; set; }
        public Ranks Rankings { get; set; }
        public Relics Relics { get; set; }
        public Leves Leves { get; set; }



    }

    public class Module_1
    {
        public int Count { get; set; }
        public int Total { get; set; }
        public int Points { get; set; }
        public int Points_total { get; set; }
        public int Ranked_points { get; set; }
        public int Ranked_points_total { get; set; }
        public DateTime Ranked_time { get; set; }
        public bool Public { get; set; }
    }


    public class Module_2
    {
        public int Count { get; set; }
        public int Total { get; set; }
        public int Ranked_count { get; set; }
        public int Ranked_total { get; set; }
    }

    public class Module_3
    {
        public int Count { get; set; }
        public int Total { get; set; }
    }


    public class Module_4
    {
        public int Server { get; set; }
        public int Data_center { get; set; }
        public int Global { get; set; }
    }

    public class Ranks
    {
        public Module_4 Achievements { get; set; }
        public Module_4 Mounts { get; set; }
        public Module_4 Minions { get; set; }
    }

    public class Relics
    {
        public Module_3 Weapons { get; set; }
        public Module_3 Ultimate { get; set; }
        public Module_3 Armor { get; set; }
        public Module_3 Tools { get; set; }
    }

    public class Leves
    {
        public Module_3 Battlecraft { get; set; }
        public Module_3 Tradecraft { get; set; }
        public Module_3 Fieldcraft { get; set; }
    }


}

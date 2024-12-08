using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begu
{
    public class Query
    {
        public string NameEnStart { get; set; }
    }

    public class Behavior
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Source
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string RelatedType { get; set; }
        public int? RelatedId { get; set; }
    }

    public class SkillType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Verminion
    {
        public int Cost { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Hp { get; set; }
        public int Speed { get; set; }
        public bool AreaAttack { get; set; }
        public string Skill { get; set; }
        public string SkillDescription { get; set; }
        public int SkillAngle { get; set; }
        public int SkillCost { get; set; }
        public bool Eye { get; set; }
        public bool Gate { get; set; }
        public bool Shield { get; set; }
        public SkillType SkillType { get; set; }
    }

    public class MinionResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Enhanced_Description { get; set; }
        public string Tooltip { get; set; }
        public string Patch { get; set; }
        public int? ItemId { get; set; }
        public Behavior Behavior { get; set; }
        public Race Race { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public string Owned { get; set; }
        public List<Source> Sources { get; set; }
        public Verminion Verminion { get; set; }
    }

    public class MinionRoot
    {
        public Query Query { get; set; }
        public int Count { get; set; }
        public List<MinionResult> Results { get; set; }
    }
}

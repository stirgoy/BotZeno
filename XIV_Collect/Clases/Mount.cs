using System.Collections.Generic;

namespace Zeno
{

    public class MountQuery
    {
        public string NameEnEnd { get; set; }
    }

    public class MountSource
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string RelatedType { get; set; }
        public int RelatedId { get; set; }
    }

    public class MountResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Enhanced_Description { get; set; }
        public string Tooltip { get; set; }
        public string Movement { get; set; }
        public int Seats { get; set; }
        public int Order { get; set; }
        public int OrderGroup { get; set; }
        public string Patch { get; set; }
        public int? ItemId { get; set; }
        public string Owned { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public List<MountSource> Sources { get; set; }
    }

    public class MountRoot
    {
        public MountQuery Query { get; set; }
        public int Count { get; set; }
        public List<MountResult> Results { get; set; }
    }
}

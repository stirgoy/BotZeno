using System.Collections.Generic;

namespace Zeno
{


    public class OutfitQuery
    {
        public string NameEnStart { get; set; }
    }

    public class OutfitItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }

    public class OutfitSource
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string RelatedType { get; set; }
        public int? RelatedId { get; set; }
    }

    public class OutfitResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Patch { get; set; }
        public string Gender { get; set; }
        public bool Armoireable { get; set; }
        public int? ItemId { get; set; }
        public bool Tradeable { get; set; }
        public string Owned { get; set; }
        public string Icon { get; set; }
        public List<OutfitItem> Items { get; set; }
        public List<OutfitSource> Sources { get; set; }
    }

    public class OutfitRoot
    {
        public OutfitQuery Query { get; set; }
        public int Count { get; set; }
        public List<OutfitResult> Results { get; set; }
    }



}

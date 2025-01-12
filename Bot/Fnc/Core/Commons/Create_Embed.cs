using Discord;

namespace Zeno
{
    internal partial class Program
    {


        static Embed CreateEmbed(string title, string description = "")
        {
            return new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .Build();
        }

        static Embed CreateEmbed(string title, string description = "", string footer = "", string miniImage = "", string image = "", Color? color = null)
        {
            Color embedColor = color ?? Color.Green;
            return new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithFooter(footer)
                .WithThumbnailUrl(miniImage)
                .WithImageUrl(image)
                .WithColor(embedColor)
                .Build();
        }


        static Embed CreateEmbedField_1(string title, string description, string field1a, string field1b, string footer = "", string miniImage = "", string image = "", Color? color = null)
        {
            Color embedColor = color ?? Color.Green;
            return new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .AddField(field1a, field1b, true)
                .WithFooter(footer)
                .WithThumbnailUrl(miniImage)
                .WithImageUrl(image)
                .WithColor(embedColor)
                .Build();
        }

        static Embed CreateEmbedField_2(string title, string description, string field1a, string field1b, string field2a, string field2b, string footer = "", string miniImage = "", string image = "", Color? color = null)
        {
            Color embedColor = color ?? Color.Green;
            return new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .AddField(field1a, field1b, true)
                .AddField(field2a, field2b, true)
                .WithFooter(footer)
                .WithThumbnailUrl(miniImage)
                .WithImageUrl(image)
                .WithColor(embedColor)
                .Build();
        }

    }
}

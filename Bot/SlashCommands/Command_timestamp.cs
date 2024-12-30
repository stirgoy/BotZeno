using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Command_timestamp(SocketSlashCommand command)
        {
            var msg = command.Data.Options.FirstOrDefault(opt => opt.Name == "time");
            if (msg?.Value is string user_msg) { } else { return; }//exit on fail

            await command.DeferAsync(ephemeral: true);

            if (string.IsNullOrEmpty(user_msg))
            {
                await command.RespondAsync("Please provide a valid date time my friend in the format YYYY-MM-DD HH:mm:ss.", ephemeral: true);
                return;
            }

            if (!DateTime.TryParse(user_msg, out DateTime parsedDate))
            {
                await command.RespondAsync("Invalid date format my friend. Use YYYY-MM-DD HH:mm:ss.", ephemeral: true);
                return;
            }

            //parsedDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc); //UTC ????? LMAO xD

            long unixTimestamp = new DateTimeOffset(parsedDate).ToUnixTimeSeconds();
            string discordTimestamp = $"<t:{unixTimestamp}:R>";

            var talkc_embT = new EmbedBuilder()
                .WithTitle("Timestamp")
                .WithDescription($"Here's your Discord timestamp my enemy:")
                .AddField(discordTimestamp, $"`{discordTimestamp}`")
                .WithFooter($" My friend.")
                .WithColor(Color.Green)
                .Build();
            await command.FollowupAsync("", embed: talkc_embT, ephemeral: true);

        }
    }
}

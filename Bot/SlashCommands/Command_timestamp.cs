using Discord.WebSocket;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        private async Task Command_timestamp(SocketSlashCommand command, string user_msg)
        {
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
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            await command.FollowupAsync("", embed: talkc_embT, ephemeral: true);

        }
    }
}

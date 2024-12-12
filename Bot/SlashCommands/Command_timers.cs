using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        private async Task Command_timers(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: false);

            DateTime ahora = DateTime.Now;
            bool isDST = ahora.IsDaylightSavingTime();

            //dst handler
            int weekly_hour = isDST ? 10 : 9;
            int daily_hour = isDST ? 17 : 16;
            int GC_hour = isDST ? 22 : 21;

            // Weekly timer
            DateTime weekly = new DateTime(ahora.Year, ahora.Month, ahora.Day, weekly_hour, 0, 0)
                .AddDays(((int)DayOfWeek.Tuesday - (int)ahora.DayOfWeek + 7) % 7);
            if (ahora >= weekly) weekly = weekly.AddDays(7); //move to next tuesday if passed
            long unixW = new DateTimeOffset(weekly).ToUnixTimeSeconds();
            string weeklyDT = $"<t:{unixW}:R>";

            // Daily timer
            DateTime daily = new DateTime(ahora.Year, ahora.Month, ahora.Day, daily_hour, 0, 0);
            if (ahora >= daily) daily = daily.AddDays(1); //move to next day if passed
            long unixD = new DateTimeOffset(daily).ToUnixTimeSeconds();
            string dailyDT = $"<t:{unixD}:R>";

            // GC timer
            DateTime gc = new DateTime(ahora.Year, ahora.Month, ahora.Day, GC_hour, 0, 0);
            if (ahora >= gc) gc = gc.AddDays(1); //move to next day if passed
            long unixG = new DateTimeOffset(gc).ToUnixTimeSeconds();
            string gcDT = $"<t:{unixG}:R>";

            // Ocean Fishing timer
            DateTime ocean = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, 0, 0)
                .AddHours(2 - (ahora.Hour % 2));
            long unixO = new DateTimeOffset(ocean).ToUnixTimeSeconds();
            string oceanDT = $"<t:{unixO}:R>";

            // Cacpot Lottery timer
            DateTime cacpot = new DateTime(ahora.Year, ahora.Month, ahora.Day, 20, 0, 0)
                .AddDays(((int)DayOfWeek.Saturday - (int)ahora.DayOfWeek + 7) % 7);
            long unixC = new DateTimeOffset(cacpot).ToUnixTimeSeconds();
            string cacpotDT = $"<t:{unixC}:R>";

            long curt = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            string timenow = $"<t:{curt}:R>";


            // Embed response
            var embed = new EmbedBuilder()
                .WithTitle($"Eorzean timers. " + Emote.Bot.FFXIV)
                .WithDescription(Emote.Bot.Sproud + " Because you looks lost. " + Emote.Bot.Sproud + Environment.NewLine + "-# " + timenow + Environment.NewLine)
                .WithColor(Color.Green)
                .AddField(Emote.Bot.MSQ + " Weekly", weeklyDT, true)
                .AddField(Emote.Bot.Roulette + " Daily", dailyDT, true)
                .AddField(Emote.Bot.Gc + " GC", gcDT, true)
                .AddField(Emote.Bot.Fishing + " Ocean fishing", oceanDT, true)
                .AddField(Emote.Bot.Cactuar + " Cacpot", cacpotDT, true)
                //.WithFooter("Take care. ")
                //.WithTimestamp(DateTimeOffset.Now)
                .Build();

            await command.FollowupAsync("", embed: embed);

        }
    }
}

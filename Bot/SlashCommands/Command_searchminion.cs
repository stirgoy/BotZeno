using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Command_searchminion(SocketSlashCommand command)
        {

            var minion = command.Data.Options.FirstOrDefault(opt => opt.Name == "minion");
            if (minion?.Value is string minionname) { } else { return; }//exit on fail

            await command.DeferAsync(ephemeral: false);

            try
            {
                string apiUrl = $"{XIV_Collect.Apis.Minion}{minionname}";
                HttpClient client = new HttpClient();


                string jsonResponse = await client.GetStringAsync(apiUrl);
                Embed user_emb;

                MinionRoot minionData = JsonConvert.DeserializeObject<MinionRoot>(jsonResponse);
                MinionResult r = minionData.Results.First();
                //MessageComponent buttos = null;

                if (minionData.Results.Count == 0)
                {
                    user_emb = new EmbedBuilder()
                        .WithTitle($"I found.")
                        .AddField("Nothing...", $"{Emote.Bot.Disconnecting_party}", true)
                        .WithColor(Color.Red)
                        .Build();
                }
                else if (minionData.Results.Count == 1)
                {
                    string ways = "No data.";

                    foreach (Source item in r.Sources)
                    {
                        if (item.Text == r.Sources.First().Text) { ways = ""; }
                        ways += "**" + item.Type + "** - " + item.Text;
                        if (item.Text != r.Sources.Last().Text) { ways = NL; }
                    }


                    user_emb = new EmbedBuilder()
                        .WithTitle($"Results for: " + minionname)
                        .AddField("Name", $"{r.Name}", true)
                        .AddField("Behavior", $"{r.Behavior.Name}", true)
                        .AddField("Race", r.Race.Name, true)
                        .AddField("Patch", $"{r.Patch}", true)
                        .AddField("Owned by ", r.Owned, true)
                        .AddField("Description", r.Description + " - " + r.Enhanced_Description, true)
                        .AddField("Sources", ways, false)
                        .WithFooter(r.Tooltip)
                        .WithImageUrl(r.Image)
                        .WithThumbnailUrl(r.Icon)
                        .WithColor(Color.Purple)
                        .Build();
                }
                else
                {   //here -> (mountData.Results.Count > 1)
                    string xD = "";

                    foreach (MinionResult item in minionData.Results)
                    {
                        xD += "- **" + item.Name + "**" + NL;
                    }

                    user_emb = new EmbedBuilder()
                        //.WithTitle(Emote.Bot.Mounts + $"**Results for: __{mountname}__** ")
                        .WithFooter("Results for: " + minionname)
                        .WithColor(Color.LightOrange)
                        .WithDescription(xD)
                            .Build();

                }

                var del = await command.FollowupAsync("", embed: user_emb, ephemeral: false);
                BorrarMsg(del, 120);
            }
            catch (Exception ex)
            {
                Print(ex.Message);
                var user_emb = new EmbedBuilder()
                        .WithTitle(Emote.Bot.Mounts + "**Found nothing...** ")
                        .WithDescription($"There is no minions that name starts with: `{minionname}` {Emote.Bot.Boss}")
                        .WithColor(Color.Red)
                            .Build();
                await command.FollowupAsync("", embed: user_emb, ephemeral: false);
            }
        }
    }


}

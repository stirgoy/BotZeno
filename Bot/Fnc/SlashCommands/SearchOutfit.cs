using Discord.Rest;
using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async void SearchOutfit(RestFollowupMessage command, string outfitname)
        {
            try
            {
                string apiUrl = $"{XIV_Collect.Apis.Outfit}{outfitname}";
                HttpClient client = new HttpClient();


                string jsonResponse = await client.GetStringAsync(apiUrl);
                Embed user_emb;

                OutfitRoot outfitdata = JsonConvert.DeserializeObject<OutfitRoot>(jsonResponse);
                OutfitResult r = outfitdata.Results.First();

                if (outfitdata.Results.Count == 0)
                {
                    user_emb = new EmbedBuilder()
                        .WithTitle($"I found.")
                        .AddField("Nothing...", $"{Emote.Bot.Disconnecting_party}", true)
                        .WithColor(Color.Red)
                        .Build();
                }
                else if (outfitdata.Results.Count == 1)
                {
                    string ways = "No data.";
                    string oitems = "";

                    foreach (OutfitSource item in r.Sources)
                    {
                        if (item.Text == r.Sources.First().Text) { ways = ""; }
                        //ways += "**" + item.Type + "** - ";
                        ways += "**" + item.Type + "** - " + item.Text;
                        if (item.Text != r.Sources.Last().Text) { ways += NL; }
                    }

                    foreach (OutfitItem item in r.Items)
                    {
                        oitems += $"* {item.Name}";
                        if (item.Name != r.Items.Last().Name) { oitems += NL; }
                    }

                    if (string.IsNullOrEmpty(oitems)) { oitems = "Error"; }

                    user_emb = new EmbedBuilder()
                        .WithTitle($"Results for: " + outfitname)
                        .AddField("Name", $"{r.Name}", true)
                        .AddField("Patch", $"{r.Patch}", true)
                        .AddField("Owned by ", r.Owned, true)
                        //.AddField("Gender", r.Gender, false)
                        .AddField("Items", oitems, false)
                        .AddField("Sources", ways, false)
                        .WithThumbnailUrl(r.Icon)
                        .WithColor(Color.Gold)
                        .Build();
                }
                else
                {   //here -> (mountData.Results.Count > 1)
                    string xD = "";

                    foreach (OutfitResult item in outfitdata.Results)
                    {
                        xD += "- **" + item.Name + "**" + NL;
                    }

                    user_emb = new EmbedBuilder()
                        //.WithTitle(Emote.Bot.Mounts + $"**Results for: __{mountname}__** ")
                        .WithFooter("Results for: " + outfitname)
                        .WithColor(Color.LightOrange)
                        .WithDescription(xD)
                            .Build();

                }

                await command.ModifyAsync(msg => msg.Embed = user_emb);
                await command.ModifyAsync(msg => msg.Content = "");
                //var del = await command.ReplyAsync("", embed: user_emb, ephemeral: false);
                BorrarMsg(command, 120);
            }
            catch (Exception ex)
            {
                Print(ex.Message);
                var user_emb = new EmbedBuilder()
                        .WithTitle(Emote.Bot.Mounts + "**Found nothing...** ")
                        .WithDescription($"There is no mounts that name starts with: `{outfitname}` {Emote.Bot.Boss}")
                        .WithColor(Color.Red)
                            .Build();
                await command.ModifyAsync(msg => msg.Embed = user_emb);
                await command.ModifyAsync(msg => msg.Content = "");
                BorrarMsg(command, 120);
                //await command.FollowupAsync("", embed: user_emb, ephemeral: false);
            }
        }
    }
}

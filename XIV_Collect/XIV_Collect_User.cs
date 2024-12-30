using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        async Task XIVCollect_User(SocketMessage message, ulong user = 0)
        {
            try
            {

                HttpClient client = new HttpClient();
                string jsonCommon;
                if ( user > 0)
                {
                    jsonCommon = await client.GetStringAsync(XIV_Collect.Apis.Users + "/" + user);
                }
                else
                {
                    jsonCommon = await client.GetStringAsync(XIV_Collect.Apis.Users + "/" + message.Author.Id);
                }
                if (string.IsNullOrEmpty(jsonCommon))
                {
                    Print("NO USER FOUND");
                }
                else
                {
                    var ffuser = JsonConvert.DeserializeObject<User>(jsonCommon);
                    string nl = NL;
                    string lndata = await client.GetStringAsync( XIV_Collect.Apis.Character + ffuser.Id);
                    Character userln = JsonConvert.DeserializeObject<Character>(lndata);

                    double acp = Math.Round(((double)userln.Achievements.Count / userln.Achievements.Total) * 100, 2);
                    double mountp = Math.Round(((double)ffuser.Mounts.Count / ffuser.Mounts.Total) * 100, 2);
                    double minionp = Math.Round(((double)ffuser.Minions.Count / ffuser.Minions.Total) * 100, 2);
                    double weapp = Math.Round(((double)userln.Relics.Weapons.Count / userln.Relics.Weapons.Total) * 100, 2);
                    double armp = Math.Round(((double)userln.Relics.Armor.Count / userln.Relics.Armor.Total) * 100, 2);
                    double toolp = Math.Round(((double)userln.Relics.Tools.Count / userln.Relics.Tools.Total) * 100, 2);
                    double ultip = Math.Round(((double)userln.Relics.Ultimate.Count / userln.Relics.Ultimate.Total) * 100, 2);


                    var talkc_embD = new EmbedBuilder()
                      .WithTitle(ffuser.Name + " - " + ffuser.Server)
                      .AddField("Achievements" + nl + userln.Achievements.Count + "/" + userln.Achievements.Total + " - " + acp + "% - " + userln.Achievements.Points + "Points", "Server: " + userln.Rankings.Achievements.Server + nl + "Data center: " + userln.Rankings.Achievements.Data_center + nl + "Global: " + userln.Rankings.Achievements.Global, true)
                      .AddField("Mounts" + nl + ffuser.Mounts.Count + "/" + ffuser.Mounts.Total + " - " + mountp + "%", "Server: " + userln.Rankings.Mounts.Server + nl + "Data center: " + userln.Rankings.Mounts.Data_center + nl + "Global: " + userln.Rankings.Mounts.Global, true)
                      .AddField("Minions" + nl + ffuser.Minions.Count + "/" + ffuser.Minions.Total + " - " + minionp + "%", "Server: " + userln.Rankings.Minions.Server + nl + "Data center: " + userln.Rankings.Minions.Data_center + nl + "Global: " + userln.Rankings.Minions.Global, true)
                      .AddField("Weapon relics - " + weapp + "%", userln.Relics.Weapons.Count + "/" + userln.Relics.Weapons.Total, true)
                      .AddField("Armor relics - " + armp + "%", userln.Relics.Armor.Count + "/" + userln.Relics.Armor.Total, true)
                      .AddField("Tool relics - " + toolp + "%", userln.Relics.Tools.Count + "/" + userln.Relics.Tools.Total, true)
                      .AddField("Ultimate Weapons - " + ultip + "%", userln.Relics.Ultimate.Count + "/" + userln.Relics.Ultimate.Total, true)
                      .WithImageUrl(ffuser.Portrait)
                      .WithThumbnailUrl(ffuser.Avatar)
                      .WithColor(Color.Green)
                      .Build();
                    await message.Channel.SendMessageAsync("", embed: talkc_embD);
                }
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

    }
}

using Discord;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        //              /userinfo
        private async Task Command_a_userinfo(SocketSlashCommand command)
        {
            var user_n = command.Data.Options.FirstOrDefault(opt => opt.Name == "name");
            if (user_n?.Value is IUser user_name) { } else { return; }

            await command.DeferAsync(ephemeral: true);

            SocketGuildUser sgu = Kuru.GetUser(user_name.Id);
            //              lulu                str
            //print(user_name.GlobalName + " " + user_name.Username);
            Print($"{command.User.Username} => a_userinfo {user_n}");

            string roles = "Error getting roles. " + Emote.Bot.Boss;

            if (sgu != null)
            {
                roles = "";

                foreach (var item in sgu.Roles)
                {
                    if (item.Name == sgu.Roles.Last().Name)
                    {
                        roles += item + ".";
                    }
                    else
                    {
                        roles += item + ", ";
                    }
                }

            }
            else
            {
                Print("NULL USER");
            }

            var roleList = string.Join(", ", sgu.Roles.Where(x => !x.IsEveryone).Select(x => x.Mention));
            roleList.Remove(roleList.Length - 2);
            roleList += ".";

            string nik = sgu.Nickname;
            if (nik == null) { nik = "none"; }
            string avatarUrl = sgu.GetAvatarUrl(ImageFormat.Auto, 512);

            string admin = (sgu.GuildPermissions.Administrator) ? Emote.XD.GeenCircle : Emote.XD.RedCircle;

            var user_emb = new EmbedBuilder()
                .WithTitle($"User information")
                .AddField("Display name", $"{sgu.DisplayName}", true)
                .AddField("Discord name", $"{sgu.Username}", true)
                .AddField("Global name", $"{sgu.GlobalName}", true)
                .AddField("Server name", $"{nik}", true)
                .AddField("Is admin", admin, true)
                .AddField("Roles", $"{roleList}")
                .WithThumbnailUrl(avatarUrl)
                .WithFooter($" My enemy.")
                .WithColor(Color.Orange)
                .Build();

            var m = await command.FollowupAsync("", embed: user_emb, ephemeral: true);
            BorrarMsg(m, 60);
        }
    }
}

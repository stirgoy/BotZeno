using Discord;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Command_warn(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: true);

            var param_user = command.Data.Options.FirstOrDefault(opt => opt.Name == "name");
            var param_reason = command.Data.Options.FirstOrDefault(opt => opt.Name == "reason");

            if (param_user?.Value is IUser user) { } else { return; }//exit on fail
            if (param_reason?.Value is string reason) { } else { return; }//exit on fail
            if (user == null) { Print($"WARN ERROR: user is null"); return; }
            if (string.IsNullOrEmpty(reason)) { reason = Emote.Bot.Boss; }

            await AddWarn(user.Id, 2);

            var dm = await user.CreateDMChannelAsync();
            await dm.SendMessageAsync($"Hey {user.Username}{NL}I don't like send this messages but i need to warn you:{NL}{reason}");

            var del = await command.FollowupAsync($"{user.Username} has warned!",ephemeral: true);
            BorrarMsg(del, 20);

        }
    }
}

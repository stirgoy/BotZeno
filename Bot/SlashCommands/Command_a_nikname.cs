using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        async Task Command_a_nikname(SocketSlashCommand command)
        {
            var user_nn = command.Data.Options.FirstOrDefault(opt => opt.Name == "name");
            var user_nnn = command.Data.Options.FirstOrDefault(opt => opt.Name == "nik");

            if (user_nn?.Value is IUser mentionuser) { } else { return; }
            if (user_nnn?.Value is string newnik) { } else { return; }
            if (mentionuser == null) { return; }
            if (string.IsNullOrEmpty(newnik)) { return; }
            Print($"{command.User.Username} => a_nikname {mentionuser.GlobalName} - {newnik}");
            string oldnik = "";
            try
            {
                await command.DeferAsync(ephemeral: true);

                SocketGuildUser user = null;
                foreach (SocketGuildUser kuruuser in Kuru.Users)
                {
                    if (kuruuser.Id == mentionuser.Id)
                    {
                        user = kuruuser;
                        break;
                    }
                }

                oldnik = user.Mention;

                if (user != null)
                {
                    await user.ModifyAsync(u => u.Nickname = newnik);
                    await command.FollowupAsync($"{oldnik} now is called {newnik} on Kuru server.");
                }


            }
            catch (Exception ex)
            {
                Print(ex.Message);
                await command.FollowupAsync($"Error: {oldnik} still with same nikname.{NL}{ex.Message}");

            }
        }
    }
}

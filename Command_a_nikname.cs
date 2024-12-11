using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        async Task Command_a_nikname(SocketSlashCommand command, IUser mentionuser, string newnik)
        {
            if (mentionuser == null) { return; }
            if (string.IsNullOrEmpty(newnik)) { return; }
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
                await command.FollowupAsync($"Error: {oldnik} still with same nikname.{Environment.NewLine}{ex.Message}");

            }
        }
    }
}

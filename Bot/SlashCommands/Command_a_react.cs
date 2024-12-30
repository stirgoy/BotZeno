using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task Command_a_react(SocketSlashCommand command)
        {
            var mesglink = command.Data.Options.FirstOrDefault(opt => opt.Name == "message_link");
            var emote = command.Data.Options.FirstOrDefault(opt => opt.Name == "emote");

            if (mesglink?.Value is string message) { } else { return; }//exit on fail
            if (emote?.Value is string reaction) { } else { return; }//exit on fail

            if (string.IsNullOrEmpty(message)) { return; }
            if (string.IsNullOrEmpty(reaction)) { return; }
            Print($"{command.User.Username} => a_react -  {message} - {reaction}");
            try
            {
                await command.DeferAsync(ephemeral: true);

                Match match = Regex.Match(message, @"https:\/\/discord\.com\/channels\/(\d+)\/(\d+)\/(\d+)");

                if (match.Success)
                {
                    ulong guildId = ulong.Parse(match.Groups[1].Value); // ID del servidor
                    ulong channelId = ulong.Parse(match.Groups[2].Value); // ID del canal
                    ulong messageId = ulong.Parse(match.Groups[3].Value); // ID del mensaje

                    var channel = Bot_Zeno.GetChannel(channelId) as ITextChannel;
                    IMessage messageToFetch = null;

                    if (channel != null)
                    {
                        messageToFetch = await channel.GetMessageAsync(messageId);
                    }

                    bool good = false;
                    if (messageToFetch != null)
                    {
                        Emoji.TryParse(reaction, out var emoji);
                        if (emoji != null)
                        {
                            var delete = await command.FollowupAsync("Done ♥", ephemeral: true);
                            BorrarMsg(delete);

                            await messageToFetch.AddReactionAsync(emoji);
                            good = true;
                        }
                    }

                    if (!good)
                    {
                        var delete = await command.FollowupAsync("Something is wrong...", ephemeral: true);
                        BorrarMsg(delete);

                    }

                }
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }



    }
}

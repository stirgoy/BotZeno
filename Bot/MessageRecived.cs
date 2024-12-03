using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        /********************
         MessageReceivedAsync
        *////////////////////
        private Task MessageReceivedAsync(SocketMessage message)
        {
            /*
            var userMessage = message as SocketUserMessage;
            if (message.Author.IsBot || userMessage == null) return;
            if (!Check_Allowed_Channel(message.Channel)) { return; }

            await userMessage.Channel.TriggerTypingAsync();

            string answer = "hmm...";
            bool sayhi = false;
            var mentionedUser = message.MentionedUsers.FirstOrDefault(u => u.Id == _client.CurrentUser.Id);

            if (userMessage.Content.ToString().ToLower().IndexOf("hi", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var emoj = new Emoji("❤️");
                await message.AddReactionAsync(emoj);
                answer = $"¡Hi, {message.Author.Mention}♥! 😊";
                sayhi = true;
                //await userMessage.Channel.SendMessageAsync($"¡Hi, {message.Author.GlobalName}♥! 😊");
            }

            if (mentionedUser != null)
            {
                //var emoj = new Emoji(":upside_down:");                

                //await message.AddReactionAsync(emoj);
                answer = $"Oh a mention!!!, {message.Author.Mention}♥! wanna be my friend 😊? or my enemy :smiling_imp: ?";

                if (sayhi)
                {
                    answer = "https://tenor.com/view/zenos-zenos-yae-galvus-ffxiv-final-fantasy14-final-fantasy-gif-25294169";
                }
            }
            var reference = new MessageReference(message.Id);

            // Responder directamente al mensaje original
            await message.Channel.SendMessageAsync(answer, false, null, null, null, reference);
            //await userMessage.Channel.SendMessageAsync(answer);
            */
            return Task.CompletedTask;
        }
    }
}

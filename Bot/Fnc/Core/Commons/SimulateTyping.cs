using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /********************
            AnswerUser
        *//////////////////// 
        async void SimulateTyping(SocketUserMessage msg, string content)
        {
#if !DEBUG
            await msg.Channel.TriggerTypingAsync();
            await Task.Delay(3000);
#endif
            await msg.ReplyAsync(content);
        }
    }
}

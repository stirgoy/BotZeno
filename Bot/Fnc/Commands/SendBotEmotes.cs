using Discord.WebSocket;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zeno
{
    internal partial class Program
    {

        /********************
            SendBotEmotes
        *//////////////////// bot emotes bulk
        private static async void SendBotEmotes(ISocketMessageChannel channel)
        {
            string emot = "";
            List<string> msgs = new List<string>();
            var properties = typeof(Emote.Bot).GetProperties(BindingFlags.Public | BindingFlags.Static);

            int c = 1;
            int m = 20;

            foreach (var property in properties)
            {
                var value = property.GetValue(null);
                //StringCollection anims = new StringCollection { "Pepo_laugh" };

                emot += value.ToString();
                if (property != properties.Last()) { emot += " "; }
                if (c == m)
                {
                    msgs.Add(emot);
                    c = 0;
                    emot = "";

                }
                c++;
            }

            if (msgs.Last() != emot) { msgs.Add(emot); }

            foreach (var item in msgs)
            {
                if (item != "")
                {
                    await channel.SendMessageAsync(item);
                }

            }
        }

    }
}

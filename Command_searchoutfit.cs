using Discord;
using Discord.Rest;
using Discord.WebSocket;
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
        private async Task Command_searchoutfit(SocketSlashCommand command)
        {
            var outfit = command.Data.Options.FirstOrDefault(opt => opt.Name == "outfit");
            if (outfit?.Value is string outfitname) { } else { return; }//exit on fail

            await command.DeferAsync(ephemeral: false);

            RestFollowupMessage answer = await command.FollowupAsync($"{Emote.Bot.Mentor} I'm working on it {Emote.Bot.Mentor}",ephemeral: false);
            // we can block getaway waiting for this API

            SearchOutfit(answer, outfitname); 

        }

        

    }
}

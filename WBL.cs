﻿using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {

        private async Task WBL(SocketMessage message)
        {
            var author = message.Author;
            if (author.IsBot) { return; }
            
            string msgcontent = message.Content.ToLower();

            foreach (string item in WBL_List)
            {
                Regex re = new Regex($@"\b{item}\b",RegexOptions.IgnoreCase);
                if (re.IsMatch(msgcontent))
                {
                    await message.DeleteAsync();

                    var serverChannel = Kuru.GetTextChannel(message.Channel.Id);

                    string log = $"Baned word detected. User: {message.Author.Username} typed: {item} on channel: {message.Channel.Name}";
                    string logz = $"Baned word detected. User: {message.Author.Mention} typed: `{item}` on channel: {serverChannel.Mention}";
                    Print(log);
                    await ZenoLog(logz);

                    break; //one is enough
                }
            }

        }
    }
}

using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        private async void logg(string tt)
        {
            Print(tt);
            await ZenoLog(tt);

        }
        /********************
         MessageReceivedAsync
        *////////////////////
        private async Task MessageReceivedAsync(SocketMessage message)
        {
            var userMessage = message as SocketUserMessage;
            var serveruser = Kuru.GetUser(userMessage.Author.Id);
            var channel = Kuru.GetTextChannel(userMessage.Channel.Id);
            if (message.Author.IsBot || userMessage == null) return;
            //if (!Check_Allowed_Channel(message.Channel)) { return; }

            //app comands
            if (serveruser.GuildPermissions.Administrator) //only admins
            {

                if (message.Content.StartsWith("!del-"))
                {

                    await message.DeleteAsync();

                    try
                    {
                        int cant = int.Parse(message.Content.Split('-')[1]);
                        if (cant > 0)
                        {
                            MassDelete(message, cant);
                        }

                    }
                    catch (Exception) { }

                }
                else if (message.Content.StartsWith("!AppCmd-"))
                {
                    string[] msg_splited = message.Content.Split('-');
                    string command = msg_splited[1];


                    switch (command)
                    {
                        case "reset": //restart bot
                            await message.DeleteAsync();
                            string exePath = Process.GetCurrentProcess().MainModule.FileName;
                            Process.Start(exePath);
                            Environment.Exit(0);
                            break;
                        case "xivln": //run news, i forgot swap XIV_LN_enabled....
                            await message.DeleteAsync();
                            if (!XIV_LN_enabled)
                            {
                                XIV_LN_enabled = true;
                                XIV_LN();
                            }
                            break;
                        case "ping": //bot alive?
                            await userMessage.ReplyAsync("Pong " + Emote.Bot.Online);
                            break;
                        case "emote": //answers wiith bot emotes
                            await message.DeleteAsync();
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
                                    await userMessage.Channel.SendMessageAsync(item);
                                }

                            }
                            break;
                        case "reconnect":
                            await message.DeleteAsync();
                            Reconnect();
                            break;
                        case "play":
                            await message.DeleteAsync();
                            string game = "Final Fantasy XIV";
                            if (msg_splited.Count() >= 3) 
                            {
                                game = "";
                                foreach (string item in msg_splited)
                                {
                                    if (item != msg_splited[0] && item != msg_splited[1]) { game += item; }
                                }
                            }
                            await _client.SetGameAsync(game, type: ActivityType.Playing);
                            break;

                    }

                    string log = $"{message.Author.Mention} used !AppCmd-{command} on {Kuru.GetTextChannel(channel.Id).Mention}";
                    logg(log);

                    return;
                }
            }

            //lets react to bot mentions
            var mentioned = message.MentionedUsers.FirstOrDefault(u => u.Id == _client.CurrentUser.Id);
            if (mentioned != null)
            {
                if (mentioned.Mention == _client.CurrentUser.Mention)
                {
                    var emoj = new Emoji("🔥");
                    await userMessage.AddReactionAsync(emoj);
                }
            }

            return;
        }

        /*
        if(userMessage.Author.Id != 247404719608168458) return;
        await userMessage.Channel.SendMessageAsync(":3");
        GZenos();
        Print("done");
        */
        /*
    await userMessage.Channel.TriggerTypingAsync();
    await Task.Delay(4000);


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

    }
}


using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /********************
         MessageReceivedAsync
        *////////////////////
        private async Task MessageReceivedAsync(SocketMessage message)
        {
            if (!Config.Channels.TalkChannel.Contains(message.Channel.Id.ToString())) return;

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

                    string log = $"{message.Author.Username} used !AppCmd-{message.Content} on {Kuru.GetTextChannel(channel.Id).Name}";
                    Print(log);

                    switch (command)
                    {
                        case "reset": //restart bot
                            await message.DeleteAsync();
                            ResetApp();
                            break;

                        case "xivln": //run news, i forgot swap XIV_LN_enabled....
                            await message.DeleteAsync();
                            if (!Config.XIV_LN_enabled)
                            {
                                Config.XIV_LN_enabled = true;
                                XIV_LN();
                            }
                            break;

                        case "ping": //bot alive?
                            await userMessage.ReplyAsync("Pong " + Emote.Bot.Online);
                            break;

                        case "emote": //answers wiith bot emotes
                            await message.DeleteAsync();
                            SendBotEmotes(userMessage.Channel);

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

                            Config.Playing = game;
                            await Config_Save();
                            await Bot_Zeno.SetCustomStatusAsync(game);

                            break;

                        case "off":
                            await message.DeleteAsync();
                            await message.Channel.SendMessageAsync($"Sayonara! {Emote.Bot.Disconnecting_party}");
                            await Bot_Zeno.StopAsync();
                            Environment.Exit(0);
                            break;

                        /*
                         */
#if DEBUG

                        case "test":
                            string userm = "";
                            ulong uid = 0;

                            if (msg_splited.Count() >= 3)
                            {
                                userm = "";
                                if (msg_splited[2] != null) { userm = msg_splited[2]; }
                                if (userm != "")
                                {

                                    userm = userm.Substring(2);

                                    userm = userm.Substring(0, userm.Length - 1);

                                    uid = ulong.Parse(userm);
                                }
                            }
                            if (uid > 0)
                            {
                                await XIVCollect_User(message, uid);
                            }
                            else
                            {
                                await XIVCollect_User(message);
                            }

                            break;
#endif
                    }


                    return;
                }
            }// if admin


            if (Config.Channels.TalkChannel.Contains(channel.Id.ToString()))
            {// allowed channels only
                string tcont = message.Content;

                if (RegExFind(ZenoTalk.Greetings, ZenoTalk.Help, tcont)) //help
                {
                    Random rng = new Random();
                    int indexr = rng.Next(ZenoTalk.answer_help.Length);
                    AnswerUser(userMessage, string.Format(ZenoTalk.answer_help[indexr], serveruser.Mention, NL));
                    //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_help[indexr], serveruser.Mention, NL));


                }
                else if (RegExFind(ZenoTalk.Macros, ZenoTalk.Help, tcont)) //macros
                {
                    Random rng = new Random();
                    int indexr = rng.Next(ZenoTalk.answer_macro.Length);
                    AnswerUser(userMessage, string.Format(ZenoTalk.answer_macro[indexr], serveruser.Mention, NL));
                    //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_macro[indexr], serveruser.Mention, NL));


                }
                else if (RegExFind(ZenoTalk.Macros, ZenoTalk.Menu, tcont)) //macros > menu
                {

                    Random rng = new Random();
                    int indexr = rng.Next(ZenoTalk.answer_menu.Length);
                    AnswerUser(userMessage, string.Format(ZenoTalk.answer_menu[indexr], serveruser.Mention, NL));
                    //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_menu[indexr], serveruser.Mention, NL));

                    IDMChannel dm = await userMessage.Author.CreateDMChannelAsync();
                    SendGuide(dm, 0);



                }
                else if (RegExFind(ZenoTalk.Retainers, ZenoTalk.Help, tcont))//retainers
                {
                    Random rng = new Random();
                    int indexr = rng.Next(ZenoTalk.answer_retainer.Length);
                    AnswerUser(userMessage, string.Format(ZenoTalk.answer_retainer[indexr], serveruser.Mention, NL));
                    //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_retainer[indexr], serveruser.Mention, NL));


                }
                else if (RegExFind(ZenoTalk.Maintenance, ZenoTalk.GameFF, tcont)) //maintenance
                {
                    Embed emb;
                    emb = await FFMaintTalk();
                    if (emb != null)
                    {
                        await userMessage.ReplyAsync("", embed: emb);
                    }
                    else
                    {
                        AnswerUser(userMessage, string.Format("Sorry {0} Something whent wrong D:", serveruser.Mention));
                        //await userMessage.ReplyAsync(string.Format("Sorry {0} Something whent wrong D:", serveruser.Mention));
                    }
                    

                }
                else if (RegExFind(ZenoTalk.Greetings, ZenoTalk.Greetings, tcont))// greetings **LETF THE LAST ONE**
                {
                    Random rng = new Random();
                    int indexr = rng.Next(ZenoTalk.answer_greetings.Length);
                    AnswerUser(userMessage, string.Format(ZenoTalk.answer_greetings[indexr], serveruser.Mention, NL));
                    //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_greetings[indexr], serveruser.Mention, NL));
                }


            }
#if DEBUG
#endif

            /*
            //lets react to bot mentions
            var mentioned = message.MentionedUsers.FirstOrDefault(u => u.Id == Bot_Zeno.CurrentUser.Id);
            if (mentioned != null)
            {
                if (mentioned.Mention == Bot_Zeno.CurrentUser.Mention)
                {
                    var emoj = new Emoji("🔥");
                    await userMessage.AddReactionAsync(emoj);
                }
            }
            */
            return;
        }

        async void AnswerUser(SocketUserMessage msg, string content)
        {
            await msg.Channel.TriggerTypingAsync();
            await Task.Delay(3000);
            await msg.ReplyAsync(content);
        }


        async void SendGuide(IDMChannel dm, int guide)
        {
            List<string> selguide = null;

            switch (guide)
            {
                case 0:
                    selguide = ClassRoom.Macros.Menu;
                    break;
                default:
                    break;
            }



            foreach (var item in selguide)
            {
                await dm.SendMessageAsync(item);
                await Task.Delay(1000);
            }
        }


        async Task<Embed> FFMaintTalk()
        {
            HttpClient client = new HttpClient();
            Embed embed = null;
            MaintenanceRoot data = null;
            string title = $"{Emote.Bot.LMaintenance} Current Maintenance";

            string jsonMaintenance = await client.GetStringAsync(XIVLN.APIs.MaintenanceCurrent);
            bool empty = true;

            if (!string.IsNullOrEmpty(jsonMaintenance))
            {
                data = JsonConvert.DeserializeObject<MaintenanceRoot>(jsonMaintenance);
                empty = (data == null || data.Game.Count == 0);
            }

            if (!empty)
            {
                foreach (var item in data.Game)
                {
                    string st = UnixTime(DateTime.Parse(item.Start));
                    string et = UnixTime(DateTime.Parse(item.End));
                    string tt = UnixTime(DateTime.Parse(item.Time));

                    embed = new EmbedBuilder()
                        .WithTitle(title)
                        .WithUrl(item.Url)
                        .AddField($"Start time: {NL + UnixTime(DateTime.Parse(item.Start), "d") + NL + UnixTime(DateTime.Parse(item.Start), "t")}", st, true)
                        .AddField($"End time: {NL + UnixTime(DateTime.Parse(item.End), "d") + NL + UnixTime(DateTime.Parse(item.End), "t")}", et, true)
                        .WithDescription("### " + item.Title + NL + NL + $"-# {tt}")
                        .WithThumbnailUrl(XIVLN.Config.FFLogo)
                        .WithColor(Color.Blue)
                        .WithFooter("From: Lodestone News")
                        .Build();
                }
            }
            else
            {
                embed = new EmbedBuilder()
                    .WithTitle("I got nothing... " + Emote.Bot.Disconnecting_party)
                    .WithDescription("Is game on maintenance??? " + Emote.Bot.Disconnecting_party)
                    .WithColor(Color.Red)
                    .Build();
            }

            return embed;

        }


        bool RegExFind(string[] constant, string[] variable, string text)
        {
            string constantchain = string.Join("|", constant); // Une las palabras fijas con "|"
            string variablechain = string.Join("|", variable); // Une las palabras opcionales con "|"
            string patron = $@"\b({constantchain})\b.*\b({variablechain})\b|\b({variablechain})\b.*\b({constantchain})\b";
            Regex regex = new Regex(patron, RegexOptions.IgnoreCase);
            return regex.IsMatch(text);
        }



    }
}


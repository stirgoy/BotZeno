using Discord;
using Discord.WebSocket;
using System;
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

            _ = Task.Run(() => WBL(message)); //word blacklist check

            if (Config.Channels.TalkChannel.Contains(message.Channel.Id.ToString()))
            {


                var userMessage = message as SocketUserMessage;
                var serveruser = Kuru.GetUser(userMessage.Author.Id);
                var channel = Kuru.GetTextChannel(userMessage.Channel.Id);
                if (message.Author.IsBot || userMessage == null) return;



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

                        string log = $"{message.Author.Username} used: {message.Content} on {Kuru.GetTextChannel(channel.Id).Name}";
                        Print(log);

                        CommandHandler(command, msg_splited, message);

                        return; // nothing more to do here
                    }
                }// if admin


                //if (!Check_Allowed_Channel(message.Channel)) { return; }

                if (message.Content.StartsWith("!hi") && userMessage.Channel.Id == Channel_hi) //AUTOROLE
                {
                    Autorole(serveruser, message);
                    return; // work done here
                }
                else if (userMessage.Channel.Id == Channel_hi) //:3 lets lock rules channel xDD jic
                {
                    if (!serveruser.GuildPermissions.Administrator)
                    {
                        await message.DeleteAsync();
                        string log = $"Message deleted on: {channel.Mention + NL} Reason: Not allowed.{NL}Autor: {message.Author.Mention + NL}Content:{NL + message.Content}";
                        Print(log);
                        await ZenoLog($"{Emote.Bot.Boss}Auto-mod{Emote.Bot.Boss}", log, message.Author.GetAvatarUrl());
                        return; // go home bb
                    }
                }

                if (channel.Id == Channel_zenos)
                {// allowed CHANNEL only
                    string tcont = message.Content;

                    if (RegExFind(ZenoTalk.Greetings, ZenoTalk.Help, tcont)) //help
                    {
                        Random rng = new Random();
                        int indexr = rng.Next(ZenoTalk.Answer_help.Length);
                        SimulateTyping(userMessage, string.Format(ZenoTalk.Answer_help[indexr], serveruser.Mention, NL));
                        //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_help[indexr], serveruser.Mention, NL));


                    }
                    else if (RegExFind(ZenoTalk.Macros, ZenoTalk.Help, tcont)) //macros
                    {
                        Random rng = new Random();
                        int indexr = rng.Next(ZenoTalk.Answer_macro.Length);
                        SimulateTyping(userMessage, string.Format(ZenoTalk.Answer_macro[indexr], serveruser.Mention, NL));
                        //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_macro[indexr], serveruser.Mention, NL));


                    }
                    else if (RegExFind(ZenoTalk.Retainers, ZenoTalk.Help, tcont))//retainers
                    {
                        Random rng = new Random();
                        int indexr = rng.Next(ZenoTalk.Answer_retainer.Length);
                        SimulateTyping(userMessage, string.Format(ZenoTalk.Answer_retainer[indexr], serveruser.Mention, NL));
                        //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_retainer[indexr], serveruser.Mention, NL));


                    }
                    else if (RegExFind(ZenoTalk.Macros, ZenoTalk.Menu, tcont)) //macros > menu
                    {

                        Random rng = new Random();
                        int indexr = rng.Next(ZenoTalk.Answer_menu.Length);
                        SimulateTyping(userMessage, string.Format(ZenoTalk.Answer_menu[indexr], serveruser.Mention, NL));
                        //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_menu[indexr], serveruser.Mention, NL));

                        IDMChannel dm = await userMessage.Author.CreateDMChannelAsync();
                        SendGuide(dm, 0);



                    }
                    else if (RegExFind(ZenoTalk.Macros, ZenoTalk.Useful, tcont)) //macros > useful
                    {

                        Random rng = new Random();
                        int indexr = rng.Next(ZenoTalk.Answer_useful.Length);
                        SimulateTyping(userMessage, string.Format(ZenoTalk.Answer_useful[indexr], serveruser.Mention, NL));
                        //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_menu[indexr], serveruser.Mention, NL));

                        IDMChannel dm = await userMessage.Author.CreateDMChannelAsync();
                        SendGuide(dm, 1);



                    }
                    else if (RegExFind(ZenoTalk.Macros, ZenoTalk.Hud, tcont)) //macros > hud
                    {

                        Random rng = new Random();
                        int indexr = rng.Next(ZenoTalk.Answer_hud.Length);
                        SimulateTyping(userMessage, string.Format(ZenoTalk.Answer_hud[indexr], serveruser.Mention, NL));
                        //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_menu[indexr], serveruser.Mention, NL));

                        IDMChannel dm = await userMessage.Author.CreateDMChannelAsync();
                        SendGuide(dm, 2);



                    }
                    else if (RegExFind(ZenoTalk.Retainers, ZenoTalk.Basic, tcont)) //macros > retainers basics
                    {

                        Random rng = new Random();
                        int indexr = rng.Next(ZenoTalk.Answer_retainerBasics.Length);
                        SimulateTyping(userMessage, string.Format(ZenoTalk.Answer_retainerBasics[indexr], serveruser.Mention, NL));
                        //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_menu[indexr], serveruser.Mention, NL));

                        IDMChannel dm = await userMessage.Author.CreateDMChannelAsync();
                        SendGuide(dm, 3);



                    }
                    else if (RegExFind(ZenoTalk.GameFF, ZenoTalk.Maintenance, tcont)) //maintenance
                    {
                        Embed emb;
                        emb = await FFMaintenanceTalk();
                        if (emb != null)
                        {
                            await userMessage.ReplyAsync("", embed: emb);
                        }
                        else
                        {
                            SimulateTyping(userMessage, string.Format(StringT.MsgRecived_err_0, serveruser.Mention));
                            //await userMessage.ReplyAsync(string.Format("Sorry {0} Something whent wrong D:", serveruser.Mention));
                        }


                    }               //**LETF THE PENULTIMATE**
                    else if (RegExFind(ZenoTalk.Greetings, ZenoTalk.Greetings, tcont))// greetings **LETF THE PENULTIMATE**
                    {
                        Random rng = new Random();
                        int indexr = rng.Next(ZenoTalk.Answer_greetings.Length);
                        SimulateTyping(userMessage, string.Format(ZenoTalk.Answer_greetings[indexr], serveruser.Mention, NL));
                        //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_greetings[indexr], serveruser.Mention, NL));
                    }
                    else            // YW **LETF THE LAST LAST**                   **LETF THE LAST LAST**
                    {
                        bool pass = false;
                        foreach (var item in ZenoTalk.Yw)
                        {
                            if (tcont.Contains(item)) { pass = true; break; }
                        }

                        if (pass)
                        {

                            Random rng = new Random();
                            int indexr = rng.Next(ZenoTalk.Answer_yw.Length);
                            SimulateTyping(userMessage, string.Format(ZenoTalk.Answer_yw[indexr], serveruser.Mention, NL));
                            //await userMessage.ReplyAsync(string.Format(ZenoTalk.answer_greetings[indexr], serveruser.Mention, NL));
                        }
                    }
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





    }
}


using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zeno
{
    internal partial class Program
    {
        private async static void CommandHandler(string command, string[] msg_splited, SocketMessage message)
        {
            var userMessage = message as SocketUserMessage;

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
                case "reconnect": //obv
                    await message.DeleteAsync();
                    Reconnect();
                    break;

                case "play": //sets custom status on bot info
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

                case "off": // shutdown bot
                    await message.DeleteAsync();
                    await message.Channel.SendMessageAsync($"Sayonara! {Emote.Bot.Disconnecting_party}");
                    await Bot_Zeno.StopAsync();
                    Environment.Exit(0);

                    break;

                case "BadWordsBlackList":
                    Config.BadWordsBlackList = !Config.BadWordsBlackList;
                    await Config_Save();
                    break;

#if DEBUG ///////////////////////////////////////////
                case "bwadd":
                    if (msg_splited.Count() < 3) break;

                    await AddBadWord(msg_splited[2]);
                    break;

                case "bwremove":
                    if (msg_splited.Count() < 3) break;

                    await RemoveBadWord(msg_splited[2]);
                    break;

                case "bwlist":

                    break;

                case "test":


                    break;
#endif //////////////////////////////////////////////
                default: //lets delete any failute !AppCmd-
                    BorrarMsg(userMessage, 0);

                    break;
            }
        }
    }
}

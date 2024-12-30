using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /********************
            ZenosLog
        *//////////////////// server log, requieres channel id on config
        private static async Task ZenoLog(string message)
        {
            if (Config.ZenoLog)
            {
                SocketTextChannel canal = Kuru.GetTextChannel(Config.Channels.LogChannel);
                if (canal != null)
                {
                    await canal.SendMessageAsync(message);
                }
            }
        }


        /********************
            borrar_msg
        *//////////////////// for delete bot messages over time
        private static void BorrarMsg(RestFollowupMessage botMessage, int tiempo = 8)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(tiempo * 1000);
                try { await botMessage.DeleteAsync(); } catch (Exception ex) { Print(ex.Message); }
            });
        }


        /********************
                MassDelete
        *//////////////////// be careful
        private static async void MassDelete(SocketMessage message, int howmany)
        {
            var channel = message.Channel;
            var mensajes = await channel.GetMessagesAsync(howmany).FlattenAsync();
            foreach (var item in mensajes)
            {
                await item.DeleteAsync();
                await Task.Delay(750);
            }

            string log = $"{message.Author.Mention} used mass delete {howmany} times on {Kuru.GetTextChannel(channel.Id).Mention}";
            Print(log);
            await ZenoLog(log);

        }


        /********************
            ResetApp
        *//////////////////// app reload
        private static void ResetApp()
        {
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(exePath);
            Environment.Exit(0);
        }

        /********************
            Reconnect
        *//////////////////// client reconnection
        private static async void Reconnect()
        {
            try
            {
                await Bot_Zeno.StopAsync();
                await Task.Delay(5000);
                await Bot_Zeno.StartAsync();
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

        /********************
            Reconnect
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

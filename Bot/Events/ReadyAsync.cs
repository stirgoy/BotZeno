using System;
using System.Linq;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /********************
                ReadyAsync
        *////////////////////

        private async Task ReadyAsync()
        {

            Kuru = Bot_Zeno.Guilds.First();

            if (Kuru == null)
            {
                Print("The bot is not connected to server.");
                return;
            }

            string tc = "";

            if (Config.Channels.TalkChannel != null)
            {
                if (Config.Channels.TalkChannel.Count >= 1)
                {
                    foreach (string item in Config.Channels.TalkChannel)
                    {
                        tc += $"{Kuru.GetChannel(ulong.Parse(item))}, ";
                    }
                    tc = tc.Remove(tc.Length - 2);
                    tc += ".";
                }
            }

            Skiplog = true;
            Print("Connected!");
            Print($"Name:               {Bot_Zeno.CurrentUser.Username}");
            Print($"Id:                 {Bot_Zeno.CurrentUser.Id}");
            Print($"Latency:            {Bot_Zeno.Latency}");
            Print($"Token:              {Bot_Zeno.TokenType}");
            Print($"Veryfed:            {Bot_Zeno.CurrentUser.IsVerified}");
            Print($"On:                 {Bot_Zeno.Guilds.Count} server/s");
            Print($"Online on:          {Kuru.Name}({Kuru.Id})");
            Print($"Talk channel/s:     {tc}");
            Print($"Log channel:        {Config.Channels.LogChannel} - {Kuru.GetChannel(Config.Channels.LogChannel)}");
            Skiplog = false;

            if (!string.IsNullOrEmpty(Config.Playing))
            {
                await Bot_Zeno.SetCustomStatusAsync(Config.Playing);
            }

            try
            {
                if (Config.UpdateSlashCommands)
                {
                    await Kuru.BulkOverwriteApplicationCommandAsync(ZenosSlashCommands.Zenos_SC);
                    Print("Application Commands registered successfully!");
                }
                else
                {
                    Print("ATENTION - BulkOverwriteApplicationCommandAsync skipped!");
                }
            }
            catch (Exception ex)
            {
                Print($"Error loading commands: {ex.Message}");
            }

            XIV_LN(); //news updater
            Cacpot(); //cacpot dm noticer
            //EventNoticer();

            try
            {
                CreateShortcut();
            }
            catch (Exception ex)
            {
                Print("ERROR creating shortcurt: " + ex.Message);
            }


        }
    }
}

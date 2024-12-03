using Discord;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Begu
{

    internal partial class Program
    {

        /********************
                MainAsync
        *////////////////////
        public async Task MainAsync()
        {
            //Environment.SetEnvironmentVariable("ZenosT", "TOKEN", EnvironmentVariableTarget.User);
#if DEBUG
            Print("<<<<< -------\\\\\\\\\\\\ DEBUG MODE //////------->>>>>");
#endif
            Print("<<<<< -------\\\\\\\\\\ Zeno♥ /////------->>>>>");
            Print("Logging in...");
            await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("ZenosT", EnvironmentVariableTarget.User));
            await _client.StartAsync();
            await Task.Delay(Timeout.Infinite);

        }

        /********************
                ReadyAsync
        *////////////////////

        private async Task ReadyAsync()
        {
            //var guild = _client.Guilds.ToList()[0];
            Kuru = _client.Guilds.First();
            if (Kuru == null)
            {
                Print("The bot is not connected to server.");
                return;
            }

            string tc = "";
            foreach (string item in Properties.Settings.Default.TalkChannel)
            {
                tc += $"{Kuru.GetChannel(ulong.Parse(item))}({Kuru.GetChannel(ulong.Parse(item)).Id}), ";
            }
            tc = tc.Remove(tc.Length - 2);
            tc += ".";

            Print("Connected!");

            Print($"Name:       {_client.CurrentUser.Username}");
            Print($"Id:         {_client.CurrentUser.Id}");
            Print($"Latency:    {_client.Latency}");
            Print($"Token:      {_client.TokenType}");
            Print($"Veryfed:    {_client.CurrentUser.IsVerified}");
            Print($"Online on:  {Kuru.Name}({Kuru.Id})");
            Print($"Talk channel/s:    {tc}");
            Print($"Log channel:    {Properties.Settings.Default.LogChannel} - {Kuru.GetChannel(Properties.Settings.Default.LogChannel)}");

            Print("Updating slash commands on server...");

            //_ = Task.Run(async () => { await ActualizarComandos(); });
            try
            {
                if (_updateSlashCommands)
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

            //news test
            //Properties.Settings.Default.update_last_id = "5978bd3462caa8e2f949327d8d13b54427af5808";
            //Properties.Settings.Default.Save();
            //Print(Properties.Settings.Default.news_last_id);

            Print("Loading news updater...");
            Check_FF_updates();
        }

        /********************
                LogAsync
        *////////////////////
        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
    }
}

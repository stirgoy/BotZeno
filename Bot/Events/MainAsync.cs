using Discord;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zeno
{

    internal partial class Program
    {

        /********************
                MainAsync
        *////////////////////
        public async Task MainAsync()
        {
            await Config_Load();

            //Environment.SetEnvironmentVariable("ZenosT", "TOKEN", EnvironmentVariableTarget.User);
            //Environment.SetEnvironmentVariable("WUZenosT", "TOKEN", EnvironmentVariableTarget.User);
#if DEBUG
            Print("<<<<< -------\\\\\\\\\\ Zeno♥ /////------->>>>>");
            Print("                                                (DEBUG)");
            await Bot_Zeno.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("ZenosT", EnvironmentVariableTarget.User));
            Config.RunBot = true;
            Config.XIV_LN_enabled = true;
            //StirgoyLN();
#else
            Print("<<<<< -------\\\\\\\\\\\\ Wind-Up Zeno♥ //////------->>>>>");
            await Bot_Zeno.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("WUZenosT", EnvironmentVariableTarget.User));

#endif

            if (Config.RunBot)
            {
                Print("Logging in...");
                await Bot_Zeno.StartAsync();

            }

            try
            {
                await Task.Delay(Timeout.Infinite, _cts.Token);
            }
            catch (TaskCanceledException)
            {
                await CleanupAsync();
            }

        }

        private static async Task CleanupAsync()
        {
            Print("Saving config before exit.");
            await Config_Save();
            Print("Saved.");
        }



    }
}

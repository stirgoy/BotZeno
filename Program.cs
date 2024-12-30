using Discord.WebSocket;
using System;
using System.Threading;

namespace Zeno
{

    internal partial class Program
    {
        //init
        public static void Main() => new Program().MainAsync().GetAwaiter().GetResult();
        //static async Task Main() { await App.MainAsync(); }
        private static readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public Program()
        {

            Bot_Zeno = new DiscordSocketClient(CordSocketConfig);

            Bot_Zeno.Log += LogAsync;
            Bot_Zeno.Ready += ReadyAsync;
            Bot_Zeno.MessageReceived += MessageReceivedAsync;
            Bot_Zeno.SlashCommandExecuted += SlashCommandHandlerAsync;
            Bot_Zeno.Disconnected += ClientDisconected;

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
        }

        private static void OnProcessExit(object sender, EventArgs e)
        {
            Print("Zeno is gone");
            _cts.Cancel(); // Asegurar que las tareas en ejecución se detengan
        }

    }

}
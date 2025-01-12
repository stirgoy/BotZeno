using Discord.Rest;
using Discord;
using Discord.WebSocket;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            //trying get close event
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            //bot events
            Bot_Zeno = new DiscordSocketClient(CordSocketConfig);
            //core
            Bot_Zeno.Log += LogAsync;
            Bot_Zeno.Ready += ReadyAsync;
            Bot_Zeno.Disconnected += ClientDisconected;
            Bot_Zeno.UserJoined += UserJoinedHandler;
            Bot_Zeno.UserLeft += UserLeftHandler;
            //msg
            Bot_Zeno.MessageReceived += MessageReceivedAsync;
            //slash commands
            Bot_Zeno.SlashCommandExecuted += SlashCommandHandlerAsync;
            //scheduled events
            Bot_Zeno.GuildScheduledEventUserAdd += EventUserAdd;
            Bot_Zeno.GuildScheduledEventUserRemove += EventUserRemove;
            Bot_Zeno.GuildScheduledEventCancelled += Kuru_EventCanceled;
            Bot_Zeno.GuildScheduledEventCompleted += Kuru_EventCompleted;
            Bot_Zeno.GuildScheduledEventCreated += Kuru_EventCreated;
            Bot_Zeno.GuildScheduledEventStarted += Kuru_EventStarted;
            
        }

        private static void OnProcessExit(object sender, EventArgs e)
        {
            Print("Zeno is gone");
            _cts.Cancel(); // Asegurar que las tareas en ejecución se detengan
        }

        


    }

}
using Discord.WebSocket;
using System;
using System.Threading;

namespace Zeno
{
    internal partial class Program
    {
        public static void Main() => new Program().MainAsync().GetAwaiter().GetResult();
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
            Bot_Zeno.UserJoined += Kuru_UserJoined;
            Bot_Zeno.UserLeft += Kuru_UserLeft;
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
    }
}

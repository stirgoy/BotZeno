using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Begu
{

    internal partial class Program
    {

        //commons
        static DiscordSocketClient _client;

        //server
        static SocketGuild Kuru = null; //Star Guardians!

        // DiscordSocketClient
        readonly DiscordSocketConfig DCFG = new DiscordSocketConfig
        {
            GatewayIntents =
                GatewayIntents.MessageContent |
                GatewayIntents.AllUnprivileged &
                ~GatewayIntents.GuildScheduledEvents &
                ~GatewayIntents.GuildInvites
        };

        

        //main
        static void Main() => new Program().MainAsync().GetAwaiter().GetResult();
        public Program()
        {

            _client = new DiscordSocketClient(DCFG);
            
            /********************
                Event Handlers
            *////////////////////
            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;
            _client.SlashCommandExecuted += SlashCommandHandlerAsync;

        }


    }

}
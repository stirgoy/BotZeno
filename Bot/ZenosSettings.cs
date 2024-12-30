using Discord;
using Discord.WebSocket;
using System;

namespace Zeno
{
    internal partial class Program
    {
        //Config Settings
        private static string Path { get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Zeno"; }
        private static string Xmlf { get => "kuru.json"; }
        //internal
        private static DiscordSocketClient Bot_Zeno;
        private static SocketGuild Kuru = null;
        private readonly DiscordSocketConfig CordSocketConfig = new DiscordSocketConfig
        {
            GatewayIntents =
                GatewayIntents.Guilds |
                GatewayIntents.GuildMembers |
                GatewayIntents.MessageContent |
                GatewayIntents.AllUnprivileged &
                ~GatewayIntents.GuildScheduledEvents &
                ~GatewayIntents.GuildInvites
        };
        //persistent config
        private static ConfigZeno Config = new ConfigZeno()
        {
            Ids = new NewsIds(),
            Channels = new KuruCFG()
        };


        //globals
        private static string NL => Environment.NewLine;
        private int Reconnections { get; set; } = 0;
        private static bool Skiplog { get; set; } = false;

    }
}

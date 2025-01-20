using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;

namespace Zeno
{
    internal partial class Program
    {
        //Config Settings
        private static string Path { get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Zeno"; }
        private static string Json_file { get => "kuru.json"; }
        private static string Words_file { get => "wbl.txt"; }
        private static string Warns_file { get => "warns.txt"; }

        //globals
        private static DiscordSocketClient Bot_Zeno;
        private static SocketGuild Kuru = null;
        private int Reconnections { get; set; } = 0;
        private static bool Skiplog { get; set; } = false; //handles with DEBUG
        private static string NL { get => Environment.NewLine; }
        private readonly DiscordSocketConfig CordSocketConfig = new DiscordSocketConfig
        {
            GatewayIntents = (
                GatewayIntents.Guilds |
                GatewayIntents.GuildMembers |
                GatewayIntents.MessageContent |
                GatewayIntents.AllUnprivileged |
                GatewayIntents.GuildScheduledEvents
            ) & ~GatewayIntents.GuildInvites
        };

#if DEBUG
        //autorole
        private readonly ulong Role_sproud = 1326212765928656977;
        private readonly ulong Role_normal = 1285702794078191616;
        private readonly ulong Channel_hi = 1326510690982170646; //welcome
        private readonly ulong Channel_greetings = 1310199196233760858; //test bot 
        private readonly ulong Channel_zenos = 1310199196233760858;

#else
        //autorole
        private readonly ulong Channel_zenos = 1315324417475219518;
        private readonly ulong Role_sproud = 1181272231477780576;
        private readonly ulong Role_normal = 1181272231477780577;
        //private readonly ulong Channel_hi = 1181272232442478665; //welcome
        private readonly ulong Channel_hi = 1181272232442478664; //rules
        private readonly ulong Channel_greetings = 1181272233260368004; // General chat

#endif
        //word blacklist
        private static List<string> WBL_List = new List<string>();

        //persistent config
        private static ConfigZeno Config = new ConfigZeno()
        {
            Ids = new NewsIds(),
            Channels = new KuruCFG()
        };



    }
}

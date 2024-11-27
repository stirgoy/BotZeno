using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Begu
{

    internal class Program
    {

        public readonly DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        static void Main(string[] args)
            => new Program().
            MainAsync().
            GetAwaiter().
            GetResult();


        public Program()
        {

            
            /***********************************
                Config para DiscordSocketClient
            *///////////////////////////////////
            var config = new DiscordSocketConfig
            {
                GatewayIntents =
                GatewayIntents.MessageContent |
                GatewayIntents.AllUnprivileged &
                ~GatewayIntents.GuildScheduledEvents & //removed
                ~GatewayIntents.GuildInvites           //removed
            };

            // It is recommended to Dispose of a client when you are finished
            // using it, at the end of your app's lifetime.
            _client = new DiscordSocketClient(config);
            _commands = new CommandService();
            /********************
                Event Handlers
            *////////////////////
            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;
            _client.SlashCommandExecuted += SlashCommandHandlerAsync;
        }

        /********************
                MainAsync
        *////////////////////
        public async Task MainAsync()
        {
            // Token == Private data, some ways to not hard code: put inside external file and read it
            
            print("<<<<< -------\\\\\\\\\\ Zeno♥ /////------->>>>>");
            print("Logging in...");
            await _client.LoginAsync(TokenType.Bot, Properties.Settings.Default.token);
            await _client.StartAsync();
            //await RegisterCommandsAsync();
            // Block the program until it is closed.
            await Task.Delay(Timeout.Infinite);

        }

        /********************
                LogAsync
        *////////////////////
        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }


        /********************
                ReadyAsync
        *////////////////////
        // The Ready event indicates that the client has opened a
        // connection and it is now safe to access the cache.
        private async Task ReadyAsync()
        {
            print("Connected!");
            var guild = _client.Guilds.ToList()[0];

            print("Loading slash commands...");
            _ = Task.Run(async () => { await ActualizarComandos(); });


            print($"Name:       {_client.CurrentUser.Username}");
            print($"Id:         {_client.CurrentUser.Id}");
            print($"Token:      {_client.TokenType}");
            print($"Veryfed:    {_client.CurrentUser.IsVerified}");
            print($"Online on:  {_client.Guilds.Count} servers");
            print($"Latency:    {_client.Latency}");
            print($"Talk channel:    {Properties.Settings.Default.TalkChannel}");




#if DEBUG
            print("DEBUG MODE!!!!!!!!!!!!!!!!!\"");
#endif
            print("I'm ready!\"");

        }

        private async Task ActualizarComandos()
        {
            if (!_client.Guilds.Any())
            {
                print("The bot is not connected to any guilds.");
                return;
            }


            var guild = _client.Guilds.First();

            var comandos = new ApplicationCommandProperties[]
            {
        new SlashCommandBuilder()
            .WithName("admin")
            .WithDescription("Shows admin commands!")
            .Build(),

        new SlashCommandBuilder()
            .WithName("timers")
            .WithDescription("Shows Eorzean timers.")
            .Build(),
        new SlashCommandBuilder()
            .WithName("listc")
            .WithDescription("Shows allowed answer channels.")
            .Build(),
        /*
        */
        new SlashCommandBuilder()
            .WithName("talkc")
            .WithDescription("Edits my answer channels, if channel is added i going remove ^^")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("channel")
                .WithDescription("Choose a text channel")
                .WithType(ApplicationCommandOptionType.Channel)
                .AddChannelType(ChannelType.Text)
                .WithRequired(true))
            .Build()
            };

            try
            {
                await guild.BulkOverwriteApplicationCommandAsync(comandos);
                print("Commands registered successfully!");
            }
            catch (Exception ex)
            {
                print($"Error loading commands: {ex.Message}");
            }
        }

        /////////////////////////////////////////
        /////////////    Command Handler
        /////////////////////////////////////////
        private async Task SlashCommandHandlerAsync(SocketSlashCommand command)
        {


            switch (command.CommandName)
            {
                //////////////////////////    Timers
                case "timers":

                    if (!Check_Allowed_Channel(command.Channel)) { goto default; } // only talk channel

                    int weekly_hour = 9;
                    int daily_hour = 16;


                    //weekly calculator                                               Restart hour
                    DateTime ahora = DateTime.Now;
                    DateTime weekly = new DateTime(ahora.Year, ahora.Month, ahora.Day, weekly_hour, 0, 0);

                    if (ahora.IsDaylightSavingTime())
                    {
                        weekly_hour = 10;
                        daily_hour = 17;
                    }

                    while (true)
                    {                           //restart day of week
                        if (weekly.DayOfWeek == DayOfWeek.Tuesday)
                        {
                            break;
                        }
                        else
                        {
                            weekly = weekly.AddDays(1); //setting next tuesday
                        }
                    }

                    if (ahora.Hour >= weekly_hour && ahora.Day == weekly.Day) { weekly = weekly.AddDays(7); }

                    long unixW = new DateTimeOffset(weekly).ToUnixTimeSeconds();
                    string weeklyDT = $"<t:{unixW}:R>"; //SIEMPRE RELATIVA

                    //daily calculator                                               Restart hour
                    DateTime daily = new DateTime(ahora.Year, ahora.Month, ahora.Day, daily_hour, 00, 00);

                    if (ahora.Hour >= daily_hour)
                    {
                        daily = daily.AddDays(1);// setting next day
                    }

                    long unixD = new DateTimeOffset(daily).ToUnixTimeSeconds();
                    string dailyDT = $"<t:{unixD}:R>";


                    //GC calculator                                               Restart hour
                    DateTime gc = new DateTime(ahora.Year, ahora.Month, ahora.Day, 21, 00, 00);

                    if (ahora.Hour >= 21)
                    {
                        gc = gc.AddDays(1);// setting next day
                    }

                    long unixG = new DateTimeOffset(gc).ToUnixTimeSeconds();
                    string gcDT = $"<t:{unixG}:R>";


                    //ocean fishing
                    DateTime ocean = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, 00, 00);

                    if (ahora.Hour % 2 == 0)
                    {
                        ocean = ocean.AddHours(2);
                    }
                    else
                    {
                        ocean = ocean.AddHours(1);
                    }

                    long unixO = new DateTimeOffset(ocean).ToUnixTimeSeconds();
                    string oceanDT = $"<t:{unixO}:R>";

                    //cacpot lotery
                    DateTime cacpot = new DateTime(ahora.Year, ahora.Month, ahora.Day, 20, 0, 0);
                    while (true)
                    {                           //lottery day
                        if (cacpot.DayOfWeek == DayOfWeek.Saturday)
                        {
                            break;
                        }
                        else
                        {
                            cacpot = cacpot.AddDays(1); //setting next saturday
                        }
                    }
                    long unixC = new DateTimeOffset(cacpot).ToUnixTimeSeconds();
                    string cacpotDT = $"<t:{unixC}:R>"; //SIEMPRE RELATIVA


                    var embed = new EmbedBuilder()
                        .WithTitle($"Eorzean timers.")
                        .WithDescription("Because you looks lost.")
                        .WithColor(Color.Green)
                        .AddField(":calendar_spiral: Weekly", weeklyDT, true)
                        .AddField(":clock4: Daily", dailyDT, true)
                        .AddField(":clock7: GC", gcDT, true)
                        .AddField(":fish: Ocean fishing", oceanDT, true)
                        .AddField("<:cactuar:1311094440215056445> Cacpot", cacpotDT, true)
                        .WithFooter("Take care.")
                        .WithTimestamp(DateTimeOffset.Now)
                        .Build();
                    await command.RespondAsync("", embed: embed);

                    break;

                //////////////////////////    admin
                case "admin":

                    var socketUser = command.User as SocketGuildUser;
                    //bool isAdmin = socketUser?.GuildPermissions.Administrator ?? false;

                    if (!socketUser.GuildPermissions.Administrator) { goto default; }
                    if (!Check_Allowed_Channel(command.Channel)) { goto default; } // only talk channel


                    var admin_emb = new EmbedBuilder()
                    .WithTitle($"Admin commands.")
                    .WithDescription("Because you looks lost.")
                    .WithColor(Color.Orange)
                    .AddField("Edit my answer channel", "!talkc", false)
                    .AddField("Show allowed channels", "!listc", false)
                    .WithFooter("Take care.")
                    .WithTimestamp(DateTimeOffset.Now)
                    .Build();
                    await command.RespondAsync("", embed: admin_emb, ephemeral: true);


                    break;

                //////////////////////////    talkc
                case "talkc":

                    var socketUsert = command.User as SocketGuildUser;
                    //bool isAdmint = socketUsert?.GuildPermissions.Administrator ?? false;

                    if (!socketUsert.GuildPermissions.Administrator) { goto default; }
                    //if (!Check_Allowed_Channel(command.Channel)) { goto default; } // only talk channel

                    var channel = command.Data.Options.FirstOrDefault(opt => opt.Name == "channel");

                    if (channel?.Value is SocketChannel selectedChannel) { } else { return; }//exit on fail

                    //EXISTE
                    if (Check_Allowed_Channel(selectedChannel))
                    {

                        RemoveTalkChannel(selectedChannel.Id.ToString());
                       //TODO REPARALO 
                        //msg 
                        var talkc_embD = new EmbedBuilder()
                        .WithTitle("Settings")
                        .WithDescription("So now i will ignore " + selectedChannel.ToString() + " <:bossicon:1311094313714974812>")
                        .WithColor(Color.Green)
                        //.AddField("Text Channel:", selectedChannel.ToString())
                        .WithTimestamp(DateTimeOffset.Now)
                        .Build();
                        await command.RespondAsync("", embed: talkc_embD, ephemeral: true);
                        return;
                    }

                    //add
                    AddTalkChannel(selectedChannel.Id.ToString());
                    //TODO REPARALO 


                    Console.WriteLine("Channel set as talk channel: " + selectedChannel.ToString() + " - " + selectedChannel.Id.ToString());

                    var talkc_emb = new EmbedBuilder()
                        .WithTitle("Settings")
                        .WithDescription($"Now i going answer on: " + selectedChannel.ToString())
                        .WithColor(Color.Green)
                        //.AddField("Text Channel:", selectedChannel.ToString())
                        .WithTimestamp(DateTimeOffset.Now)
                        .Build();
                    await command.RespondAsync("", embed: talkc_emb, ephemeral: true);

                    break;


                case "listc":

                    var socketUserc = command.User as SocketGuildUser;
                    //bool isAdmin = socketUser?.GuildPermissions.Administrator ?? false;

                    if (!socketUserc.GuildPermissions.Administrator) { goto default; }
                    //if (!Check_Allowed_Channel(command.Channel)) { goto default; } // only talk channel
                    string cnls = "";

                    if(Properties.Settings.Default.TalkChannel == null ) 
                    {                        

                        Properties.Settings.Default.TalkChannel = new StringCollection();
                        cnls = "You need add atleast one channel, keep a look on /talkc";
                    }
                    else if (Properties.Settings.Default.TalkChannel.Count == 0)
                    {
                        cnls = "You need add atleast one channel, keep a look on /talkc";
                    }
                    else
                    {
                        foreach (string item in Properties.Settings.Default.TalkChannel)
                        {
                            string nme = _client.GetChannel(ulong.Parse(item)).ToString();
                            cnls += nme;
                            if (item == Properties.Settings.Default.TalkChannel[Properties.Settings.Default.TalkChannel.Count - 1])
                            {
                                cnls+= ".";
                            }
                            else
                            {
                                cnls += ", ";
                            }
                        }
                    }
                    
                    
                    var admin_embc = new EmbedBuilder()
                    .WithTitle($"Admin commands.")
                    .WithDescription("Because you looks lost.")
                    .WithColor(Color.Orange)
                    .AddField("Current channel/s:", cnls, false)
                    .WithFooter("Take care.")
                    .WithTimestamp(DateTimeOffset.Now)
                    .Build();
                    await command.RespondAsync("", embed: admin_embc, ephemeral: true);
                    

                    break;

                //////////////////////////    Default
                default:
                    var def_emb = new EmbedBuilder()
                        .WithTitle($"Zeno♥.")
                        .WithDescription("Because you looks lost. <:disconnecting:1311089532527054909>")
                        .WithColor(Color.Red)
                        .AddField("Wrong command", "/NotForYou", false)
                        .AddField("You missing permissions.", "/YouNotAdmin", false)
                        .AddField("I shouldn't talk here", "/NotForMe", false)
                        .Build();
                    await command.RespondAsync("", embed: def_emb, ephemeral: true);
                    break;
            }
        }



        private async Task MessageReceivedAsync(SocketMessage message)
        {
            var userMessage = message as SocketUserMessage;
            if (message.Author.IsBot || userMessage == null) return;
            if (!Check_Allowed_Channel(message.Channel)) { return; }

            await userMessage.Channel.TriggerTypingAsync();

            string answer = "";
            bool sayhi = false;
            var mentionedUser = message.MentionedUsers.FirstOrDefault(u => u.Id == _client.CurrentUser.Id);

            if (userMessage.Content.ToString().ToLower().IndexOf("hi", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var emoj = new Emoji("❤️");
                await message.AddReactionAsync(emoj);
                answer = $"¡Hi, {message.Author.Mention}♥! 😊";
                sayhi = true;
                //await userMessage.Channel.SendMessageAsync($"¡Hi, {message.Author.GlobalName}♥! 😊");
            }

            if(mentionedUser != null)
            {
                //var emoj = new Emoji(":upside_down:");                
                
                //await message.AddReactionAsync(emoj);
                answer = $"Oh a mention!!!, {message.Author.Mention}♥! wanna be my friend 😊? or my enemy :smiling_imp: ?";

                if (sayhi)
                {
                    answer = "https://tenor.com/view/zenos-zenos-yae-galvus-ffxiv-final-fantasy14-final-fantasy-gif-25294169";
                }
            }
            var reference = new MessageReference(message.Id);

            // Responder directamente al mensaje original
            await message.Channel.SendMessageAsync(answer, false, null, null, null, reference);
            //await userMessage.Channel.SendMessageAsync(answer);

            return;
        }

        private bool Check_Allowed_Channel(ISocketMessageChannel channel_to_check)
        {

            StringCollection channels = Properties.Settings.Default.TalkChannel;
            if (channels == null) return false;
            foreach (var item in channels)
            {
                if (channel_to_check.Id == ulong.Parse(item))
                {
                    return true;
                }
            }

            return false;
        }

        private bool Check_Allowed_Channel(SocketChannel channel_to_check)
        {

            StringCollection channels = Properties.Settings.Default.TalkChannel;
            if (channels == null) return false;
            foreach (var item in channels)
            {
                if (channel_to_check.Id == ulong.Parse(item))
                {
                    return true;
                }
            }

            return false;
        }

        /********************
              Time Stamp
        *////////////////////
        string tik()
        {
            //vars
            string rt = "", h = "", m = "", s = "";
            DateTime n = DateTime.Now;
            //0 on left
            if (n.Hour < 10) { h = "0"; }
            if (n.Minute < 10) { m = "0"; }
            if (n.Second < 10) { s = "0"; }
            //getting values
            h += n.Hour.ToString();
            m += n.Minute.ToString();
            s += n.Second.ToString();
            //making timestamp
            rt = h + ":" + m + ":" + s + " "; //format 00:00:00

            return rt;
        }

        private async void print(string line, bool showname = true)
        {
            string time_stamp = "", h = "", m = "", s = "";
            DateTime n = DateTime.Now;
            //0 on left
            if (n.Hour < 10) { h = "0"; }
            if (n.Minute < 10) { m = "0"; }
            if (n.Second < 10) { s = "0"; }
            //getting values
            h += n.Hour.ToString();
            m += n.Minute.ToString();
            s += n.Second.ToString();
            //making timestamp
            time_stamp = h + ":" + m + ":" + s + " "; //format 00:00:00

            if (showname) { line = "Zeno♥ - " + line; }
            Console.WriteLine(time_stamp + " " + line);
        }

        //relative timestamps
        private string CordTime(DateTime date)
        {
            long unixTimestamp = new DateTimeOffset(date).ToUnixTimeSeconds();
            string r = $"<t:{unixTimestamp}:R>";
            return r;
        }

        private void AddTalkChannel(String channel)
        {
            StringCollection channels = Properties.Settings.Default.TalkChannel;
            if (channels == null) channels = new StringCollection();
            channels.Add(channel);
            Properties.Settings.Default.TalkChannel = channels;
            Properties.Settings.Default.Save();
        }

        private void RemoveTalkChannel(String channel)
        {
            StringCollection channels = Properties.Settings.Default.TalkChannel;
            StringCollection new_channels = new StringCollection();
            foreach (var item in channels)
            {
                if (item != channel)
                {
                    new_channels.Add(item);
                }
            }

            Properties.Settings.Default.TalkChannel = new_channels;
            Properties.Settings.Default.Save();
        }
        /*
        */
    }
}


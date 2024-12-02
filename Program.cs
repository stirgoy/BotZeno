using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace Begu
{

    internal class Program
    {

        public readonly DiscordSocketClient _client;
        private CommandService _commands = new CommandService();
        //private IServiceProvider _services;

        static void Main(string[] args) =>
            new Program()
            .MainAsync()
            .GetAwaiter()
            .GetResult();


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
                ~GatewayIntents.GuildScheduledEvents &
                ~GatewayIntents.GuildInvites
            };

            _client = new DiscordSocketClient(config);
            _commands = new CommandService();

            /********************
                Event Handlers
            *////////////////////
            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;
            _client.SlashCommandExecuted += SlashCommandHandlerAsync;
            /*_client.Disconnected += async (exception) =>
            {
                if (exception is GatewayReconnectException)
                {
                    Console.WriteLine("Server wants restart");
                    await ReiniciarBotAsync();
                }
                else
                {
                    Console.WriteLine($"Client.Disconnected: {exception?.Message}");
                }
            };
            */
        }

        /********************
                ReiniciarBotAsync
        *////////////////////
        /*private async Task ReiniciarBotAsync()
        {
            try
            {
                await _client.LogoutAsync();
                await _client.StopAsync();
                Console.WriteLine("Bot go sleep :D  ZZzz.._");

                
                await Task.Delay(5000);

                
                await _client.LoginAsync(TokenType.Bot, Properties.Settings.Default.token);
                await _client.StartAsync();
                Console.WriteLine("Restarting bot...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }*/

        /********************
                MainAsync
        *////////////////////
        public async Task MainAsync()
        {
            // Token == Private data, some ways to not hard code: put inside external file and read it
#if DEBUG
            Print("<<<<< -------\\\\\\\\\\\\ DEBUG MODE //////------->>>>>");
#endif
            Print("<<<<< -------\\\\\\\\\\ Zeno♥ /////------->>>>>");
            Print("Logging in...");
            await _client.LoginAsync(TokenType.Bot, Properties.Settings.Default.token);
            await _client.StartAsync();
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

        private async Task ReadyAsync()
        {
            var guild = _client.Guilds.ToList()[0];
            string tc = "";
            foreach (string item in Properties.Settings.Default.TalkChannel)
            {
                tc += $"{guild.GetChannel(ulong.Parse(item))}({guild.GetChannel(ulong.Parse(item)).Id}), ";
            }
            tc = tc.Remove(tc.Length - 2);
            tc += ".";

            Print("Connected!");

            Print($"Name:       {_client.CurrentUser.Username}");
            Print($"Id:         {_client.CurrentUser.Id}");
            Print($"Latency:    {_client.Latency}");
            Print($"Token:      {_client.TokenType}");
            Print($"Veryfed:    {_client.CurrentUser.IsVerified}");
            Print($"Online on:  {_client.Guilds.First().Name}({_client.Guilds.First().Id})");
            Print($"Talk channel/s:    {tc}");
            Print($"Log channel:    {Properties.Settings.Default.LogChannel} - {guild.GetChannel(Properties.Settings.Default.LogChannel)}");

            Print("Loading slash commands on new thread...");

            //_ = Task.Run(async () => { await ActualizarComandos(); });
            await ActualizarComandos();
            //news test
            //Properties.Settings.Default.update_last_id = "5978bd3462caa8e2f949327d8d13b54427af5808";
            //Properties.Settings.Default.Save();
            //Print(Properties.Settings.Default.news_last_id);
            Print("Loading news updater...");
            Check_FF_updates();
            //return Task.CompletedTask;

        }


        /********************
          ActualizarComandos
        *////////////////////
        private async Task ActualizarComandos()
        {
            if (!_client.Guilds.Any())
            {
                Print("The bot is not connected to any guilds.");
                return;
            }

            var guild = _client.Guilds.First();

            try
            {

                var comandos = new ApplicationCommandProperties[] {

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

        new SlashCommandBuilder()
        .WithName("userinfo")
        .WithDescription("I bring server information from a specific user.")
        .AddOption(new SlashCommandOptionBuilder()
            .WithName("name")
            .WithDescription("User name.")
            .WithType(ApplicationCommandOptionType.User)
            .WithRequired(true))
        .Build(),

        new SlashCommandBuilder()
            .WithName("ffnews")
            .WithDescription("Shows Lodestone news.")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("number")
                .WithDescription("How many news you want see?")
                .WithType(ApplicationCommandOptionType.Integer)
                .WithRequired(false)
                )
            .Build(),

                new SlashCommandBuilder()
            .WithName("ffstatus")
            .WithDescription("Shows Lodestone news.")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("number")
                .WithDescription("How many news you want see?")
                .WithType(ApplicationCommandOptionType.Integer)
                .WithRequired(false)
                )
            .Build(),
                /*
                */
            new SlashCommandBuilder()
            .WithName("ffmaintenance")
            .WithDescription("Shows Lodestone maintenance status.")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("number")
                .WithDescription("How many news you want see?")
                .WithType(ApplicationCommandOptionType.Integer)
                .WithRequired(false)
                )
            .Build(),
            new SlashCommandBuilder()
            .WithName("ffupdates")
            .WithDescription("Shows Lodestone news.")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("number")
                .WithDescription("How many news you want see?")
                .WithType(ApplicationCommandOptionType.Integer)
                .WithRequired(false)
                )
            .Build(),


        new SlashCommandBuilder()
            .WithName("talkc")
            .WithDescription("Edits my answer channels, if channel is added i going remove ^^")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("channel")
                .WithDescription("Choose a text channel")
                .WithType(ApplicationCommandOptionType.Channel)
                .AddChannelType(ChannelType.Text)
                .WithRequired(true))
            .Build(),

        new SlashCommandBuilder()
            .WithName("logc")
            .WithDescription("Edits my log channel, only can be one channel")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("channell")
                .WithDescription("Choose a text channel")
                .WithType(ApplicationCommandOptionType.Channel)
                .AddChannelType(ChannelType.Text)
                .WithRequired(true))
            .Build(),

        new SlashCommandBuilder()
            .WithName("newsc")
            .WithDescription("Edits my ff news channel, only can be one channel")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("newsc")
                .WithDescription("Choose a text channel")
                .WithType(ApplicationCommandOptionType.Channel)
                .AddChannelType(ChannelType.Text)
                .WithRequired(true))
            .Build(),

        new SlashCommandBuilder()
            .WithName("updatec")
            .WithDescription("Edits my ff updates channel, only can be one channel")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("updatec")
                .WithDescription("Choose a text channel")
                .WithType(ApplicationCommandOptionType.Channel)
                .AddChannelType(ChannelType.Text)
                .WithRequired(true))
            .Build(),

        new SlashCommandBuilder()
            .WithName("statusc")
            .WithDescription("Edits my ff status channel, only can be one channel")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("statusc")
                .WithDescription("Choose a text channel")
                .WithType(ApplicationCommandOptionType.Channel)
                .AddChannelType(ChannelType.Text)
                .WithRequired(true))
            .Build(),

        new SlashCommandBuilder()
            .WithName("maintenancec")
            .WithDescription("Edits my ff mainenance channel, only can be one channel")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("maintenancec")
                .WithDescription("Choose a text channel")
                .WithType(ApplicationCommandOptionType.Channel)
                .AddChannelType(ChannelType.Text)
                .WithRequired(true))
            .Build(),

        new SlashCommandBuilder()
            .WithName("timec")
            .WithDescription("I bring you a dynamic display of date time and the code ♥")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("time")
                .WithDescription("Text a date time. example: YYYY-MM-DD HH:mm:ss, can be only the hour.")
                .WithType(ApplicationCommandOptionType.String)
                .WithRequired(true))
            .Build()

            };

                if (false)
                {
                    await guild.BulkOverwriteApplicationCommandAsync(comandos);
                    Print("Application Commands registered successfully!");
                }
                else
                {
                    Print("ATENTION - BulkOverwriteApplicationCommandAsync skipped!!!!!!!!!!!!!!");
                }

            }
            catch (Exception ex)
            {
                Print($"Error loading commands: {ex.Message}");
            }

        }




        /********************
             SLASH COMMANDS
        *////////////////////

        //              /admin
        private async Task Command_admin(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: true);

            var admin_emb = new EmbedBuilder()
            .WithTitle($"Admin commands.")
            .WithDescription("Because you looks lost.")
            .WithColor(Color.Orange)
            .AddField("Edit my answer channel", "/talkc", false)
            .AddField("Show allowed channels", "/listc", false)
            .AddField("Edit log channel", "/logc", false)
            .AddField("Shows server info from user", "/useri", false)
            .WithFooter("Take care.")
            .WithTimestamp(DateTimeOffset.Now)
            .Build();
            var sm = await command.FollowupAsync("", embed: admin_emb, ephemeral: true);
            borrar_msg(sm, 20);
        }


        //              /news
        private async Task Command_news(SocketSlashCommand command, string mode)
        {
            await command.DeferAsync(ephemeral: false);

            try
            {
                int def = 1; int min = 1; int max = 5;

                var cantidadOption = command.Data.Options.FirstOrDefault(opt => opt.Name == "number");
                int cantidad = cantidadOption?.Value is long value ? (int)value : def;
                if (cantidad > max) { cantidad = max; }
                if (cantidad <= 0) { cantidad = min; }

                //maintenance >> need struct
                //news
                //status
                //updates
                List<Embed> news = await LodestoneHandler(cantidad, command, mode);

                await command.FollowupAsync("", embeds: news.ToArray(), ephemeral: false);

            }
            catch (Exception ex)
            {
                Print($"Error: {ex.Message}");
            }
        }


        //              /userinfo
        private async Task Command_userinfo(SocketSlashCommand command, IUser user_name)
        {
            await command.DeferAsync(ephemeral: true);

            SocketGuildUser sgu = null;
            //              lulu                str
            //print(user_name.GlobalName + " " + user_name.Username);
            var serv_users = _client.Guilds.First().Users;
            foreach (var item in serv_users)
            {
                if (item.Id == user_name.Id) { sgu = item; break; }
            }

            string roles = "Error getting roles.";

            if (sgu != null)
            {
                roles = "";

                foreach (var item in sgu.Roles)
                {
                    if (item.Name == sgu.Roles.Last().Name)
                    {
                        roles += item + ".";
                    }
                    else
                    {
                        roles += item + ", ";
                    }
                }

            }
            var roleList = string.Join(", ", sgu.Roles.Where(x => !x.IsEveryone).Select(x => x.Mention));
            roleList.Remove(roleList.Length - 2);
            roleList += ".";

            string nik = sgu.Nickname;
            if (nik == null) { nik = "none"; }
            string avatarUrl = sgu.GetAvatarUrl(ImageFormat.Auto, 512);
            // 🟢 green
            //🔴 red
            string admin = (sgu.GuildPermissions.Administrator) ? "🟢" : "🔴";

            var user_emb = new EmbedBuilder()
                .WithTitle($"User information")
                .AddField("Display name", $"{sgu.DisplayName}", true)
                .AddField("Discord name", $"{sgu.Username}", true)
                .AddField("Global name", $"{sgu.GlobalName}", true)
                .AddField("Server name", $"{nik}", true)
                .AddField("Is admin", admin, true)
                .AddField("Roles", $"{roleList}")
                .WithThumbnailUrl(avatarUrl)
                .WithFooter($" My enemy.")
                .WithColor(Color.Orange)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();

            var m = await command.FollowupAsync("", embed: user_emb, ephemeral: true);
            borrar_msg(m, 60);
        }


        //              /timec
        private async Task Command_timec(SocketSlashCommand command, string user_msg)
        {
            await command.DeferAsync(ephemeral: true);

            if (string.IsNullOrEmpty(user_msg))
            {
                await command.RespondAsync("Please provide a valid date time my friend in the format YYYY-MM-DD HH:mm:ss.", ephemeral: true);
                return;
            }

            if (!DateTime.TryParse(user_msg, out DateTime parsedDate))
            {
                await command.RespondAsync("Invalid date format my friend. Use YYYY-MM-DD HH:mm:ss.", ephemeral: true);
                return;
            }

            //parsedDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc); //UTC ????? LMAO xD

            long unixTimestamp = new DateTimeOffset(parsedDate).ToUnixTimeSeconds();
            string discordTimestamp = $"<t:{unixTimestamp}:R>";

            var talkc_embT = new EmbedBuilder()
                .WithTitle("Timestamp")
                .WithDescription($"Here's your Discord timestamp my enemy:")
                .AddField(discordTimestamp, $"`{discordTimestamp}`")
                .WithFooter($" My friend.")
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            await command.FollowupAsync("", embed: talkc_embT, ephemeral: true);

        }


        //              /timers
        private async Task Command_timers(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: false);

            DateTime ahora = DateTime.Now;
            bool isDST = ahora.IsDaylightSavingTime();

            //dst handler
            int weekly_hour = isDST ? 10 : 9;
            int daily_hour = isDST ? 17 : 16;
            int GC_hour = isDST ? 22 : 21;

            // Weekly timer
            DateTime weekly = new DateTime(ahora.Year, ahora.Month, ahora.Day, weekly_hour, 0, 0)
                .AddDays(((int)DayOfWeek.Tuesday - (int)ahora.DayOfWeek + 7) % 7);
            if (ahora >= weekly) weekly = weekly.AddDays(7); //move to next tuesday if passed
            long unixW = new DateTimeOffset(weekly).ToUnixTimeSeconds();
            string weeklyDT = $"<t:{unixW}:R>";

            // Daily timer
            DateTime daily = new DateTime(ahora.Year, ahora.Month, ahora.Day, daily_hour, 0, 0);
            if (ahora >= daily) daily = daily.AddDays(1); //move to next day if passed
            long unixD = new DateTimeOffset(daily).ToUnixTimeSeconds();
            string dailyDT = $"<t:{unixD}:R>";

            // GC timer
            DateTime gc = new DateTime(ahora.Year, ahora.Month, ahora.Day, GC_hour, 0, 0);
            if (ahora >= gc) gc = gc.AddDays(1); //move to next day if passed
            long unixG = new DateTimeOffset(gc).ToUnixTimeSeconds();
            string gcDT = $"<t:{unixG}:R>";

            // Ocean Fishing timer
            DateTime ocean = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, 0, 0)
                .AddHours(2 - (ahora.Hour % 2));
            long unixO = new DateTimeOffset(ocean).ToUnixTimeSeconds();
            string oceanDT = $"<t:{unixO}:R>";

            // Cacpot Lottery timer
            DateTime cacpot = new DateTime(ahora.Year, ahora.Month, ahora.Day, 20, 0, 0)
                .AddDays(((int)DayOfWeek.Saturday - (int)ahora.DayOfWeek + 7) % 7);
            long unixC = new DateTimeOffset(cacpot).ToUnixTimeSeconds();
            string cacpotDT = $"<t:{unixC}:R>";

            // Embed response
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

            await command.FollowupAsync("", embed: embed);

        }


        //              /talkc
        private async Task Command_talkc(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            //EXISTE
            if (Check_Allowed_Channel(selectedChannel))
            {

                RemoveTalkChannel(selectedChannel.Id.ToString());
                //msg 
                var talkc_embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription("So now i will ignore " + selectedChannel.ToString() + " <:bossicon:1311094313714974812>")
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: talkc_embD, ephemeral: true);
                borrar_msg(m);
                await ZenosLog($"{command.User.Mention} removes {selectedChannel.ToString()} as talk channel.");
                return;
            }

            AddTalkChannel(selectedChannel.Id.ToString());

            Print("Channel set as talk channel: " + selectedChannel.ToString() + " - " + selectedChannel.Id.ToString());

            var talkc_emb = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Now i going answer on: " + selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            await command.FollowupAsync("", embed: talkc_emb, ephemeral: true);
            await ZenosLog($"{command.User.Mention} sets {selectedChannel.ToString()} as talk channel.");

        }


        //              /listc
        private async Task Command_listc(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: true);

            string cnls = "";

            if (Properties.Settings.Default.TalkChannel == null)
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
                    //string nme = _client.GetChannel(ulong.Parse(item)).ToString();
                    var t = _client.Guilds.First().GetTextChannel(ulong.Parse(item));
                    cnls += t.Mention;

                    if (item == Properties.Settings.Default.TalkChannel[Properties.Settings.Default.TalkChannel.Count - 1])
                    {
                        cnls += ".";
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
            .AddField("Current talk channel/s:", cnls, false)
            .AddField("Current log channel:", (_client.Guilds.First().GetTextChannel(Properties.Settings.Default.LogChannel)).Mention, false)
            .AddField("Current ff news channel:", (_client.Guilds.First().GetTextChannel(Properties.Settings.Default.news_channel)).Mention, false)
            .AddField("Current ff update channel:", (_client.Guilds.First().GetTextChannel(Properties.Settings.Default.update_channel)).Mention, false)
            .AddField("Current ff status channel:", (_client.Guilds.First().GetTextChannel(Properties.Settings.Default.status_channel)).Mention, false)
            .AddField("Current ff maintenance channel:", (_client.Guilds.First().GetTextChannel(Properties.Settings.Default.maintenance_channel)).Mention, false)
            .WithFooter("Take care.")
            .WithTimestamp(DateTimeOffset.Now)
            .Build();
            var m = await command.FollowupAsync("", embed: admin_embc, ephemeral: true);
            borrar_msg(m, 20);

        }


        //              /logc
        private async Task Command_logc(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var logc_embD = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... <:bossicon:1311094313714974812>")
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: logc_embD, ephemeral: true);
                borrar_msg(m);
                return;
            }

            Properties.Settings.Default.LogChannel = selectedChannel.Id;
            Properties.Settings.Default.Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as Log Channel");
            var t = _client.Guilds.First().GetTextChannel(selectedChannel.Id);

            var talkc_embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly. <:bossicon:1311094313714974812>")
                .AddField("Channel", selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            var m2 = await command.FollowupAsync("", embed: talkc_embD, ephemeral: true);
            borrar_msg(m2);
            await ZenosLog($"{command.User.Mention} sets {t.Mention} as log channel.");
        }


        //              /newsc
        private async Task Command_newsc(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var embD2 = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... <:bossicon:1311094313714974812>")
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: embD2, ephemeral: true);
                borrar_msg(m);
                return;
            }

            Properties.Settings.Default.news_channel = selectedChannel.Id;
            Properties.Settings.Default.Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as ff news Channel");
            var t = _client.Guilds.First().GetTextChannel(selectedChannel.Id);

            var embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly for ff news. <:bossicon:1311094313714974812>")
                .AddField("Channel", selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            var m2 = await command.FollowupAsync("", embed: embD, ephemeral: true);
            await ZenosLog($"{command.User.Mention} sets {t.Mention} as ff news channel.");
            borrar_msg(m2);
        }


        //              /uppdatec
        private async Task Command_updatec(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var embD2 = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... <:bossicon:1311094313714974812>")
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: embD2, ephemeral: true);
                borrar_msg(m);
                return;
            }

            Properties.Settings.Default.update_channel = selectedChannel.Id;
            Properties.Settings.Default.Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as ff updates Channel");
            var t = _client.Guilds.First().GetTextChannel(selectedChannel.Id);

            var embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly for ff updates. <:bossicon:1311094313714974812>")
                .AddField("Channel", selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            var m2 = await command.FollowupAsync("", embed: embD, ephemeral: true);
            await ZenosLog($"{command.User.Mention} sets {t.Mention} as ff updates channel.");
            borrar_msg(m2);


        }


        //              /statusc
        private async Task Command_statusc(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var embD2 = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... <:bossicon:1311094313714974812>")
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: embD2, ephemeral: true);
                borrar_msg(m);
                return;
            }

            Properties.Settings.Default.status_channel = selectedChannel.Id;
            Properties.Settings.Default.Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as ff status Channel");
            var t = _client.Guilds.First().GetTextChannel(selectedChannel.Id);

            var embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly for ff status. <:bossicon:1311094313714974812>")
                .AddField("Channel", selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            var m2 = await command.FollowupAsync("", embed: embD, ephemeral: true);
            await ZenosLog($"{command.User.Mention} sets {t.Mention} as ff status channel.");
            borrar_msg(m2);

        }


        //              /maintenancec
        private async Task Command_maintenancec(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var embD2 = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... <:bossicon:1311094313714974812>")
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m2 = await command.FollowupAsync("", embed: embD2, ephemeral: true);
                borrar_msg(m2);
                return;
            }

            Properties.Settings.Default.maintenance_channel = selectedChannel.Id;
            Properties.Settings.Default.Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as ff maintenance Channel");
            var t = _client.Guilds.First().GetTextChannel(selectedChannel.Id);

            var embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly for ff maintenance. <:bossicon:1311094313714974812>")
                .AddField("Channel", selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            var m = await command.FollowupAsync("", embed: embD, ephemeral: true);
            await ZenosLog($"{command.User.Mention} sets {t.Mention} as ff maintenance channel.");
            borrar_msg(m);
        }



        /********************
          LodestoneHandler
        *////////////////////
        private async Task<List<Embed>> LodestoneHandler(int cantidad, SocketSlashCommand command, string kindOf)
        {
            //SOURCE https://documenter.getpostman.com/view/1779678/TzXzDHVk#5bd3a0a5-43b1-408d-bb7a-1788f22662a8
            //  topics
            string apiUrl = "";

            switch (kindOf)
            {
                case "news":
                    apiUrl = "https://na.lodestonenews.com/news/topics?locale=eu";
                    break;

                case "maintenance":
                    apiUrl = "https://na.lodestonenews.com/news/maintenance/current?locale=eu";
                    break;

                case "updates":
                    apiUrl = "https://na.lodestonenews.com/news/updates?locale=eu";
                    break;

                case "status":
                    apiUrl = "https://na.lodestonenews.com/news/status?locale=eu";
                    break;

                default:
                    apiUrl = "https://na.lodestonenews.com/news/topics?locale=eu";
                    break;
            }
            HttpClient client = new HttpClient();
            string jsonMaintenance = "", jsonCommon = "";
            MaintenanceRoot data = null;
            List<LodestoneNews> newsListD = new List<LodestoneNews>();

            //LodestoneMaintenance
            //List<LodestoneMaintenance> newsListM = new List<LodestoneMaintenance>();
            bool empty = true;

            if (kindOf == "maintenance")
            {
                //newsListM = JsonConvert.DeserializeObject<List<LodestoneMaintenance>>(jsonResponse);
                jsonMaintenance = await client.GetStringAsync(apiUrl);
                if (string.IsNullOrEmpty(jsonMaintenance)) { Print("NEWS ARE EMPTY OR NULL"); }
                data = JsonConvert.DeserializeObject<MaintenanceRoot>(jsonMaintenance);
                empty = (data == null || data.Game.Count == 0) ? true : false;
                cantidad = 1; //plz xD

            }
            else
            {
                jsonCommon = await client.GetStringAsync(apiUrl);
                if (string.IsNullOrEmpty(jsonCommon)) { Print("NEWS ARE EMPTY OR NULL"); }
                newsListD = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonCommon);
                empty = (newsListD == null || newsListD.Count == 0) ? true : false;
            }

            if (empty)
            {
                string tit = (kindOf == "maintenance") ? "I got nothing..." : "Error";
                string str = (kindOf == "maintenance") ? "Is game on maintenance???" : "No data recieved.";

                Print("NEW LIST IS NULL");
                //await command.FollowupAsync("Something went wrong...");
                var embed = new EmbedBuilder()
                    .WithTitle(tit)
                    .WithDescription(str)
                    //.WithTimestamp(DateTimeOffset.Now)
                    .WithColor(Color.Red)
                    .Build();
                return new List<Embed> { embed };
            }

            List<Embed> ret = new List<Embed>();

            if (kindOf == "maintenance")
            {
                foreach (var news in data.Game)
                {
                    var embed = new EmbedBuilder()
                        .WithTitle(news.Title)
                        .WithUrl(news.Url)
                        .WithTimestamp(DateTime.Parse(news.Time))
                        .WithTimestamp(DateTime.Parse(news.Start))
                        .WithTimestamp(DateTime.Parse(news.End))
                        //.WithDescription(news.Description)
                        .WithColor(Color.Blue)
                        .WithFooter("From: Lodestone")
                        .Build();
                    //await command.FollowupAsync(embed: embed);

                    ret.Add(embed);
                }
            }
            else
            {
                foreach (var news in newsListD.Take(cantidad))
                {
                    var embed = new EmbedBuilder()
                        .WithTitle(news.Title)
                        .WithUrl(news.Url)
                        .WithTimestamp(news.Time)
                        .WithImageUrl(news.Image)
                        .WithDescription(news.Description)
                        .WithColor(Color.Blue)
                        .WithFooter("From: Lodestone")
                        .Build();
                    //await command.FollowupAsync(embed: embed);

                    ret.Add(embed);
                }

            }
            //Print(ret.Count.ToString());
            return ret;

        }



        /////////////////////////////////////////
        /////////////    Command Handler
        /////////////////////////////////////////
        private async Task SlashCommandHandlerAsync(SocketSlashCommand command)
        {

            //for conditional use            
            bool isAdmin = (command.User as SocketGuildUser).GuildPermissions.Administrator;
            bool canTalk = Check_Allowed_Channel(command.Channel);
            int error = 0;

            try
            {
                switch (command.CommandName)
                {

                    case "admin":

                        if (!isAdmin) { error = 1; goto default; }
                        if (!canTalk) { error = 3; goto default; }
                        await Command_admin(command);
                        break;

                    case "ffnews":

                        //maintenance
                        //news
                        //status
                        //updates
                        if (!canTalk) { error = 3; goto default; }
                        await Command_news(command, "news");

                        break;


                    case "ffstatus":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_news(command, "status");

                        break;


                    case "ffupdates":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_news(command, "updates");

                        break;

                    case "ffmaintenance":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_news(command, "maintenance");

                        break;


                    case "userinfo":

                        var user_n = command.Data.Options.FirstOrDefault(opt => opt.Name == "name");
                        if (user_n?.Value is IUser user_name) { } else { goto default; }
                        if (!isAdmin) { error = 1; goto default; }
                        if (!canTalk) { error = 3; goto default; }
                        if (user_name.IsBot) { error = 2; goto default; } //don't check bots ¬¬
                        await Command_userinfo(command, user_name);

                        break;


                    case "timec":

                        var msg = command.Data.Options.FirstOrDefault(opt => opt.Name == "time");
                        if (msg?.Value is string user_msg) { } else { goto default; }//exit on fail
                        if (!canTalk) { error = 3; goto default; }
                        await Command_timec(command, user_msg);

                        break;


                    case "timers":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_timers(command);

                        break;


                    case "talkc":

                        if (!isAdmin) { error = 1; goto default; }
                        var channel = command.Data.Options.FirstOrDefault(opt => opt.Name == "channel");
                        if (channel?.Value is SocketChannel selectedChannel) { } else { error = 3; goto default; }//exit on fail
                        await Command_talkc(command, selectedChannel);

                        break;

                    case "logc":

                        if (!isAdmin) { error = 1; goto default; }
                        var channelL = command.Data.Options.FirstOrDefault(opt => opt.Name == "channell");
                        if (channelL?.Value is SocketChannel selectedChannelL) { } else { error = 3; goto default; }//exit on fail
                        await Command_logc(command, selectedChannelL);

                        break;

                    case "newsc":

                        if (!isAdmin) { error = 1; goto default; }
                        var channelNc = command.Data.Options.FirstOrDefault(opt => opt.Name == "newsc");
                        if (channelNc?.Value is SocketChannel selectedChannelNc) { } else { error = 3; goto default; }//exit on fail
                        await Command_newsc(command, selectedChannelNc);

                        break;

                    case "updatec":

                        if (!isAdmin) { error = 1; goto default; }
                        var channelUc = command.Data.Options.FirstOrDefault(opt => opt.Name == "updatec");
                        if (channelUc?.Value is SocketChannel selectedChannelUc) { } else { error = 3; goto default; }//exit on fail
                        await Command_updatec(command, selectedChannelUc);

                        break;

                    case "statusc":

                        if (!isAdmin) { error = 1; goto default; }
                        var channelSc = command.Data.Options.FirstOrDefault(opt => opt.Name == "statusc");
                        if (channelSc?.Value is SocketChannel selectedChannelSc) { } else { error = 3; goto default; }//exit on fail
                        await Command_statusc(command, selectedChannelSc);

                        break;

                    case "maintenancec":

                        if (!isAdmin) { error = 1; goto default; }
                        var channelMc = command.Data.Options.FirstOrDefault(opt => opt.Name == "maintenancec");
                        if (channelMc?.Value is SocketChannel selectedChannelMc) { } else { error = 3; goto default; }//exit on fail
                        await Command_maintenancec(command, selectedChannelMc);

                        break;


                    case "listc":

                        if (!isAdmin) { error = 1; goto default; }
                        await Command_listc(command);

                        break;

                    //////////////////////////    Errors
                    default:

                        string part1 = "", part2 = "";

                        switch (error)
                        {
                            case 1:
                                part1 = "What you trying?";
                                part2 = "/YouNotAdmin";
                                var t = _client.Guilds.First().GetTextChannel(command.Channel.Id);
                                await ZenosLog($"{command.User.Mention} try to use  /{command.CommandName} command on {t.Mention}, but is not admin, i say nothing!!");
                                break;

                            case 2:
                                part1 = "I don't understand...";
                                part2 = "/WhatYouSendMe?";
                                break;

                            case 3:
                                part1 = "Im not allowed to talk here...";
                                part2 = "/NotForZenos♥";
                                break;

                            default:
                                part1 = "Easter egg? out of limits";
                                part2 = "This never should happen.... gj xD";
                                break;
                        }
                        var def_emb = new EmbedBuilder()
                            .WithTitle($"Zeno♥.")
                            .WithDescription("Because you looks lost. <:disconnecting:1311089532527054909>")
                            .WithColor(Color.Red)
                            .AddField(part1, part2, false)
                            .Build();
                        var m = await command.FollowupAsync("", embed: def_emb, ephemeral: true);
                        borrar_msg(m);
                        break;
                }

            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }



        /********************
         MessageReceivedAsync
        *////////////////////
        private Task MessageReceivedAsync(SocketMessage message)
        {
            /*
            var userMessage = message as SocketUserMessage;
            if (message.Author.IsBot || userMessage == null) return;
            if (!Check_Allowed_Channel(message.Channel)) { return; }

            await userMessage.Channel.TriggerTypingAsync();

            string answer = "hmm...";
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

            if (mentionedUser != null)
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
            */
            return Task.CompletedTask;
        }


        /********************
         Check_Allowed_Channel
        *////////////////////
        private bool Check_Allowed_Channel(ISocketMessageChannel channel_to_check)
        {
            //Chech if can talk on channel

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
         Check_Allowed_Channel
        *////////////////////
        private bool Check_Allowed_Channel(SocketChannel channel_to_check)
        {
            //Chech if can talk on channel

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
                Print
        *////////////////////
        private void Print(string line, bool showname = true)
        {
            //Console bot print with timespamp
            string time_stamp = "", h = "", m = "", s = "", ms = "";
            DateTime n = DateTime.Now;
            //0 on left
            if (n.Hour < 10) { h = "0"; }
            if (n.Minute < 10) { m = "0"; }
            if (n.Second < 10) { s = "0"; }
            if (n.Millisecond < 100) { ms = "0"; }
            if (n.Millisecond < 10) { ms = "00"; }

            //getting values
            h += n.Hour.ToString();
            m += n.Minute.ToString();
            s += n.Second.ToString();
            ms += n.Millisecond.ToString();
            //making timestamp
            time_stamp = h + ":" + m + ":" + s + "." + ms + " "; //format 00:00:00

            if (showname) { line = "Zeno♥ - " + line; }
            Console.WriteLine(time_stamp + " " + line);
        }


        /********************
            AddTalkChannel
        *////////////////////
        private void AddTalkChannel(String channel)
        {
            //Add allowed talk channel
            StringCollection channels = Properties.Settings.Default.TalkChannel;
            if (channels == null) channels = new StringCollection();
            channels.Add(channel);
            Properties.Settings.Default.TalkChannel = channels;
            Properties.Settings.Default.Save();
        }

        /********************
            RemoveTalkChannel
        *////////////////////
        private void RemoveTalkChannel(String channel)
        {
            //Remove allowed talk channel
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


        /********************
            ZenosLog
        *////////////////////
        private async Task ZenosLog(string message)
        {
            SocketTextChannel canal = _client.Guilds.First().GetTextChannel(Properties.Settings.Default.LogChannel);
            await canal.SendMessageAsync(message);
        }


        /********************
            borrar_msg
        *////////////////////
        private void borrar_msg(RestFollowupMessage botMessage, int tiempo = 8)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(tiempo * 1000);
                try { await botMessage.DeleteAsync(); } catch (Exception ex) { Print(ex.Message); }

            });
        }


        private void Check_FF_updates()
        {
            _ = Task.Run(async () =>
            {
                int each = 5; //min
                bool c_news = Properties.Settings.Default.news_channel == 0;
                bool c_status = Properties.Settings.Default.status_channel == 0;
                bool c_update = Properties.Settings.Default.update_channel == 0;
                bool c_maintenance = Properties.Settings.Default.maintenance_channel == 0;

                if (c_news || c_status || c_update || c_maintenance)
                {
                    SocketTextChannel _channel = (SocketTextChannel)_client.Guilds.First().GetChannel(Properties.Settings.Default.LogChannel);
                    await _channel.SendMessageAsync($"Final Fantasy XIV News is stoped because i miss where i can put the news, trying again on {each} minutes." +
                        $"{Environment.NewLine} Set it usung `/newsc` `/updatec` `/statusc` `/maintenancec`");
                }

                while (true)
                {

                    try
                    {
                        string cmsg = "";
                        string mmsg = "";
                        string apiUrl = "https://na.lodestonenews.com/news/topics?locale=eu";
                        HttpClient client = new HttpClient();
                        string jsonCommon = "";
                        bool empty = true;

                        //LodestoneNews
                        List<LodestoneNews> newsListD = new List<LodestoneNews>();
                        jsonCommon = await client.GetStringAsync(apiUrl);
                        if (string.IsNullOrEmpty(jsonCommon)) { Print("NEWS ARE EMPTY OR NULL"); }
                        newsListD = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonCommon);
                        empty = (newsListD == null || newsListD.Count == 0) ? true : false;
                        bool havenews = !(Properties.Settings.Default.news_last_id == newsListD.First().Id);
                        //Print($"New updates? = {!empty && havenews}  ");
                        cmsg += $"News:{!empty && havenews} | ";
                        if (!empty && havenews)
                        {

                            SocketTextChannel news_channel = _client.Guilds.First().GetTextChannel(Properties.Settings.Default.news_channel);
                            bool first = true;
                            string bera = "";

                            foreach (var item in newsListD)
                            {
                                if (bera == item.Id) { break; } // don't show again
                                if (first)
                                {
                                    bera = Properties.Settings.Default.news_last_id; // i need that xD
                                    first = false;
                                    Properties.Settings.Default.news_last_id = item.Id;
                                    Properties.Settings.Default.Save();
                                }

                                var embed = new EmbedBuilder()
                                    .WithTitle(item.Title)
                                    .WithUrl(item.Url)
                                    .WithTimestamp(item.Time)
                                    .WithImageUrl(item.Image)
                                    .WithDescription(item.Description)
                                    .WithColor(Color.Blue)
                                    .WithFooter("From: Lodestone")
                                    .Build();
                                await news_channel.SendMessageAsync("", embed: embed);
                            }

                        }


                        //status
                        apiUrl = "https://na.lodestonenews.com/news/status?locale=eu";
                        client = new HttpClient();
                        jsonCommon = "";
                        empty = true;
                        newsListD = new List<LodestoneNews>();

                        jsonCommon = await client.GetStringAsync(apiUrl);
                        if (string.IsNullOrEmpty(jsonCommon)) { Print("NEWS ARE EMPTY OR NULL"); }
                        newsListD = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonCommon);
                        empty = (newsListD == null || newsListD.Count == 0) ? true : false;
                        havenews = !(Properties.Settings.Default.status_last_id == newsListD.First().Id);
                        //Print($"Status updates? = {!empty && havenews}  ");
                        cmsg += $"Status:{!empty && havenews} | ";

                        if (!empty && havenews)
                        {

                            SocketTextChannel news_channel = _client.Guilds.First().GetTextChannel(Properties.Settings.Default.status_channel);
                            bool first = true;
                            string bera = "";

                            foreach (var item in newsListD)
                            {
                                if (bera == item.Id) { break; } // don't show again
                                if (first)
                                {
                                    bera = Properties.Settings.Default.status_last_id;
                                    first = false;
                                    Properties.Settings.Default.status_last_id = item.Id;
                                    Properties.Settings.Default.Save();
                                }

                                var embed = new EmbedBuilder()
                                    .WithTitle(item.Title)
                                    .WithUrl(item.Url)
                                    .WithTimestamp(item.Time)
                                    .WithImageUrl(item.Image)
                                    .WithDescription(item.Description)
                                    .WithColor(Color.Blue)
                                    .WithFooter("From: Lodestone")
                                    .Build();
                                await news_channel.SendMessageAsync("", embed: embed);
                            }

                        }




                        //update
                        apiUrl = "https://na.lodestonenews.com/news/updates?locale=eu";
                        client = new HttpClient();
                        jsonCommon = "";
                        empty = true;
                        newsListD = new List<LodestoneNews>();


                        jsonCommon = await client.GetStringAsync(apiUrl);
                        if (string.IsNullOrEmpty(jsonCommon)) { Print("NEWS ARE EMPTY OR NULL"); }
                        newsListD = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonCommon);
                        empty = (newsListD == null || newsListD.Count == 0) ? true : false;
                        havenews = !(Properties.Settings.Default.update_last_id == newsListD.First().Id);
                        //Print($"Updates updates? = {!empty && havenews}  ");
                        cmsg += $"Update:{!empty && havenews}";

                        if (!empty && havenews)
                        {

                            SocketTextChannel news_channel = _client.Guilds.First().GetTextChannel(Properties.Settings.Default.update_channel);
                            bool first = true;
                            string hold_new = "";

                            foreach (var item in newsListD)
                            {
                                if (hold_new == item.Id) { break; }

                                if (first)
                                {
                                    first = false;
                                    hold_new = Properties.Settings.Default.update_last_id;
                                    Properties.Settings.Default.update_last_id = item.Id;
                                    Properties.Settings.Default.Save();
                                }

                                var embed = new EmbedBuilder()
                                    .WithTitle(item.Title)
                                    .WithUrl(item.Url)
                                    .WithTimestamp(item.Time)
                                    .WithImageUrl(item.Image)
                                    .WithDescription(item.Description)
                                    .WithColor(Color.Blue)
                                    .WithFooter("From: Lodestone")
                                    .Build();
                                await news_channel.SendMessageAsync("", embed: embed);
                            }

                        }


                        //maintenance
                        //Game
                        apiUrl = "https://lodestonenews.com/news/maintenance/current?locale=eu";
                        client = new HttpClient();
                        jsonCommon = "";
                        MaintenanceRoot data = null;
                        empty = true;
                        newsListD = new List<LodestoneNews>();

                        jsonCommon = await client.GetStringAsync(apiUrl);
                        if (string.IsNullOrEmpty(jsonCommon)) { Print("NEWS ARE EMPTY OR NULL"); }
                        data = JsonConvert.DeserializeObject<MaintenanceRoot>(jsonCommon);
                        empty = (data == null || data.Game.Count == 0) ? true : false;
                        //Print($"Maintenance game updates? = {!empty && Properties.Settings.Default.maintenance_last_game_id != data.Game.First().Id}  ");
                        mmsg += $"M/Game:{!empty && Properties.Settings.Default.maintenance_last_game_id != data.Game.First().Id} | ";

                        if (empty)
                        {
                            Properties.Settings.Default.maintenance_last_game_id = "";
                            Properties.Settings.Default.Save();
                        }

                        if ((!empty) && Properties.Settings.Default.maintenance_last_game_id != data.Game.First().Id)
                        {

                            SocketTextChannel news_channel = _client.Guilds.First().GetTextChannel(Properties.Settings.Default.maintenance_channel);
                            bool first = true;
                            string hold_new = "";

                            foreach (var news in data.Game) //should be one
                            {
                                if (first)
                                {
                                    first = false;
                                    hold_new = Properties.Settings.Default.maintenance_last_game_id;
                                    Properties.Settings.Default.maintenance_last_game_id = news.Id;
                                    Properties.Settings.Default.Save();
                                }

                                string st = utime(DateTime.Parse(news.Start));
                                string et = utime(DateTime.Parse(news.End));
                                string tt = utime(DateTime.Parse(news.Time));

                                var embed = new EmbedBuilder()
                                    .WithTitle(news.Title)
                                    .WithUrl(news.Url)
                                    .AddField($"Start time: {Environment.NewLine + utime(DateTime.Parse(news.Start), "d") + Environment.NewLine + utime(DateTime.Parse(news.Start), "t")}", st, true)
                                    .AddField($"End time: {Environment.NewLine + utime(DateTime.Parse(news.End), "d") + Environment.NewLine + utime(DateTime.Parse(news.End), "t")}", et, true)
                                    //.WithTimestamp(DateTime.Parse(news.Time))
                                    //.WithDescription(news.Description)
                                    .WithColor(Color.Blue)
                                    .WithFooter($"From: Lodestone")
                                    .Build();

                                await news_channel.SendMessageAsync("Final Fantasy XIV - Game maintenance", embed: embed);
                            }
                        }

                        //Mog station
                        empty = (data == null || data.Mog.Count == 0) ? true : false;
                        //Print($"Maintenance mog updates? = {!empty && Properties.Settings.Default.maintenance_last_mog_id != data.Mog.First().Id}  ");
                        mmsg += $"M/Mog:{!empty && Properties.Settings.Default.maintenance_last_mog_id != data.Mog.First().Id} | ";

                        if (empty)
                        {
                            Properties.Settings.Default.maintenance_last_mog_id = "";
                            Properties.Settings.Default.Save();
                        }
                        //Properties.Settings.Default.maintenance_last_mog_id = "";

                        if ((!empty) && Properties.Settings.Default.maintenance_last_mog_id != data.Mog.First().Id)
                        {
                            SocketTextChannel news_channel = _client.Guilds.First().GetTextChannel(Properties.Settings.Default.maintenance_channel);
                            bool first = true;
                            string hold_new = "";

                            foreach (var news in data.Mog) //should be one
                            {
                                if (first)
                                {
                                    first = false;
                                    hold_new = Properties.Settings.Default.maintenance_last_mog_id;
                                    Properties.Settings.Default.maintenance_last_mog_id = news.Id;
                                    Properties.Settings.Default.Save();
                                }

                                string st = utime(DateTime.Parse(news.Start));
                                string et = utime(DateTime.Parse(news.End));
                                string tt = utime(DateTime.Parse(news.Time));

                                var embed = new EmbedBuilder()
                                    .WithTitle(news.Title)
                                    .WithUrl(news.Url)
                                    .AddField($"Start time: {Environment.NewLine + utime(DateTime.Parse(news.Start), "d") + Environment.NewLine + utime(DateTime.Parse(news.Start), "t")}", st, true)
                                    .AddField($"End time: {Environment.NewLine + utime(DateTime.Parse(news.End), "d") + Environment.NewLine + utime(DateTime.Parse(news.End), "t")}", et, true)
                                    //.WithTimestamp(DateTime.Parse(news.Time))
                                    //.WithDescription(news.Description)
                                    .WithColor(Color.Blue)
                                    .WithFooter($"From: Lodestone")
                                    .Build();

                                await news_channel.SendMessageAsync("Final Fantasy XIV - Mog Station maintenance", embed: embed);
                            }
                        }



                        //Lodestone
                        empty = (data == null || data.Lodestone.Count == 0) ? true : false;
                        //Print($"Maintenance lodestone updates? = {!empty && Properties.Settings.Default.maintenance_last_lodestone_id != data.Lodestone.First().Id}  ");
                        mmsg += $"M/Lodestone:{!empty && Properties.Settings.Default.maintenance_last_lodestone_id != data.Lodestone.First().Id} | ";

                        if (empty)
                        {
                            Properties.Settings.Default.maintenance_last_lodestone_id = "";
                            Properties.Settings.Default.Save();
                        }
                        //Properties.Settings.Default.maintenance_last_lodestone_id = "";

                        if ((!empty) && Properties.Settings.Default.maintenance_last_lodestone_id != data.Lodestone.First().Id)
                        {
                            SocketTextChannel news_channel = _client.Guilds.First().GetTextChannel(Properties.Settings.Default.maintenance_channel);
                            bool first = true;
                            string hold_new = "";

                            foreach (var news in data.Lodestone) //should be one
                            {
                                if (first)
                                {
                                    first = false;
                                    hold_new = Properties.Settings.Default.maintenance_last_lodestone_id;
                                    Properties.Settings.Default.maintenance_last_lodestone_id = news.Id;
                                    Properties.Settings.Default.Save();
                                }

                                string st = utime(DateTime.Parse(news.Start));
                                string et = utime(DateTime.Parse(news.End));
                                string tt = utime(DateTime.Parse(news.Time));

                                var embed = new EmbedBuilder()
                                    .WithTitle(news.Title)
                                    .WithUrl(news.Url)
                                    .AddField($"Start time: {Environment.NewLine + utime(DateTime.Parse(news.Start), "d") + Environment.NewLine + utime(DateTime.Parse(news.Start), "t")}", st, true)
                                    .AddField($"End time: {Environment.NewLine + utime(DateTime.Parse(news.End), "d") + Environment.NewLine + utime(DateTime.Parse(news.End), "t")}", et, true)
                                    //.WithTimestamp(DateTime.Parse(news.Time))
                                    //.WithDescription(news.Description)
                                    .WithColor(Color.Blue)
                                    .WithFooter($"From: Lodestone")
                                    .Build();

                                await news_channel.SendMessageAsync("Final Fantasy XIV - Lodestone maintenance", embed: embed);
                            }

                        }



                        //Companion
                        empty = (data == null || data.Companion.Count == 0) ? true : false;
                        //Print($"Maintenance companion updates? = {!empty && Properties.Settings.Default.maintenance_last_companion_id != data.Companion.First().Id}  ");
                        mmsg += $"M/Companion:{!empty && Properties.Settings.Default.maintenance_last_companion_id != data.Companion.First().Id}";

                        if (empty)
                        {
                            Properties.Settings.Default.maintenance_last_companion_id = "";
                            Properties.Settings.Default.Save();
                        }
                        //TEST
                        //Properties.Settings.Default.maintenance_last_companion_id = "";

                        if ((!empty) && Properties.Settings.Default.maintenance_last_companion_id != data.Companion.First().Id)
                        {
                            SocketTextChannel news_channel = _client.Guilds.First().GetTextChannel(Properties.Settings.Default.maintenance_channel);
                            bool first = true;
                            string hold_new = "";

                            foreach (var news in data.Companion) //should be one
                            {
                                if (first)
                                {
                                    first = false;
                                    hold_new = Properties.Settings.Default.maintenance_last_companion_id;
                                    Properties.Settings.Default.maintenance_last_companion_id = news.Id;
                                    Properties.Settings.Default.Save();
                                }


                                string st = utime(DateTime.Parse(news.Start));
                                string et = utime(DateTime.Parse(news.End));
                                string tt = utime(DateTime.Parse(news.Time));

                                var embed = new EmbedBuilder()
                                    .WithTitle(news.Title)
                                    .WithUrl(news.Url)
                                    .AddField($"Start time: {Environment.NewLine + utime(DateTime.Parse(news.Start), "d") + Environment.NewLine + utime(DateTime.Parse(news.Start), "t")}", st, true)
                                    .AddField($"End time: {Environment.NewLine + utime(DateTime.Parse(news.End), "d") + Environment.NewLine + utime(DateTime.Parse(news.End), "t")}", et, true)
                                    //.WithTimestamp(DateTime.Parse(news.Time))
                                    //.WithDescription(news.Description)
                                    .WithColor(Color.Blue)
                                    .WithFooter($"From: Lodestone")
                                    .Build();

                                //TEST
                                //news_channel = (SocketTextChannel)_client.Guilds.First().GetChannel(1310199196233760858); //test-bot channel
                                //await news_channel.SendMessageAsync("Final Fantasy XIV - Companion maintenance", embed: embed);
                                await news_channel.SendMessageAsync("Final Fantasy XIV - Companion maintenance", embed: embed);
                            }

                        }

                        bool c1 = false, c2 = false;
                        if (cmsg.Contains(":true")) { Print(cmsg); c1 = true; }
                        if (mmsg.Contains(":true")) { Print(mmsg); c2 = true; }
                        if (c1 || c2)
                        {
                            Print($"Cheching again in {each} minutes...");
                        }
                        else
                        {
                            Print($"No updates, cheching again in {each} minutes...\"");
                        }

                    }
                    catch (Exception ex)
                    {
                        Print(ex.Message);
                    }


                    //               1 min * each
                    await Task.Delay(60000 * each);
                }
            });
        }

        private string utime(DateTime date, string mode = "R")
        {
            long unixTimestamp = new DateTimeOffset(date).ToUnixTimeSeconds();
            return $"<t:{unixTimestamp}:{mode}>";

        }

    }


    //Lodestone structs
    //
    /********************
            news?
    *////////////////////
    public class LodestoneNews
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }

    public class MaintenanceRoot
    {
        public List<MaintenanceEvent> Companion { get; set; }
        public List<MaintenanceEvent> Game { get; set; }
        public List<MaintenanceEvent> Lodestone { get; set; }
        public List<MaintenanceEvent> Mog { get; set; }
        public List<object> Psn { get; set; }
    }

    public class MaintenanceEvent
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool Current { get; set; }
    }

}
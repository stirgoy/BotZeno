using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

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
        }

        /********************
                MainAsync
        *////////////////////
        public async Task MainAsync()
        {
            // Token == Private data, some ways to not hard code: put inside external file and read it
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

        private Task ReadyAsync()
        {
            Print("Connected!");
            var guild = _client.Guilds.ToList()[0];

            Print("Loading slash commands...");
            _ = Task.Run(async () => { await ActualizarComandos(); });


            Print($"Name:       {_client.CurrentUser.Username}");
            Print($"Id:         {_client.CurrentUser.Id}");
            Print($"Token:      {_client.TokenType}");
            Print($"Veryfed:    {_client.CurrentUser.IsVerified}");
            Print($"Online on:  {_client.Guilds.Count} servers");
            Print($"Latency:    {_client.Latency}");
            Print($"Talk channel:    {Properties.Settings.Default.TalkChannel}");




#if DEBUG
            Print("DEBUG MODE!!!!!!!!!!!!!!!!!\"");
#endif
            Print("I'm ready!\"");

            return Task.CompletedTask;

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
            .WithName("news")
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
            .WithName("timec")
            .WithDescription("I bring you a dynamic display of date time and the code ♥")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("time")
                .WithDescription("Text a date time. example: YYYY-MM-DD HH:mm:ss, can be only the hour.")
                .WithType(ApplicationCommandOptionType.String)
                .WithRequired(true))
            .Build()

            };
            

            try
            {
                await guild.BulkOverwriteApplicationCommandAsync(comandos);
                Print("Application Commands registered successfully!");

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
            .AddField("Edit my answer channel", "!talkc", false)
            .AddField("Show allowed channels", "!listc", false)
            .WithFooter("Take care.")
            .WithTimestamp(DateTimeOffset.Now)
            .Build();
            await command.FollowupAsync("", embed: admin_emb, ephemeral: true);
        }

        //              /news
        private async Task Command_news(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: false);

            try
            {
                int def = 1; int min = 1; int max = 5;

                var cantidadOption = command.Data.Options.FirstOrDefault(opt => opt.Name == "number");
                int cantidad = cantidadOption?.Value is long value ? (int)value : def;
                if (cantidad > max) { cantidad = max; }
                if (cantidad <= 0) { cantidad = min; }

                //SOURCE https://documenter.getpostman.com/view/1779678/TzXzDHVk#5bd3a0a5-43b1-408d-bb7a-1788f22662a8
                //  topics
                string apiUrl = "https://na.lodestonenews.com/news/topics";

                //  maintenance   >> https://na.lodestonenews.com/news/maintenance/current
                //string apiUrl = "https://na.lodestonenews.com/news/maintenance"; 

                //  need new struct and chech 
                //string apiUrl = "https://na.lodestonenews.com/news/notices"; 

                //  last update
                //string apiUrl = "https://na.lodestonenews.com/news/updates"; 

                //  https://na.lodestonenews.com/news/status

                //string apiUrl = "https://na.lodestonenews.com/news/updates"; 

                // status news
                //string apiUrl = "https://na.lodestonenews.com/news/status"; 

                HttpClient client = new HttpClient();
                string jsonResponse = await client.GetStringAsync(apiUrl);
                if (string.IsNullOrEmpty(jsonResponse)) { Print("NEWS ARE EMPTY OR NULL"); }

                var newsList = JsonConvert.DeserializeObject<List<LodestoneNews>>(jsonResponse);

                if (newsList == null || newsList.Count == 0)
                {
                    Print("NEW LIST IS NULL");
                    await command.FollowupAsync("Something went wrong...");
                    return;
                }

                foreach (var news in newsList.Take(cantidad))
                {
                    var embed2 = new EmbedBuilder()
                        .WithTitle(news.Title)
                        .WithUrl(news.Url)
                        .WithTimestamp(news.Time)
                        .WithImageUrl(news.Image)
                        .WithDescription(news.Description)
                        .WithColor(Color.Blue)
                        .WithFooter("From: Lodestone News")
                        .Build();
                    await command.FollowupAsync(embed: embed2);
                    await Task.Delay(500);
                }
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

            var user_emb = new EmbedBuilder()
                .WithTitle($"User information")
                .AddField("Display name", $"{sgu.DisplayName}", true)
                .AddField("Discord name", $"{sgu.Username}", true)
                .AddField("Global name", $"{sgu.GlobalName}", true)
                .AddField("Server name", $"{nik}", true)
                .AddField("Is admin", $"{sgu.GuildPermissions.Administrator}", true)
                .AddField("Roles", $"{roleList}")
                .WithThumbnailUrl(avatarUrl)
                .WithFooter($" My enemy.")
                .WithColor(Color.Orange)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();

            await command.FollowupAsync("", embed: user_emb, ephemeral: true);

        }


        //              /timec
        private async Task Command_timec(SocketSlashCommand command, string user_msg)
        {
            await command.DeferAsync(ephemeral: true);

            if (string.IsNullOrEmpty(user_msg))
            {
                await command.RespondAsync("Please provide a valid date my friend and time in the format YYYY-MM-DD HH:mm:ss.", ephemeral: true);
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


        //              /timec
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
                await command.FollowupAsync("", embed: talkc_embD, ephemeral: true);
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
                    string nme = _client.GetChannel(ulong.Parse(item)).ToString();
                    cnls += nme;
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
            .AddField("Current channel/s:", cnls, false)
            .WithFooter("Take care.")
            .WithTimestamp(DateTimeOffset.Now)
            .Build();
            await command.FollowupAsync("", embed: admin_embc, ephemeral: true);

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

                    case "news":


                        if (!canTalk) { error = 3; goto default; }
                        await Command_news(command);

                        break;


                    case "userinfo":

                        var user_n = command.Data.Options.FirstOrDefault(opt => opt.Name == "name");
                        if (user_n?.Value is IUser user_name) { } else { goto default; }
                        if (!isAdmin) { error = 1; goto default; }
                        if (!canTalk) { error = 3; goto default; }
                        if (user_name.IsBot) { goto default; } //don't check bots ¬¬
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
                        await command.RespondAsync("", embed: def_emb, ephemeral: true);
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

}


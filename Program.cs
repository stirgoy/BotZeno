using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Begu
{

    internal partial class Program
    {

        //commons
        readonly DiscordSocketClient _client;

        //server
        SocketGuild Kuru = null; //Star Guardians!

        //main
        static void Main() => new Program().MainAsync().GetAwaiter().GetResult();
        public Program()
        {



            /***********************************
                DiscordSocketClient
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
            //_commands = new CommandService();


            /********************
                Event Handlers
            *////////////////////
            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;
            _client.SlashCommandExecuted += SlashCommandHandlerAsync;

        }



        /********************
             SLASH COMMANDS
        *////////////////////

        //              /admin
        private async Task Command_a_commands(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: true);

            var admin_emb = new EmbedBuilder()
            .WithTitle("Admin commands. " + Emote.Bot.Mentor)
            .WithDescription(Emote.Bot.Sproud + " Because you looks lost. " + Emote.Bot.Sproud)
            .WithColor(Color.Orange)
            .AddField("Edit my answer channel", "/talkc", false)
            .AddField("Edit log channel", "/logc", false)
            .AddField("Edit my ff news channel", "/newsc", false)
            .AddField("Edit my ff status channel", "/statusc", false)
            .AddField("Edit my ff updates channel", "/updatec", false)
            .AddField("Edit my ff maintenance", "/maintenancec", false)
            .AddField("Show allowed answer channels", "/listc", false)
            .AddField("Shows server info from user", "/useri", false)
            .WithFooter("Take care.")
            .WithTimestamp(DateTimeOffset.Now)
            .Build();
            var sm = await command.FollowupAsync("", embed: admin_emb, ephemeral: true);
            BorrarMsg(sm, 20);
        }


        //              /userinfo
        private async Task Command_a_userinfo(SocketSlashCommand command, IUser user_name)
        {
            await command.DeferAsync(ephemeral: true);

            SocketGuildUser sgu = null;
            //              lulu                str
            //print(user_name.GlobalName + " " + user_name.Username);
            var serv_users = Kuru.Users;
            foreach (var item in serv_users)
            {
                if (item.Id == user_name.Id) { sgu = item; break; }
            }

            string roles = "Error getting roles. " + Emote.Bot.Boss;

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

            string admin = (sgu.GuildPermissions.Administrator) ? Emote.XD.GeenCircle : Emote.XD.RedCircle;

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
            BorrarMsg(m, 60);
        }

        //              /talkc
        private async Task Command_a_set_answer(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            //EXISTE
            if (Check_Allowed_Channel(selectedChannel))
            {

                RemoveTalkChannel(selectedChannel.Id.ToString());
                //msg 
                var talkc_embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription("So now i will ignore " + selectedChannel.ToString() + Emote.Bot.Boss)
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: talkc_embD, ephemeral: true);
                BorrarMsg(m);
                await ZenosLog($"{command.User.Mention} removes {selectedChannel} as talk channel.");
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
            await ZenosLog($"{command.User.Mention} sets {selectedChannel} as talk channel.");

        }


        //              /listc
        private async Task Command_a_answer(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: true);

            string cnls = "";

            if (Properties.Settings.Default.TalkChannel == null)
            { Properties.Settings.Default.TalkChannel = new StringCollection(); }

            //setting texts
            if (Properties.Settings.Default.TalkChannel.Count != 0)
            {

                foreach (string item in Properties.Settings.Default.TalkChannel)
                {
                    var t = Kuru.GetTextChannel(ulong.Parse(item));
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
            else
            {
                cnls = "No channel, use `/talkc` for set.";
            }




            (Properties.Settings.Default.status_channel) = 0;


            string logc1 = $"Current log channel/s: {((Properties.Settings.Default.LogChannel == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}";
            string logc2 = (Properties.Settings.Default.LogChannel == 0) ? "No channel, use `/logc` for set." : (Kuru.GetTextChannel(Properties.Settings.Default.LogChannel)).Mention;

            string newsc1 = $"Current ff news channel: {((Properties.Settings.Default.news_channel == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}";
            string newsc2 = (Properties.Settings.Default.news_channel == 0) ? "No channel, use `/newsc` for set." : (Kuru.GetTextChannel(Properties.Settings.Default.news_channel)).Mention;

            string updatec1 = $"Current ff update channel: {((Properties.Settings.Default.update_channel == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}";
            string updatec2 = (Properties.Settings.Default.update_channel == 0) ? "No channel, use `/updatec` for set." : (Kuru.GetTextChannel(Properties.Settings.Default.update_channel)).Mention;

            string status1 = $"Current ff status channel: {((Properties.Settings.Default.status_channel == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}";
            string status2 = (Properties.Settings.Default.status_channel == 0) ? "No channel, use `/statusc` for set." : (Kuru.GetTextChannel(Properties.Settings.Default.status_channel)).Mention;

            string maint1 = $"Current ff maintenance channel: {((Properties.Settings.Default.maintenance_channel == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}";
            string maint2 = (Properties.Settings.Default.maintenance_channel == 0) ? "No channel, use `/maintenancec` for set." : (Kuru.GetTextChannel(Properties.Settings.Default.maintenance_channel)).Mention;

            //embed
            var admin_embc = new EmbedBuilder()
            .WithTitle($"Admin settings status.")
            .WithDescription(Emote.Bot.Sproud + " Because you looks lost. " + Emote.Bot.Sproud)
            .WithColor(Color.Orange)
            .AddField($"Current answer channel/s: {((Properties.Settings.Default.TalkChannel.Count == 0) ? Emote.XD.RedCircle : Emote.XD.GeenCircle)}", cnls, false)
            .AddField(logc1, logc2, false)
            .AddField(newsc1, newsc2, false)
            .AddField(updatec1, updatec2, false)
            .AddField(status1, status2, false)
            .AddField(maint1, maint2, false)
            .WithFooter("Take care.")
            .WithTimestamp(DateTimeOffset.Now)
            .Build();
            var m = await command.FollowupAsync("", embed: admin_embc, ephemeral: true);
            BorrarMsg(m, 20);

        }


        //              /logc
        private async Task Command_a_set_log(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var logc_embD = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... " + Emote.Bot.Boss)
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: logc_embD, ephemeral: true);
                BorrarMsg(m);
                return;
            }

            Properties.Settings.Default.LogChannel = selectedChannel.Id;
            Properties.Settings.Default.Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as Log Channel");
            var t = Kuru.GetTextChannel(selectedChannel.Id);

            var talkc_embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly. " + Emote.Bot.Boss)
                .AddField("Channel", selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            var m2 = await command.FollowupAsync("", embed: talkc_embD, ephemeral: true);
            BorrarMsg(m2);
            await ZenosLog($"{command.User.Mention} sets {t.Mention} as log channel.");
        }

        //              /newsc
        private async Task Command_a_set_news(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var embD2 = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... " + Emote.Bot.Boss)
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: embD2, ephemeral: true);
                BorrarMsg(m);
                return;
            }

            Properties.Settings.Default.news_channel = selectedChannel.Id;
            Properties.Settings.Default.Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as ff news Channel");
            var t = Kuru.GetTextChannel(selectedChannel.Id);

            var embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly for ff news. " + Emote.Bot.Boss)
                .AddField("Channel", selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            var m2 = await command.FollowupAsync("", embed: embD, ephemeral: true);
            await ZenosLog($"{command.User.Mention} sets {t.Mention} as ff news channel.");
            BorrarMsg(m2);
        }


        //              /uppdatec
        private async Task Command_a_set_update(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var embD2 = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... " + Emote.Bot.Boss)
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: embD2, ephemeral: true);
                BorrarMsg(m);
                return;
            }

            Properties.Settings.Default.update_channel = selectedChannel.Id;
            Properties.Settings.Default.Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as ff updates Channel");
            var t = Kuru.GetTextChannel(selectedChannel.Id);

            var embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly for ff updates. " + Emote.Bot.Boss)
                .AddField("Channel", selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            var m2 = await command.FollowupAsync("", embed: embD, ephemeral: true);
            await ZenosLog($"{command.User.Mention} sets {t.Mention} as ff updates channel.");
            BorrarMsg(m2);


        }


        //              /statusc
        private async Task Command_a_set_status(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var embD2 = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... " + Emote.Bot.Boss)
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m = await command.FollowupAsync("", embed: embD2, ephemeral: true);
                BorrarMsg(m);
                return;
            }

            Properties.Settings.Default.status_channel = selectedChannel.Id;
            Properties.Settings.Default.Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as ff status Channel");
            var t = Kuru.GetTextChannel(selectedChannel.Id);

            var embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly for ff status. " + Emote.Bot.Boss)
                .AddField("Channel", selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            var m2 = await command.FollowupAsync("", embed: embD, ephemeral: true);
            await ZenosLog($"{command.User.Mention} sets {t.Mention} as ff status channel.");
            BorrarMsg(m2);

        }


        //              /maintenancec
        private async Task Command_a_set_maintenance(SocketSlashCommand command, SocketChannel selectedChannel)
        {
            await command.DeferAsync(ephemeral: true);

            if (selectedChannel == null)
            {
                var embD2 = new EmbedBuilder()
                .WithTitle("Settings Error")
                .WithDescription("Something is wrong... " + Emote.Bot.Boss)
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
                var m2 = await command.FollowupAsync("", embed: embD2, ephemeral: true);
                BorrarMsg(m2);
                return;
            }

            Properties.Settings.Default.maintenance_channel = selectedChannel.Id;
            Properties.Settings.Default.Save();
            Print($"Channel {selectedChannel} - {selectedChannel.Id} seted as ff maintenance Channel");
            var t = Kuru.GetTextChannel(selectedChannel.Id);

            var embD = new EmbedBuilder()
                .WithTitle("Settings")
                .WithDescription($"Channel saved correctly for ff maintenance. " + Emote.Bot.Boss)
                .AddField("Channel", selectedChannel.ToString())
                .WithColor(Color.Green)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();
            var m = await command.FollowupAsync("", embed: embD, ephemeral: true);
            await ZenosLog($"{command.User.Mention} sets {t.Mention} as ff maintenance channel. " + Emote.Bot.Maintenance);
            BorrarMsg(m);
        }

        //              /timec
        private async Task Command_timestamp(SocketSlashCommand command, string user_msg)
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
                .WithTitle($"Eorzean timers. " + Emote.Bot.FFXIV)
                .WithDescription(Emote.Bot.Sproud + " Because you looks lost. " + Emote.Bot.Sproud)
                .WithColor(Color.Green)
                .AddField(Emote.Bot.MSQ + " Weekly", weeklyDT, true)
                .AddField(Emote.Bot.Roulette + " Daily", dailyDT, true)
                .AddField(Emote.Bot.Gc + " GC", gcDT, true)
                .AddField(Emote.Bot.Fishing + " Ocean fishing", oceanDT, true)
                .AddField(Emote.Bot.Cactuar + " Cacpot", cacpotDT, true)
                .WithFooter("Take care.")
                .WithTimestamp(DateTimeOffset.Now)
                .Build();

            await command.FollowupAsync("", embed: embed);

        }


    }

}
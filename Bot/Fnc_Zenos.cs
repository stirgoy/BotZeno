using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /********************
            ZenosLog
        *//////////////////// server log, requieres channel id on config
        private static async Task ZenoLog(string message)
        {
            if (Config.ZenoLog)
            {
                SocketTextChannel canal = Kuru.GetTextChannel(Config.Channels.LogChannel);
                if (canal != null)
                {
                    await canal.SendMessageAsync(message);
                }
            }
        }


        //////////////////// same but with embed
        private static async Task ZenoLog(string title, string message, string img = "")
        {
            if (Config.ZenoLog)
            {
                SocketTextChannel canal = Kuru.GetTextChannel(Config.Channels.LogChannel);
                if (canal != null)
                {
                    Embed emb = CreateEmbed(
                        title,
                        message,
                        miniImage: img,
                        color: Color.Orange);

                    await canal.SendMessageAsync("", embed: emb);
                }
            }
        }

        /********************
            Autorole
        *//////////////////// 
        private async void Autorole(SocketGuildUser serveruser, SocketMessage message)
        {
            var userMessage = message as SocketUserMessage;
            var sproud = Kuru.GetRole(Role_sproud);
            var normal = Kuru.GetRole(Role_normal);

            if (serveruser.Roles.Contains(sproud))
            {
                BorrarMsg(userMessage, 0);

                await serveruser.AddRoleAsync(normal);
                await ZenoLog($"{Emote.Bot.LFP}Auto-role{Emote.Bot.LFP}", $"{serveruser.Mention} has been added to {normal.Mention}", serveruser.GetAvatarUrl());
                Print($"{serveruser.Mention} has been added to {normal.Mention}");

                await serveruser.RemoveRoleAsync(sproud);
                await ZenoLog($"{Emote.Bot.LFP}Auto-role{Emote.Bot.LFP}", $"{serveruser.Mention} has been removed to {sproud.Mention}", serveruser.GetAvatarUrl());
                Print($"{serveruser.Mention} has been removed to {sproud.Mention}");

                var greetingschannel = Kuru.GetTextChannel(Channel_greetings);
                await greetingschannel.SendMessageAsync($"{Emote.Bot.Pepeshookt} Welcome {userMessage.Author.Mention + " " + Emote.Bot.Happytuff} you and me will be best friends {Emote.Bot.Pepelove} and enemies {Emote.Bot.Boss}!!!");


                Print($"{serveruser.GlobalName} has registered  on server!!");

            }
            else
            {
                Print($"{message.Author.GlobalName} is trying to verify again...");

            }
        }

        /********************
            FFMaintTalk
        *//////////////////// 
        async Task<Embed> FFMaintTalk()
        {
            HttpClient client = new HttpClient();
            Embed embed = null;
            MaintenanceRoot data = null;
            string title = $"{Emote.Bot.LMaintenance} Current Maintenance";

            string jsonMaintenance = await client.GetStringAsync(XIVLN.APIs.MaintenanceCurrent);
            bool empty = true;

            if (!string.IsNullOrEmpty(jsonMaintenance))
            {
                data = JsonConvert.DeserializeObject<MaintenanceRoot>(jsonMaintenance);
                empty = (data == null || data.Game.Count == 0);
            }

            if (!empty)
            {
                foreach (var item in data.Game)
                {
                    string st = UnixTime(DateTime.Parse(item.Start));
                    string et = UnixTime(DateTime.Parse(item.End));
                    string tt = UnixTime(DateTime.Parse(item.Time));

                    embed = CreateEmbedField_2(
                        title,
                        "### " + item.Title + NL + NL + $"-# {tt}",
                        $"Start time: {NL + UnixTime(DateTime.Parse(item.Start), "d") + NL + UnixTime(DateTime.Parse(item.Start), "t")}",
                        st,
                        $"End time: {NL + UnixTime(DateTime.Parse(item.End), "d") + NL + UnixTime(DateTime.Parse(item.End), "t")}",
                        et,
                        "From: Lodestone News",
                        XIVLN.Config.FFLogo,
                        item.Url,
                        Color.Blue);
                }
            }
            else
            {
                embed = CreateEmbed(
                    "I got nothing... " + Emote.Bot.Disconnecting_party,
                    "Is game on maintenance??? " + Emote.Bot.Disconnecting_party,
                    color: Color.Red);
            }

            return embed;

        }


        /********************
            AnswerUser
        *//////////////////// 
        async void AnswerUser(SocketUserMessage msg, string content)
        {
#if !DEBUG
            await msg.Channel.TriggerTypingAsync();
            await Task.Delay(3000);
#endif
            await msg.ReplyAsync(content);
        }

        /********************
            SendGuide
        *//////////////////// 
        async void SendGuide(IDMChannel dm, int guide)
        {
            List<string> selguide = null;

            switch (guide)
            {
                case 0:
                    selguide = ClassRoom.Macros.Menu;
                    break;
                case 1:
                    selguide = ClassRoom.Macros.UsefulMacros;
                    break;
                case 2:
                    selguide = ClassRoom.Macros.HUD;
                    break;
                case 3:
                    selguide = ClassRoom.Macros.RetainerBasics;
                    break;
                default:
                    break;
            }



            foreach (var item in selguide)
            {
                await dm.SendMessageAsync(item);
                await Task.Delay(1000);
            }
        }

        /********************
            RegExFind
        *//////////////////// 
        bool RegExFind(string[] constant, string[] variable, string text)
        {
            string constantchain = string.Join("|", constant); // Une las palabras fijas con "|"
            string variablechain = string.Join("|", variable); // Une las palabras opcionales con "|"
            string patron = $@"\b({constantchain})\b.*\b({variablechain})\b|\b({variablechain})\b.*\b({constantchain})\b";
            Regex regex = new Regex(patron, RegexOptions.IgnoreCase);
            return regex.IsMatch(text);
        }


        /********************
                SendEventNotice
        *////////////////////
        private async void SendEventNotice(string uid, SocketGuildEvent server_event)
        {
            await Kuru.DownloadUsersAsync();
            var user = Kuru.GetUser(ulong.Parse(uid));
            IDMChannel dm = await user.CreateDMChannelAsync();
            Embed emb = CreateEmbed($"Hey! {user.DisplayName} Wake up!!", $"{Emote.Bot.Online} The event: **{server_event.Name}**{NL}Just start now!!!! {Emote.Bot.Online}", miniImage: Kuru.IconUrl);
            await dm.SendMessageAsync("", embed: emb);

        }

        /********************
                GetEvent
        *////////////////////
        private static string[] GetEvent(string event_id)
        {
            foreach (string[] item in Config.Events_Noticed)
            {
                if (event_id == item[0])
                {
                    return item;
                }
            }

            return new string[0];
        }

        /********************
                EventUserAdd
        *////////////////////
        private async Task EventUserAdd(Cacheable<SocketUser, RestUser, IUser, ulong> user, SocketGuildEvent server_event)
        {
            Print($"New user added on event {user.Id}");

            string[] event_data = GetEvent(server_event.Id.ToString());
            List<string[]> evnts = Config.Events_Noticed;

            if (!event_data.Contains(user.Id.ToString()))
            {
                for (int i = 0; i < evnts.Count; i++)
                {
                    if (evnts[i][0] == server_event.Id.ToString())
                    {
                        List<string> newlist = evnts[i].ToList();
                        newlist.Add(user.Id.ToString());
                        evnts[i] = newlist.ToArray();
                        Config.Events_Noticed = evnts;
                        await Config_Save();
                        break;
                    }
                }
            }


        }

        /********************
                Delete_Event
        *////////////////////
        private async Task Delete_Event(SocketGuildEvent server_event)
        {
            Print("Event deletd from config");

            foreach (var item in Config.Events_Noticed)
            {
                if (item[0] == server_event.Id.ToString())
                {
                    Config.Events_Noticed.Remove(item);
                    await Config_Save();
                    break;
                }
            }
        }

        /********************
            borrar_msg
        *//////////////////// for delete bot messages over time
        private static void BorrarMsg(RestFollowupMessage botMessage, int tiempo = 8)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(tiempo * 1000);
                try { await botMessage.DeleteAsync(); } catch (Exception ex) { Print(ex.Message); }
            });
        }

        private static void BorrarMsg(IUserMessage botMessage, int tiempo = 8)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(tiempo * 1000);
                try { await botMessage.DeleteAsync(); } catch (Exception ex) { Print(ex.Message); }
            });
        }


        /********************
            AddTalkChannel
        *////////////////////
        private async void AddTalkChannel(String channel)
        {
            //Add allowed talk channel
            StringCollection channels = Config.Channels.TalkChannel;
            if (channels == null) channels = new StringCollection();
            channels.Add(channel);
            Config.Channels.TalkChannel = channels;
            await Config_Save();
        }

        /********************
            RemoveTalkChannel
        *////////////////////
        private void RemoveTalkChannel(String channel)
        {
            //Remove allowed talk channel
            StringCollection channels = Config.Channels.TalkChannel;
            StringCollection new_channels = new StringCollection();
            foreach (var item in channels)
            {
                if (item != channel)
                {
                    new_channels.Add(item);
                }
            }

            Config.Channels.TalkChannel = new_channels;
            Config_Save();
        }
    }
}

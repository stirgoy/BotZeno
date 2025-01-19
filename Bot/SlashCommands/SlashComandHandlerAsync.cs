using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task SlashCommandHandlerAsync(SocketSlashCommand command)
        {

            //for conditional use            
            bool isAdmin = (command.User as SocketGuildUser).GuildPermissions.Administrator; //actually useless, handled by bot
            bool canTalk = false;
            StringCollection channels = Config.Channels.TalkChannel;

            if (channels != null)
            {
                foreach (var item in channels) { if (command.Channel.Id == ulong.Parse(item)) { canTalk = true; break; } }
            }

            var suser = Kuru.GetUser(command.User.Id);
            if (suser != null)
            {
                if (suser.GuildPermissions.Administrator)
                {
                    canTalk = true;
                }
            }

            //bool canTalk = Check_Allowed_Channel(command.Channel);
            int error = 0;

            try
            {
                switch (command.CommandName)
                {


                    case "timers":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_timers(command);

                        break;


                    case "a_answer":

                        if (!isAdmin) { error = 1; goto default; }
                        await Command_a_answer(command);

                        break;


                    case "a_userinfo":

                        if (!canTalk) { error = 3; goto default; }
                        if (command.User.IsBot) { error = 2; goto default; } //don't check bots ¬¬
                        await Command_a_userinfo(command);

                        break;

                    case "fftopics":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_ffhandler(command, "news");

                        break;


                    case "ffstatus":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_ffhandler(command, "status");

                        break;

                    case "ffmaintenance":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_ffhandler(command, "maintenance");

                        break;

                    case "ffmaintenancenow":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_ffhandler(command, "maintenance_c");

                        break;


                    case "ffupdates":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_ffhandler(command, "updates");

                        break;

                    case "ffnotices":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_ffhandler(command, "notices");

                        break;

                    case "a_set_answer":

                        if (!isAdmin) { error = 1; goto default; }
                        await Command_a_set_answer(command);

                        break;

                    case "a_set_log":

                        if (!isAdmin) { error = 1; goto default; }
                        await Command_a_set_log(command);

                        break;

                    case "a_set_ffnews":

                        if (!isAdmin) { error = 1; goto default; }

                        await Command_a_set_topics(command);

                        break;

                    case "timestamp":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_timestamp(command);

                        break;

                    case "searchmount":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_searchmount(command);

                        break;

                    case "searchminion":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_searchminion(command);

                        break;

                    case "searchoutfit":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_searchoutfit(command);

                        break;

                    case "a_show_stored":
                        if (!isAdmin) { error = 1; goto default; }
                        await Command_a_show_stored(command);
                        break;

                    case "a_sendmsg":
                        if (!isAdmin) { error = 1; goto default; }
                        await Command_a_sendmsg(command);
                        break;

                    case "a_react":

                        if (!isAdmin) { error = 1; goto default; }
                        await Command_a_react(command);

                        break;

                    case "a_nikname":

                        if (!isAdmin) { error = 1; goto default; }
                        if (command.User.IsBot) { error = 2; goto default; } //don't change bots ¬¬

                        await Command_a_nikname(command);

                        break;

                    case "a_log":
                        if (!isAdmin) { error = 1; goto default; }
                        await Command_a_log(command);

                        break;

                    case "cacpot":
                        if (!canTalk) { error = 3; goto default; }
                        await Command_cacpot(command);

                        break;

                    case "botinfo":
                        if (!canTalk) { error = 3; goto default; }
                        await Command_botinfo(command);
                        break;

                    case "addword":
                        if (!isAdmin) { error = 1; goto default; }
                        await Command_addword(command);
                        break;

                    case "removeword":
                        if (!isAdmin) { error = 1; goto default; }
                        await Command_removeword(command);
                        break;
                    
                    case "badwordslist":
                        if (!isAdmin) { error = 1; goto default; }
                        await Command_badwordslist(command);
                        break; 
                    /*
                    */

                    //////////////////////////    Errors
                    default:

                        string part1 = "", part2 = "";

                        switch (error)
                        {
                            case 1:
                                part1 = StringT.Err1_p1;
                                part2 = StringT.Err1_p2;
                                break;

                            case 2:
                                part1 = StringT.Err2_p1;
                                part2 = StringT.Err2_p2;
                                break;

                            case 3:
                                part1 = StringT.Err3_p1;
                                part2 = StringT.Err3_p2;
                                break;

                            default:
                                part1 = StringT.Errd_p1;
                                part2 = StringT.Errd_p2;
                                break;
                        }
                        var def_emb = new EmbedBuilder()
                            .WithTitle($"Zeno♥.")
                            .WithDescription(StringT.Desc_com1)
                            .WithColor(Color.Red)
                            .AddField(part1, part2, false)
                            .Build();
                        var m = await command.FollowupAsync("", embed: def_emb, ephemeral: true);
                        BorrarMsg(m);
                        break;
                }

            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }


    }
}

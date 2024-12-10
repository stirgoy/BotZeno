using Discord.WebSocket;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        /////////////////////////////////////////
        /////////////    Command Handler
        /////////////////////////////////////////
        private async Task SlashCommandHandlerAsync(SocketSlashCommand command)
        {

            //for conditional use            
            bool isAdmin = (command.User as SocketGuildUser).GuildPermissions.Administrator; //actually useless, hadled by bot
            bool canTalk = Check_Allowed_Channel(command.Channel);
            int error = 0;

            try
            {
                switch (command.CommandName)
                {

                    case "a_commands":

                        if (!isAdmin) { error = 1; goto default; }
                        if (!canTalk) { error = 3; goto default; }
                        await Command_a_commands(command);
                        break;

                    case "timers":

                        if (!canTalk) { error = 3; goto default; }
                        await Command_timers(command);

                        break;


                    case "a_answer":

                        if (!isAdmin) { error = 1; goto default; }
                        await Command_a_answer(command);

                        break;


                    case "a_userinfo":

                        var user_n = command.Data.Options.FirstOrDefault(opt => opt.Name == "name");
                        if (user_n?.Value is IUser user_name) { } else { goto default; }
                        if (!isAdmin) { error = 1; goto default; }
                        if (!canTalk) { error = 3; goto default; }
                        if (user_name.IsBot) { error = 2; goto default; } //don't check bots ¬¬
                        await Command_a_userinfo(command, user_name);

                        break;

                    case "ffnews":

                        //maintenance
                        //news
                        //status
                        //updates
                        if (!canTalk) { error = 3; goto default; }
                        await FFXIVModeHandler(command, "news");

                        break;


                    case "ffstatus":

                        if (!canTalk) { error = 3; goto default; }
                        await FFXIVModeHandler(command, "status");

                        break;

                    case "ffmaintenance":

                        if (!canTalk) { error = 3; goto default; }
                        await FFXIVModeHandler(command, "maintenance");

                        break;

                    case "ffmaintenancenow":

                        if (!canTalk) { error = 3; goto default; }
                        await FFXIVModeHandler(command, "maintenance_c");

                        break;


                    case "ffupdates":

                        if (!canTalk) { error = 3; goto default; }
                        await FFXIVModeHandler(command, "updates");

                        break;

                    case "ffnotices":

                        if (!canTalk) { error = 3; goto default; }
                        await FFXIVModeHandler(command, "notices");

                        break;

                    case "a_set_answer":

                        if (!isAdmin) { error = 1; goto default; }
                        var channel = command.Data.Options.FirstOrDefault(opt => opt.Name == "channel");
                        if (channel?.Value is SocketChannel selectedChannel) { } else { error = 3; goto default; }//exit on fail
                        await Command_a_set_answer(command, selectedChannel);

                        break;

                    case "a_set_log":

                        if (!isAdmin) { error = 1; goto default; }
                        var channelL = command.Data.Options.FirstOrDefault(opt => opt.Name == "channell");
                        if (channelL?.Value is SocketChannel selectedChannelL) { } else { error = 3; goto default; }//exit on fail
                        await Command_a_set_log(command, selectedChannelL);

                        break;

                    case "a_set_news":

                        if (!isAdmin) { error = 1; goto default; }
                        var channelNc = command.Data.Options.FirstOrDefault(opt => opt.Name == "newsc");
                        if (channelNc?.Value is SocketChannel selectedChannelNc) { } else { error = 3; goto default; }//exit on fail
                        await Command_a_set_news(command, selectedChannelNc);

                        break;

                    case "a_set_update":

                        if (!isAdmin) { error = 1; goto default; }
                        var channelUc = command.Data.Options.FirstOrDefault(opt => opt.Name == "updatec");
                        if (channelUc?.Value is SocketChannel selectedChannelUc) { } else { error = 3; goto default; }//exit on fail
                        await Command_a_set_update(command, selectedChannelUc);

                        break;

                    case "a_set_status":

                        if (!isAdmin) { error = 1; goto default; }
                        var channelSc = command.Data.Options.FirstOrDefault(opt => opt.Name == "statusc");
                        if (channelSc?.Value is SocketChannel selectedChannelSc) { } else { error = 3; goto default; }//exit on fail
                        await Command_a_set_status(command, selectedChannelSc);

                        break;

                    case "a_set_maintenance":

                        if (!isAdmin) { error = 1; goto default; }
                        var channelMc = command.Data.Options.FirstOrDefault(opt => opt.Name == "maintenancec");
                        if (channelMc?.Value is SocketChannel selectedChannelMc) { } else { error = 3; goto default; }//exit on fail
                        await Command_a_set_maintenance(command, selectedChannelMc);

                        break;

                    case "timestamp":

                        var msg = command.Data.Options.FirstOrDefault(opt => opt.Name == "time");
                        if (msg?.Value is string user_msg) { } else { goto default; }//exit on fail
                        if (!canTalk) { error = 3; goto default; }
                        await Command_timestamp(command, user_msg);

                        break;

                    case "searchmount":

                        var mount = command.Data.Options.FirstOrDefault(opt => opt.Name == "mount");
                        if (mount?.Value is string usr_mount) { } else { goto default; }//exit on fail
                        if (!canTalk) { error = 3; goto default; }
                        await Command_searchmount(usr_mount, command);

                        break;

                    case "searchminion":

                        var minion = command.Data.Options.FirstOrDefault(opt => opt.Name == "minion");
                        if (minion?.Value is string usr_minion) { } else { goto default; }//exit on fail
                        if (!canTalk) { error = 3; goto default; }
                        await Command_searchminion(usr_minion, command);

                        break;

                    case "a_show_stored":
                        if (!isAdmin) { error = 1; goto default; }
                        await Command_a_show_stored(command);
                        break;

                    case "a_sendmsg":
                        if (!isAdmin) { error = 1; goto default; }
                        var ttt = command.Data.Options.FirstOrDefault(opt => opt.Name == "title");
                        var mmm = command.Data.Options.FirstOrDefault(opt => opt.Name == "msg");
                        var ppp = command.Data.Options.FirstOrDefault(opt => opt.Name == "picture");
                        var ccc = command.Data.Options.FirstOrDefault(opt => opt.Name == "channel");

                        if (ttt?.Value is string tt) { } else { goto default; }//exit on fail
                        if (mmm?.Value is string mm) { } else { goto default; }//exit on fail 
                        if (ppp?.Value is string pp) { } else { pp = ""; }//exit on fail ????????? no lmao
                        if (ccc?.Value is SocketTextChannel cc) { } else { cc = null; }//exit on fail ????????? no lmao

                        await Command_a_sendmsg(command, tt, mm, pp, cc);
                        break;

                    case "a_set_notices":

                        if (!isAdmin) { error = 1; goto default; }
                        var channelN = command.Data.Options.FirstOrDefault(opt => opt.Name == "notices");
                        if (channelN?.Value is SocketChannel selectedChannelN) { } else { error = 3; goto default; }//exit on fail
                        await Command_a_set_notices(command, selectedChannelN);

                        break;

                    case "a_react":

                        if (!isAdmin) { error = 1; goto default; }
                        var mesglink = command.Data.Options.FirstOrDefault(opt => opt.Name == "message_link");
                        var emote = command.Data.Options.FirstOrDefault(opt => opt.Name == "emote");
                        if (mesglink?.Value is string mesglink1) { } else { error = 3; goto default; }//exit on fail
                        if (emote?.Value is string emote1) { } else { error = 3; goto default; }//exit on fail
                        await Command_a_react(command, mesglink1, emote1);

                        break;

                    //////////////////////////    Errors
                    default:

                        string part1 = "", part2 = "";

                        switch (error)
                        {
                            case 1:
                                part1 = StringT.Err1_p1;
                                part2 = StringT.Err1_p2;
                                //var t = Kuru.GetTextChannel(command.Channel.Id);
                                //await ZenoLog($"{command.User.Mention} try to use  /{command.CommandName} command on {t.Mention}, but is not admin, i say nothing!!");
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

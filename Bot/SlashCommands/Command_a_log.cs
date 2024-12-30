using Discord;
using Discord.WebSocket;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        async Task Command_a_log(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: true);
            Embed answer=null;

            var log_t = command.Data.Options.FirstOrDefault(opt => opt.Name == "logname");
            if (log_t?.Value is string logname) { } else { logname = ""; }
            string logpath = $"{Path}\\Logs";
            bool isLog = false;
            List<string> text = new List<string>();


            if (logname == "")
            {
                // no logname
                if (!Directory.Exists(logpath))
                {
                    //no log folder
                    Directory.CreateDirectory(logpath);
                }

                if (Directory.GetFiles(logpath).Count() == 0)
                {
                    //no logs
                    answer = new EmbedBuilder()
                        .WithTitle("I found no logs " + Emote.Bot.Sadtuff)
                        .WithColor(Color.Orange)
                        .Build();
                }
                else
                {
                    //have logs
                    string desc = "";
                    string last = Directory.GetFiles(logpath).Last();
                    foreach (string item in Directory.GetFiles(logpath))
                    {
                        desc += System.IO.Path.GetFileName(item);
                        if (item != last)
                        {
                            desc += NL;
                        }
                    }

                    answer = new EmbedBuilder()
                        .WithTitle("I have this logs:" + Emote.Bot.Happytuff)
                        .WithDescription(desc)
                        .WithColor(Color.Orange)
                        .Build();
                }

            }
            else
            {
                //with logname
                string newpath = System.IO.Path.Combine(logpath, logname);
                if (File.Exists(newpath))
                {
                    isLog = true;
                    string[] lines = File.ReadAllLines(newpath);
                    string temp = "";

                    foreach (var item in lines)
                    {
                        if (temp.Length + item.Length > 2000)
                        {
                            text.Add(temp);
                            temp = "";
                        }
                        else
                        {
                            temp += item + NL;
                        }
                    }

                    text.Add(temp);

                    //2k charas
                }
                else
                {
                    answer = new EmbedBuilder()
                        .WithTitle("I found nothing with " + logname + " " + Emote.Bot.Sadtuff)
                        .WithColor(Color.Orange)
                        .Build();
                }
            }

            if (isLog)
            {
                foreach (var item in text)
                {
                    await command.FollowupAsync(item, ephemeral: true);
                }
            }
            else
            {
                await command.FollowupAsync("", embed:answer, ephemeral: true);
            }

        }
    }
}


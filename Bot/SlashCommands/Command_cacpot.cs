using Discord;
using Discord.WebSocket;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        internal async Task Command_cacpot(SocketSlashCommand command)
        {
            await command.DeferAsync(ephemeral: true);

            Embed emb;
            if (!Config.CacpotIds.Contains(command.User.Id.ToString()))
            {
                //add
                StringCollection temp = Config.CacpotIds;
                if (temp.Count == 0)
                {
                    temp = new StringCollection() { command.User.Id.ToString() };
                }
                else
                {
                    temp.Add(command.User.Id.ToString());
                }

                Config.CacpotIds = temp;
                await Config_Save();
                Print($"{command.User.GlobalName} added to cacpot list");

                emb = CreateEmbed(StringT.Embed_add_cuctar_t, StringT.Embed_add_cuctar_d, miniImage: StringT.Embed_add_cuctar_url, color: Color.Red);
                
            }
            else
            {
                //del
                StringCollection temp = Config.CacpotIds;
                int indx = 0;
                int cont = 0;

                foreach (string id in temp)
                {
                    if (id == command.User.Id.ToString()) { indx = cont; break; }
                    cont++;
                }

                temp.RemoveAt(indx);
                Config.CacpotIds = temp;
                await Config_Save();
                Print($"{command.User.GlobalName} removed from cacpot list");

                emb = CreateEmbed(StringT.Embed_remove_cuctar_t, StringT.Embed_remove_cuctar_d, miniImage: StringT.Embed_remove_cuctar_url,color: Color.Green);

            }

            await command.FollowupAsync("", embed: emb, ephemeral: true);

        }
    }
}

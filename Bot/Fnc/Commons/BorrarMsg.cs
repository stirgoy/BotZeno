using Discord;
using Discord.Rest;
using System;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /********************
            borrar_msg
        *//////////////////// for delete bot messages over time
        private static void BorrarMsg(dynamic botMessage, int tiempo = 8)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(tiempo * 1000);
                try { await botMessage.DeleteAsync(); } catch (Exception ex) { Print(ex.Message); }
            });
        }
        
    }
}

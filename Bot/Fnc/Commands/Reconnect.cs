using System;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /********************
            Reconnect
        *//////////////////// client reconnection
        private static async void Reconnect()
        {
            try
            {
                await Bot_Zeno.StopAsync();
                await Task.Delay(5000);
                await Bot_Zeno.StartAsync();
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }
    }
}

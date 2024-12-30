using System;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private async Task ClientDisconected(Exception exception)
        {
            try
            {
                //this should reset app on each discord reconnection reply
                Reconnections++;
                if (Reconnections >= Config.MaxReconnections)
                {
                    Print("RESETING ZENO EXE!");
                    ResetApp();
                }
                else
                {
                    Print("Server requested a reconnect...");
                    await Task.Delay(200);
                }

                /*
                if (exception is GatewayReconnectException)
                {
                }
                */
            }
            catch (Exception ex)
            {
                Print(ex.Message);

            }
        }
    }
}

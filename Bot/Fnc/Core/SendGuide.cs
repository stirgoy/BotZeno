using Discord;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
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
    }
}

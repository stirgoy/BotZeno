using System;

namespace Zeno
{
    //function
    internal partial class Program
    {
        internal static string EorzeaTime(string format = "HH,mm") => DateTime.Now.ToEorzeaTime().ToString(format);
    }

    //DateTime extension
    public static class EorzeaDateTimeExtention
    {
        public static DateTime ToEorzeaTime(this DateTime date)
        {
            const double EORZEA_MULTIPLIER = 3600D / 175D;

            // Calculate how many ticks have elapsed since 1/1/1970
            long epochTicks = date.ToUniversalTime().Ticks - (new DateTime(1970, 1, 1).Ticks);

            // Multiply those ticks by the Eorzea multipler (approx 20.5x)
            long eorzeaTicks = (long)Math.Round(epochTicks * EORZEA_MULTIPLIER);

            return new DateTime(eorzeaTicks);
        }
    }
}

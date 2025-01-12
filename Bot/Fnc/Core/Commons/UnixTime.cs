using System;

namespace Zeno
{
    internal partial class Program
    {
        /********************
                UnixTime
        *//////////////////// returns cord timestamp
        private static string UnixTime(DateTime date, string mode = "R")
        {
            long unixTimestamp = new DateTimeOffset(date).ToUnixTimeSeconds();
            return $"<t:{unixTimestamp}:{mode}>";

        }
    }
}

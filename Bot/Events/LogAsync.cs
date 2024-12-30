using Discord;
using System;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        /********************
                LogAsync
        *////////////////////
        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
    }
}

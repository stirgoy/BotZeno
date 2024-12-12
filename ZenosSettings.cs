namespace Zeno
{
    internal partial class Program
    {
        //Bot Dev Settings
        readonly bool _updateSlashCommands = true; //slash commands server update
        static readonly bool _consolePrint = true; //console log
        readonly bool _ZenoLog = true; //zeno log is kuru log

#if DEBUG
        static bool XIV_LN_enabled = false;
#else
        static bool XIV_LN_enabled = true;
#endif

    }
}

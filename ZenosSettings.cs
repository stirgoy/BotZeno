using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begu
{
    internal partial class Program
    {
        //Bot Dev Settings
        readonly bool _updateSlashCommands = false; //slash commands server update
        readonly bool _consolePrint = false; //console log
        readonly bool _ZenoLog = true; //zeno log is kuru log

        //news updater
        readonly bool _ffNewsUpdater = true; //enabled? handles property log
        bool _ffNewsUpdaterPrepareIDS = false; //Shows 1st news and stoere ids!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!   SET FALSE NEXT 1st RUN   !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        readonly int _ffNewsUpdaterTimer = 5; //minutes. they update each 10...
        readonly bool _keeptrying = false; //ignores the 5 reconnect trys *will reconect every chech time instead some secs need revise
        readonly bool _silentTrys = true; //console log


    }
}

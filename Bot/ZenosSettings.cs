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
        readonly bool _updateSlashCommands = true; //slash commands server update
        readonly bool _consolePrint = true; //console log
        readonly bool _ZenosLog = false; //zenos log enabled
        //news updater
        readonly bool _ffNewsUpdater = false; //enabled?
        readonly int _ffNewsUpdaterTimer = 5; //minutes
    }
}

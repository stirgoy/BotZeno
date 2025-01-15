using System;
using System.Collections.Specialized;

namespace Zeno
{
    internal partial class Program
    {
        /********************
            AddTalkChannel
        *////////////////////
        private async void AddTalkChannel(String channel)
        {
            //Add allowed talk channel
            StringCollection channels = Config.Channels.TalkChannel;
            if (channels == null) channels = new StringCollection();
            channels.Add(channel);
            Config.Channels.TalkChannel = channels;
            await Config_Save();
        }
    }
}

using System;
using System.Collections.Specialized;

namespace Zeno
{
    internal partial class Program
    {
        /********************
            RemoveTalkChannel
        *////////////////////
        private void RemoveTalkChannel(String channel)
        {
            //Remove allowed talk channel
            StringCollection channels = Config.Channels.TalkChannel;
            StringCollection new_channels = new StringCollection();
            foreach (var item in channels)
            {
                if (item != channel)
                {
                    new_channels.Add(item);
                }
            }

            Config.Channels.TalkChannel = new_channels;
            Config_Save();
        }
    }
}

namespace Zeno
{
    internal partial class Program
    {
        /********************
                GetEvent
        *////////////////////
        private static string[] GetEvent(string event_id)
        {
            foreach (string[] item in Config.Events_Noticed)
            {
                if (event_id == item[0])
                {
                    return item;
                }
            }

            return new string[0];
        }
    }
}

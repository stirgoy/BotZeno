using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Zeno
{
    internal partial class Program
    {
        private static Task Config_Save()
        {
            string js = JsonConvert.SerializeObject(Config, Formatting.Indented);
            File.WriteAllText($"{Path}\\{Json_file}", js);
            return Task.CompletedTask;
        }
    }
}

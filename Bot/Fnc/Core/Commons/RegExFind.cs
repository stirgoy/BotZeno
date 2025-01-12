using System.Text.RegularExpressions;

namespace Zeno
{
    internal partial class Program
    {
        /********************
            RegExFind
        *//////////////////// 
        bool RegExFind(string[] constant, string[] variable, string text)
        {
            string constantchain = string.Join("|", constant); // Une las palabras fijas con "|"
            string variablechain = string.Join("|", variable); // Une las palabras opcionales con "|"
            string patron = $@"\b({constantchain})\b.*\b({variablechain})\b|\b({variablechain})\b.*\b({constantchain})\b";
            Regex regex = new Regex(patron, RegexOptions.IgnoreCase);
            return regex.IsMatch(text);
        }
    }
}

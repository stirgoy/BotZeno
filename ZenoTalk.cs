namespace Zeno
{
    internal partial class Program
    {

        internal static class ZenoTalk
        {
            internal static string[] Greetings { get => new string[] { "zeno", "zenos", "zeno♥", "zenos♥", Bot_Zeno.CurrentUser.Id.ToString(), "wind-up", "friend", "enemy", "hello", "hey", "hi" }; }
            internal static string[] Help { get => new string[] { "help", "guide", "guides", "tutorial" }; }
            internal static string[] Macros { get => new string[] { "macro", "macros" }; } 
            internal static string[] Menu { get => new string[] { "macro-menu", "menu" }; } 
            internal static string[] Retainers { get => new string[] { "retainer", "retiners", "venture", "ventures" }; }
            internal static string[] Maintenance { get => new string[] { "maintenance", "update", "patch" }; }
            internal static string[] GameFF { get => new string[] { "game", "ff", "xiv", "ffxiv" }; }

            private static string DescHelp { get => $"_We can talk about:_ **Game maintenance**{NL}-# This still on work, i will add more things when i have time :)"; }
            //private static string DescHelp { get => $"**_Guides:_ Macros**{NL}**_Talks:_ Game Maintenance**"; }

            internal static string[] answer_help
            {
                get => new string[] {
                    "Hey {0}! With what i can try help you?{1}" + DescHelp,
                    "{0} my _friendnemy_!!! You needs something?{1}" + DescHelp,
                    $"{Emote.Bot.Happytuff} {{0}} {Emote.Bot.Happytuff} Can i give a hand?{{1}}{DescHelp}"
                };
            }


            internal static string[] answer_macro
            {
                get => new string[] {
                    $"I will send you the guide via **direct message**, then i will close our **direct message**{NL}I found this about Macros:{NL}Personalized **Menu** with macros"
                };
            }

            internal static string[] answer_menu
            {
                get => new string[] {
                    $"Ok i going send you the guide... humm... this one have 8 parts {Emote.Bot.Boss}",
                    $"So you want build a macro menu {Emote.Bot.Happytuff}. Sending the guide on our DM channel {Emote.Bot.Pepo_laugh}"
                };
            }

            internal static string[] answer_retainer
            {
                get => new string[] {
                    "Actually have nothing to show",
                    "Lulure is sooooo laaazzyyyy",
                    "im falling sleep ZZzz.._"
                };
            }

            internal static string[] answer_greetings
            {
                get => new string[] {
                    $"Hi hiiii {{0}}I hope you having a good day!! i going go fishing right now!!!",
                    ":D {0} Remember!! whe you gets a __stack marker__ and runs to party you are maximizing damage " + Emote.Bot.Happytuff + NL +  "-# __**to the party**__",
                    $"{Emote.Bot.Pepeshookt} La-hee ♫ ♪ Don huera ♫ evi foh ♪ la hee ♫ ♪"
                };
            }

        }
    }
}

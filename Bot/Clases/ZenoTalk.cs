namespace Zeno
{
    internal partial class Program
    {

        internal static class ZenoTalk
        {
            internal static string[] Greetings { get => new string[] { "potato", "zeno", "zenos", "zeno♥", "zenos♥", Bot_Zeno.CurrentUser.Id.ToString(), "wind-up", "friend", "enemy", "hello", "hey", "hi" }; }
            internal static string[] Help { get => new string[] { "help", "guide", "guides", "tutorial" }; }
            internal static string[] Yw { get => new string[] { "thanks", "ty", "thank" }; }
            internal static string[] Macros { get => new string[] { "macro", "macros" }; }
            internal static string[] Retainers { get => new string[] { "retainer", "retainers" }; }
            internal static string[] Basic { get => new string[] { "melds", "meld", "basics", "basic" }; }
            internal static string[] Maintenance { get => new string[] { "maintenance" }; }
            internal static string[] Menu { get => new string[] { "menu" }; }
            internal static string[] Useful { get => new string[] { "useful" }; }
            internal static string[] Hud { get => new string[] { "hud" }; }
            internal static string[] GameFF { get => new string[] { "game", "ff", "xiv", "ffxiv", "help" }; }


            private static string DescHelp { get => $"_I can **help** with:_ {NL}* FFXIV game **maintenance** sheudles{NL}* **Macros**{NL}* **Retainers**{NL}-# This still on work, i will add more things when i have time :)"; }
            //private static string DescHelp { get => $"**_Guides:_ Macros**{NL}**_Talks:_ Game Maintenance**"; }

            internal static string[] Answer_help
            {
                get => new string[] {
                    "Hey {0}! With what i can try **help** you?{1}" + DescHelp,
                    "{0} My _friendnemy_!!! You needs a **guide** about FFXIV?{1}" + DescHelp,
                    $"{Emote.Bot.Happytuff} {{0}} {Emote.Bot.Happytuff} Can i give a **tutorial**?{{1}}{DescHelp}"
                };
            }

            internal static string[] Answer_greetings
            {
                get => new string[] {
                    $"Hi hiiii {{0}}I hope you having a good day!! i going go fishing right now!!! Or you needs some **help**?",
                    Emote.Bot.Happytuff + " {0} Remember!! when you gets a __stack marker__ and runs to your party you are maximizing damage " + Emote.Bot.Happytuff + NL +  $"-# __**to the party**__ {Emote.Bot.Boss}",
                    $"{Emote.Bot.Pepeshookt} La-hee ♫ ♪ Don huera ♫ evi foh ♪ la hee ♫ ♪. Some day i will do a **tutorial** for sing that song!"
                };
            }

            internal static string[] Answer_yw
            {
                get => new string[] {
                    $"You are welcome {{0}}!!!{NL}I'm happy to help {Emote.Bot.Sproud}.",
                    $"Don't worry {{0}} my _friendnemy_ you owe me one now {NL+Emote.Bot.Pepelove}"
                };
            }

            internal static string[] Answer_macro
            {
                get => new string[] {
                    $"__I will send you the guide via **direct message**, then i will close our **direct message**__{NL}I found this about **Macros**:{NL}* Personalized **Menu** with macros{NL}* **Useful** macros{NL}* **HUD** Basics"
                };
            }


            internal static string[] Answer_menu
            {
                get => new string[] {
                    $"Ok i going send you the guide... humm... this one have 8 parts {Emote.Bot.Boss}",
                    $"So you want build a macro menu {Emote.Bot.Happytuff}. Sending the guide on our DM channel {Emote.Bot.Pepo_laugh}"
                };
            }

            internal static string[] Answer_useful
            {
                get => new string[] {
                    $"Ok i going send you the guide... humm... this one have 5 parts {Emote.Bot.Boss}",
                    $"I hope this macros help you {Emote.Bot.Happytuff}. Sending the guide on our DM channel {Emote.Bot.Pepo_laugh}"
                };
            }

            internal static string[] Answer_hud
            {
                get => new string[] {
                    $"Ok i going send you the guide... humm... this one have 4 parts {Emote.Bot.Boss}",
                    $"Editing HUD huh {Emote.Bot.Happytuff}. Sending the guide on our DM channel {Emote.Bot.Pepo_laugh}"
                };
            }

            internal static string[] Answer_retainer
            {
                get => new string[] {
                    $"__I will send you the guide via **direct message**, then i will close our **direct message**__{NL}I found this about **Retainers**:{NL}* Retainer **basics** and **melds**"

                };
            }

            internal static string[] Answer_retainerBasics
            {
                get => new string[] {
                    $"Ok i going send you the guide... humm... this one have 2 parts {Emote.Bot.Boss}",
                    $"Retainers are good farmers {Emote.Bot.Happytuff}. Sending the guide on our DM channel {Emote.Bot.Pepo_laugh}"
                };
            }



        }
    }
}

using System.Collections.Generic;

namespace Zeno
{
    internal static class ClassRoom
    {
        internal static class Macros
        {
            internal static readonly List<string> Menu = new List<string> {
                "There is a way to make your own personalized menu for jobs, settings, emotes, macros, etc, using:\r\n- 2 hotbars\r\n- Some macros\r\n- Jobs without soulstone (for store menus)\r\n\r\nHelps to have screen more clean and have more free hotbars.\r\n\r\n:warning: Game don't let you run more than one macro at same time, when this happens game cancel your current macro and run the new one. :warning:\r\n:warning: If you running a crafting macro and uses the menu YOU going cancel the crafting macro :warning:\r\nYou can use `/macrolock` on your crafting macros for prevent it.\r\n\r\n(Recomended hide unassigned slots.)",
                "https://i.postimg.cc/ZR1ntgmV/menugif.gif",
                "## _Part 1_\r\n# Understanding\r\nBasicaly we use one shared hotbar as main menu for store macros, on this macros we going handle the other hotbar that need be non-shared hotbar.\r\nThis menu works making a copy of hotbars, so we need make this hotbar on our useless job.\r\nFor this we going use jobs without stones for save the menus on it hotbars.\r\n\r\n_On next GIF you can see how is copying hotbars from lancer job._",
                "https://i.postimg.cc/2SmqSPZt/muestra-menu.gif",
                "## _Part 2_\r\n#  Setting hotbars\r\nBefore starting be sure you knows what hotbars you going use and remember what number is each. (you can see on `Main menu > System > HUD Layout` or type `/hudlayout`).\r\nOn this guide i going use hotbars 5 and 8.\r\n\r\nNext is go to `Main menu > System > Character Configuration > Hotbar Settings > Sharing`.\r\nHere set your static menu bar as shared and variable menu unshared.\r\n\r\n__ _(If your variable menu is shared hotbar, the copy macro it won't work)_ __",
                "https://i.postimg.cc/x1RXJtG1/ffxiv-dx11-m-Vo-Idrbt4-C.png",
                ".\r\nWith this done you can start filling the hotbars of any job with no stone.\r\nFor this just remove the Soulstone from character menu.\r\nIf you can't can be for 2 reasons:\r\n- This job have no class xD\r\n- Your weapon is only for a specific job.\r\n\r\n## _Part 3_\r\n# Macros\r\nWe can put this macros on both menus, but all need __edit only the variable menu__.\r\nThe comand we going use is `/hotbar copy FROM X TO Y`\r\n- FROM = Job that have the hotbar to copy.\r\n- X      = Number of the hotbar to copy.\r\n- TO   = Job where paste, we going use the keyword `current` that uses current job.\r\n- Y      = Number of hotbar where paste. This should be ever variable menu.\r\n\r\n_Example_\r\n_Copying pugilist hotbar 3 to your current job hotbar 8_\r\n```\r\n/hotbar copy PGL 3 current 8\r\n```\r\n\r\nIn addition we can add a timer for wait some seconds then clear the hotbar for have the menu \"closed\"\r\n_ Game don't let you run more than one macro at each time, so game cancel your current macro and run the new one _\r\n_example_\r\n```\r\n/hotbar copy PGL 3 current 8\r\n/wait 5\r\n/hotbar remove 8 all\r\n```\r\n\r\nOr we can hide the hotbar and show each time we need, but this add one more line to all menu macros because we need show again.\r\n```\r\n/hotbar display 8 on\r\n/hotbar copy PGL 3 current 8\r\n/wait 5\r\n/hotbar display 8 off\r\n```",
                "https://i.postimg.cc/W3DzyVBd/mex.png"
            };



            internal static class MacroMenu
            {
                internal static string Part_1 { get => "There is a way to make your own personalized menu for jobs, settings, emotes, macros, etc, using:\r\n- 2 hotbars\r\n- Some macros\r\n- Jobs without soulstone (for store menus)\r\n\r\nHelps to have screen more clean and have more free hotbars.\r\n\r\n:warning: Game don't let you run more than one macro at same time, when this happens game cancel your current macro and run the new one. :warning:\r\n:warning: If you running a crafting macro and uses the menu YOU going cancel the crafting macro :warning:\r\nYou can use `/macrolock` on your crafting macros for prevent it.\r\n\r\n(Recomended hide unassigned slots.)"; }
                internal static string Part_2 { get => "https://i.postimg.cc/ZR1ntgmV/menugif.gif"; }
                internal static string Part_3 { get => "## _Part 1_\r\n# Understanding\r\nBasicaly we use one shared hotbar as main menu for store macros, on this macros we going handle the other hotbar that need be non-shared hotbar.\r\nThis menu works making a copy of hotbars, so we need make this hotbar on our useless job.\r\nFor this we going use jobs without stones for save the menus on it hotbars.\r\n\r\n_On next GIF you can see how is copying hotbars from lancer job._"; }
                internal static string Part_4 { get => "https://i.postimg.cc/2SmqSPZt/muestra-menu.gif"; }
                internal static string Part_5 { get => "## _Part 2_\r\n#  Setting hotbars\r\nBefore starting be sure you knows what hotbars you going use and remember what number is each. (you can see on `Main menu > System > HUD Layout` or type `/hudlayout`).\r\nOn this guide i going use hotbars 5 and 8.\r\n\r\nNext is go to `Main menu > System > Character Configuration > Hotbar Settings > Sharing`.\r\nHere set your static menu bar as shared and variable menu unshared.\r\n\r\n__ _(If your variable menu is shared hotbar, the copy macro it won't work)_ __"; }
                internal static string Part_6 { get => "https://i.postimg.cc/x1RXJtG1/ffxiv-dx11-m-Vo-Idrbt4-C.png"; }
                internal static string Part_7 { get => ".\r\nWith this done you can start filling the hotbars of any job with no stone.\r\nFor this just remove the Soulstone from character menu.\r\nIf you can't can be for 2 reasons:\r\n- This job have no class xD\r\n- Your weapon is only for a specific job.\r\n\r\n## _Part 3_\r\n# Macros\r\nWe can put this macros on both menus, but all need __edit only the variable menu__.\r\nThe comand we going use is `/hotbar copy FROM X TO Y`\r\n- FROM = Job that have the hotbar to copy.\r\n- X      = Number of the hotbar to copy.\r\n- TO   = Job where paste, we going use the keyword `current` that uses current job.\r\n- Y      = Number of hotbar where paste. This should be ever variable menu.\r\n\r\n_Example_\r\n_Copying pugilist hotbar 3 to your current job hotbar 8_\r\n```\r\n/hotbar copy PGL 3 current 8\r\n```\r\n\r\nIn addition we can add a timer for wait some seconds then clear the hotbar for have the menu \"closed\"\r\n_ Game don't let you run more than one macro at each time, so game cancel your current macro and run the new one _\r\n_example_\r\n```\r\n/hotbar copy PGL 3 current 8\r\n/wait 5\r\n/hotbar remove 8 all\r\n```\r\n\r\nOr we can hide the hotbar and show each time we need, but this add one more line to all menu macros because we need show again.\r\n```\r\n/hotbar display 8 on\r\n/hotbar copy PGL 3 current 8\r\n/wait 5\r\n/hotbar display 8 off\r\n```"; }
                internal static string Part_8 { get => "https://i.postimg.cc/W3DzyVBd/mex.png"; }


            }
        }
    }
}


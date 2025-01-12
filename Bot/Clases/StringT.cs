using System;

namespace Zeno
{
    public static class StringT
    {
        private static string NL => Environment.NewLine;

        //embed errors
        public static string Err1_p1 => "What you trying?";
        public static string Err1_p2 => "/YouNotAdmin";
        public static string Err2_p1 => "I don't understand...";
        public static string Err2_p2 => "/WhatYouSendMe?";
        public static string Err3_p1 => "Im not allowed to talk here...";
        public static string Err3_p2 => "/NotForZeno♥";
        public static string Errd_p1 => "Easter egg? out of limits";
        public static string Errd_p2 => "This never should happen.... gj xD";
        public static string Desc_com1 => $"{Emote.Bot.Sproud} Because you looks lost. {Emote.Bot.Sproud}";


        //slash command builder
        public static string Scd_a_commands => "Shows admin commands!";
        public static string Scd_timers => "Shows Eorzean timers.";
        public static string Scd_a_answer => "Shows my allowed answer channels for all users.";
        public static string Scd_a_userinfo => "I bring server information from a specific user.";
        public static string Scd_ffnews => "Shows Lodestone news.";
        public static string Scd_ffstatus => "Shows Lodestone status news.";
        public static string Scd_ffupdates => "Shows Lodestone updates news.";
        public static string Scd_ffmaintenence => "Shows FFXIV maintenance news.";
        public static string Scd_ffnotices => "Shows FFXIV notices news.";
        public static string Scd_ffmaintenencenow => "Shows current FFXIV game maintenance staus.";
        public static string Scd_desc_hmn => "Number of news i try get";
        public static string Scd_a_set_answer => "Edits my answer channels, if channel is already added i will remove ^^";
        public static string Scd_a_set_log => "Edits my log channel, only can be one channel";
        public static string Scd_a_set_ffnews => "Edits my ff news channel, only can be one channel";
        public static string Scd_a_set_ffupdate => "Edits my ff updates channel, only can be one channel";
        public static string Scd_a_set_ffstatus => "Edits my ff status channel, only can be one channel";
        public static string Scd_a_set_ffnotices => "\"Edits my ff notices channel, only can be one channel";
        public static string Scd_a_set_ffmainenance => "Edits my ff mainenance channel, only can be one channel";
        public static string Scd_timestamp => "I bring you a dynamic display of date time and discord markdown code ♥";
        public static string Scd_timestamp_d => "Text a date time. format: YYYY-MM-DD HH:mm:ss, can be only the hour.";
        public static string Scd_searchmount => "I try search information about ffxiv mount";
        public static string Scd_searchmount_d => "Mount name.";
        public static string Scd_searchminion => "I try search information about ffxiv minion";
        public static string Scd_searchminion_d => "Minion name.";
        public static string Scd_a_show_stored => "I show all stored data.";
        public static string Scd_a_sendmsg => "I send an embeded message.";
        public static string Scd_a_sendmsg_d1 => "Title of embed.";
        public static string Scd_a_sendmsg_d2 => "The message you want show, use \\n for add new line.";
        public static string Scd_a_sendmsg_d3 => "Url picture.";
        public static string Scd_a_sendmsg_d4 => "Where i post the embed.";
        public static string Scd_a_sendmsg_d5 => "Adds FFXIV roles to footer and change Zenos picture for Server picture.";
        public static string Scd_a_react => "Wind-up Zeno♥ react to a message";
        public static string Scd_a_react_d1 => "Link of message(you can get with left click)";
        public static string Scd_a_react_d2 => "Emote for react";
        public static string Scd_ctt_d => "Choose a text channel";
        public static string Scd_usermention => "User mention.";
        public static string Scd_a_nikname => "Changes the user nikname";
        public static string Scd_a_nikname_d => "New nikname";
        public static string Scd_a_log_d => "name of stored log";
        public static string Scd_a_log => "Zenos will show stored logs.";
        public static string Scd_cacpot => "Adds or removes you from my reminder list for ff cacpot.";
        public static string Scd_botinfo => "Shows information about Wind-up Zeno♥.";


        //Commands
        public static string Embed_add_cuctar_t => $"{Emote.Bot.Cactuar} WOOOAAA!! {Emote.Bot.Pepelove}";
        public static string Embed_add_cuctar_d => $"Now your are on my {Emote.Bot.Cactuar} list!!! {Emote.Bot.Boss + NL} I will remember you each cacpot {Emote.Bot.Cactuar}!!!";
        public static string Embed_add_cuctar_url => $"https://i.postimg.cc/vHScd8Y6/t-E1cr-TL23-R.png";
        public static string Embed_remove_cuctar_t => $"Why!!! {Emote.Bot.Sadtuff}";
        public static string Embed_remove_cuctar_d => $"Ok lets do this fast... {Emote.Bot.Disconnecting_party + NL} You are not on my list, now i don't going remember you {Emote.Bot.Cactuar}.";
        public static string Embed_remove_cuctar_url => $"https://i.postimg.cc/fbR2SK4P/Xd-Pvo-I2yp-Q.png";
        public static string Msg_err1_timestamp => "Please provide a valid date time my friend in the format YYYY-MM-DD HH:mm:ss.";
        public static string Msg_err2_timestamp => "Invalid date format my friend.Use YYYY-MM-DD HH:mm:ss.";
        public static string Embed_timestamp_t => "Timestamp";
        public static string Embed_timestamp_d => $"Here's your Discord timestamp my enemy:";
        public static string Embed_timestamp_f => $" My friend.";

        //others
        public static string MsgRecived_err_0 => "Sorry {0} Something whent wrong D:";

        //public static string xxxxxx => "xxxxxxxxxxxx"; 
        //public static string xxxxxx => "xxxxxxxxxxxx"; 
        //public static string xxxxxx => "xxxxxxxxxxxx"; 
    }
}

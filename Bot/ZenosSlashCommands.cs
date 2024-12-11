using Discord;
using static System.Net.WebRequestMethods;

namespace Begu
{
    public static class ZenosSlashCommands
    {
        public static ApplicationCommandProperties[] Zenos_SC = new ApplicationCommandProperties[]
        {

            new SlashCommandBuilder()
                .WithName("a_commands")
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .WithDescription(StringT.Scd_a_commands)
                .Build(),

            new SlashCommandBuilder()
                .WithName("timers")
                .WithDescription(StringT.Scd_timers)
                .Build(),
            new SlashCommandBuilder()
                .WithName("a_answer")
                .WithDescription(StringT.Scd_a_answer)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)            
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_userinfo")
                .WithDescription(StringT.Scd_a_userinfo)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("name")
                    .WithDescription(StringT.Scd_usermention)
                    .WithType(ApplicationCommandOptionType.User)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("ffnews")
                .WithDescription(StringT.Scd_ffnews)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("number")
                    .WithDescription(StringT.Scd_desc_hmn)
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithRequired(false)
                    )
                .Build(),

            new SlashCommandBuilder()
                .WithName("ffstatus")
                .WithDescription(StringT.Scd_ffstatus)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("number")
                    .WithDescription(StringT.Scd_desc_hmn)
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithRequired(false)
                    )
                .Build(),

            new SlashCommandBuilder()
            .WithName("ffmaintenance")
            .WithDescription(StringT.Scd_ffmaintenence)
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("number")
                .WithDescription(StringT.Scd_desc_hmn)
                .WithType(ApplicationCommandOptionType.Integer)
                .WithRequired(false)
                )
            .Build(),

            new SlashCommandBuilder()
            .WithName("ffmaintenancenow")
            .WithDescription(StringT.Scd_ffmaintenencenow)
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("number")
                .WithDescription(StringT.Scd_desc_hmn)
                .WithType(ApplicationCommandOptionType.Integer)
                .WithRequired(false)
                )
            .Build(),

            new SlashCommandBuilder()
            .WithName("ffupdates")
            .WithDescription(StringT.Scd_ffupdates)
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("number")
                .WithDescription(StringT.Scd_desc_hmn)
                .WithType(ApplicationCommandOptionType.Integer)
                .WithRequired(false)
                )
            .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_answer")
                .WithDescription(StringT.Scd_a_set_answer)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("channel")
                    .WithDescription(StringT.Scd_ctt_d)
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_log")
                .WithDescription(StringT.Scd_a_set_log)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("channell")
                    .WithDescription(StringT.Scd_ctt_d)
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_news")
                .WithDescription(StringT.Scd_a_set_ffnews)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("newsc")
                    .WithDescription(StringT.Scd_ctt_d)
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_update")
                .WithDescription(StringT.Scd_a_set_ffupdate)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("updatec")
                    .WithDescription(StringT.Scd_ctt_d)
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_status")
                .WithDescription(StringT.Scd_a_set_ffstatus)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("statusc")
                    .WithDescription(StringT.Scd_ctt_d)
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_maintenance")
                .WithDescription(StringT.Scd_a_set_ffmainenance)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("maintenancec")
                    .WithDescription(StringT.Scd_ctt_d)
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("timestamp")
                .WithDescription(StringT.Scd_timestamp)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("time")
                    .WithDescription(StringT.Scd_timestamp_d )
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("searchmount")
                .WithDescription(StringT.Scd_searchmount)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("mount")
                    .WithDescription(StringT.Scd_searchmount_d)
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("searchminion")
                .WithDescription(StringT.Scd_searchminion)
                .AddOption(new SlashCommandOptionBuilder()
                        .WithName("minion")
                    .WithDescription(StringT.Scd_searchminion_d)
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_show_stored")
                .WithDescription(StringT.Scd_a_show_stored)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_sendmsg")
                .WithDescription(StringT.Scd_a_sendmsg)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                        .WithName("title")
                        .WithDescription(StringT.Scd_a_sendmsg_d1)
                        .WithType(ApplicationCommandOptionType.String)
                        .WithRequired(true))
            .AddOption(new SlashCommandOptionBuilder()
                        .WithName("msg")
                        .WithDescription(StringT.Scd_a_sendmsg_d2)
                        .WithType(ApplicationCommandOptionType.String)
                        .WithRequired(true))
            .AddOption(new SlashCommandOptionBuilder()
                        .WithName("picture")
                        .WithDescription(StringT.Scd_a_sendmsg_d3)
                        .WithType(ApplicationCommandOptionType.String)
                        .WithRequired(false))
            .AddOption(new SlashCommandOptionBuilder()
                        .WithName("channel")
                        .WithDescription(StringT.Scd_a_sendmsg_d4)
                        .WithType(ApplicationCommandOptionType.Channel)
                        .AddChannelType(ChannelType.Text)
                        .WithRequired(false))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_notices")
                .WithDescription(StringT.Scd_a_set_ffnotices)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("notices")
                    .WithDescription(StringT.Scd_ctt_d)
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_react")
                .WithDescription(StringT.Scd_a_react)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                 .AddOption(new SlashCommandOptionBuilder()
                    .WithName("message_link")
                    .WithDescription(StringT.Scd_a_react_d1)
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("emote")
                    .WithDescription(StringT.Scd_a_react_d2)
                    .WithType(ApplicationCommandOptionType.String)                
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("ffnotices")
                .WithDescription(StringT.Scd_ffnotices)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("number")
                    .WithDescription(StringT.Scd_desc_hmn)
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithRequired(false))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_nikname")
                .WithDescription(StringT.Scd_a_nikname)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("name")
                    .WithDescription(StringT.Scd_usermention)
                    .WithType(ApplicationCommandOptionType.User)
                    .WithRequired(true))
            .AddOption(new SlashCommandOptionBuilder()
                    .WithName("nik")
                    .WithDescription(StringT.Scd_a_nikname_d)
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .Build()


        };
    }
}

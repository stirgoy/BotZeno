using Discord;

namespace Begu
{
    public static class ZenosSlashCommands
    {
        public static ApplicationCommandProperties[] Zenos_SC = new ApplicationCommandProperties[]
        {

            new SlashCommandBuilder()
                .WithName("a_commands")
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .WithDescription("Shows admin commands!")
                .Build(),

            new SlashCommandBuilder()
                .WithName("timers")
                .WithDescription("Shows Eorzean timers.")
                .Build(),
            new SlashCommandBuilder()
                .WithName("a_answer")
                .WithDescription("Shows my allowed answer channels for all users.")
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_userinfo")
                .WithDescription("I bring server information from a specific user.")
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("name")
                    .WithDescription("User name.")
                    .WithType(ApplicationCommandOptionType.User)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("ffnews")
                .WithDescription("Shows Lodestone news.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("number")
                    .WithDescription("How many news you want see?")
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithRequired(false)
                    )
                .Build(),

            new SlashCommandBuilder()
                .WithName("ffstatus")
                .WithDescription("Shows Lodestone news.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("number")
                    .WithDescription("How many news you want see?")
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithRequired(false)
                    )
                .Build(),

            new SlashCommandBuilder()
            .WithName("ffmaintenance")
            .WithDescription("Shows Lodestone maintenance status.")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("number")
                .WithDescription("How many news you want see?")
                .WithType(ApplicationCommandOptionType.Integer)
                .WithRequired(false)
                )
            .Build(),
            new SlashCommandBuilder()
            .WithName("ffupdates")
            .WithDescription("Shows Lodestone news.")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("number")
                .WithDescription("How many news you want see?")
                .WithType(ApplicationCommandOptionType.Integer)
                .WithRequired(false)
                )
            .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_answer")
                .WithDescription("Edits my answer channels, if channel is already added i going remove ^^")
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("channel")
                    .WithDescription("Choose a text channel")
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_log")
                .WithDescription("Edits my log channel, only can be one channel")
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("channell")
                    .WithDescription("Choose a text channel")
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_news")
                .WithDescription("Edits my ff news channel, only can be one channel")
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("newsc")
                    .WithDescription("Choose a text channel")
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_update")
                .WithDescription("Edits my ff updates channel, only can be one channel")
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("updatec")
                    .WithDescription("Choose a text channel")
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_status")
                .WithDescription("Edits my ff status channel, only can be one channel")
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("statusc")
                    .WithDescription("Choose a text channel")
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("a_set_maintenance")
                .WithDescription("Edits my ff mainenance channel, only can be one channel")
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("maintenancec")
                    .WithDescription("Choose a text channel")
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Text)
                    .WithRequired(true))
                .Build(),

            new SlashCommandBuilder()
                .WithName("timestamp")
                .WithDescription("I bring you a dynamic display of date time and the code ♥")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("time")
                    .WithDescription("Text a date time. example: YYYY-MM-DD HH:mm:ss, can be only the hour.")
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .Build()
        };
    }
}

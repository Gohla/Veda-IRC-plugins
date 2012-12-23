using System;
using ReactiveIRC.Interface;
using Veda.Interface;

namespace Veda.Plugins.Channel
{
    [Plugin(Name = "Channel", Description = "Provides IRC channel control.")]
    public static class ChannelPlugin
    {
        [Command(Description = "Joins given channel."), Permission(Group.Administrator, Allowed = true, Limit = 5, 
            Timespan = 60000), Permission(Group.Owner, Allowed = true)]
        public static void Join(IContext context, String channelName)
        {
            context.Connection.Join(channelName);
        }

        [Command(Description = "Parts given channel."), Permission(Group.Administrator, Allowed = true, Limit = 5,
            Timespan = 60000), Permission(Group.Owner, Allowed = true)]
        public static void Part(IContext context, IChannel channel)
        {
            context.Connection.Part(channel);
        }

        [Command(Description = "Parts given channel with a part message."), Permission(Group.Administrator, 
            Allowed = true, Limit = 5, Timespan = 60000), Permission(Group.Owner, Allowed = true)]
        public static void Part(IContext context, IChannel channel, String message)
        {
            context.Connection.Part(channel, message);
        }

        [Command(Description = "Parts and joins given channel."), Permission(Group.Administrator, Allowed = true, 
            Limit = 5, Timespan = 60000), Permission(Group.Owner, Allowed = true)]
        public static void Hop(IContext context, IChannel channel)
        {
            context.Connection.Hop(channel);
        }

        [Command(Description = "Parts and joins given channel with a part message."), Permission(Group.Administrator, 
            Allowed = true, Limit = 5, Timespan = 60000), Permission(Group.Owner, Allowed = true)]
        public static void Hop(IContext context, IChannel channel, String message)
        {
            context.Connection.Hop(channel, message);
        }

        [Command(Description = "Kicks a user from the channel."), Permission(Group.Administrator, Allowed = true, 
            Limit = 5, Timespan = 60000), Permission(Group.Owner, Allowed = true)]
        public static void Kick(IContext context, IChannelUser user)
        {
            user.Kick();
        }

        [Command(Description = "Kicks a user from the channel with a reason."), Permission(Group.Administrator, 
            Allowed = true, Limit = 5, Timespan = 60000), Permission(Group.Owner, Allowed = true)]
        public static void Kick(IContext context, IChannelUser user, String reason)
        {
            user.Kick(reason);
        }
    }
}

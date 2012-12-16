using System;
using ReactiveIRC.Interface;
using Veda.Interface;

namespace Veda.Plugins.Channel
{
    [Plugin(Name = "Channel", Description = "Provides IRC channel control.")]
    public static class ChannelPlugin
    {
        [Command(Description = "Joins given channel.")]
        public static void Join(IContext context, String channelName)
        {
            context.Message.Connection.Join(channelName);
        }

        [Command(Description = "Parts given channel.")]
        public static void Part(IContext context, IChannel channel)
        {
            context.Message.Connection.Part(channel);
        }

        [Command(Description = "Parts given channel with a part message.")]
        public static void Part(IContext context, IChannel channel, String message)
        {
            context.Message.Connection.Part(channel, message);
        }

        [Command(Description = "Parts and joins given channel.")]
        public static void Hop(IContext context, IChannel channel)
        {
            context.Message.Connection.Hop(channel);
        }

        [Command(Description = "Parts and joins given channel with a part message.")]
        public static void Hop(IContext context, IChannel channel, String message)
        {
            context.Message.Connection.Hop(channel, message);
        }

        [Command(Description = "Kicks a user from the channel.")]
        public static void Kick(IContext context, IChannelUser user)
        {
            user.Kick();
        }

        [Command(Description = "Kicks a user from the channel with a reason.")]
        public static void Kick(IContext context, IChannelUser user, String reason)
        {
            user.Kick(reason);
        }
    }
}

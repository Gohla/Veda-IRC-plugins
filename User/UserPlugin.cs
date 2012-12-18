using System;
using Veda.Interface;

namespace Veda.Plugins.User
{
    [Plugin(Name = "User", Description = "User identification and management.")]
    public static class UserPlugin
    {
        [Command(Description = "Register a user account with given username and password. Must be sent in private.")]
        public static void Register(IContext context, String username, String password)
        {
            context.Bot.Authentication.Register(context.Sender, username, password);
        }

        [Command(Description = "Identify as user with given username and password. Must be sent in private.")]
        public static void Identify(IContext context, String username, String password)
        {
            context.Bot.Authentication.Identify(context.Sender, username, password);
        }

        [Command(Description = "Returns your identified username.")]
        public static String Username(IContext context)
        {
            if(!context.Bot.Authentication.IsIdentified(context.Sender))
                throw new InvalidOperationException("You are not identified.");
            return context.User.Username;
        }

        [Command(Description = "Returns your identified group.")]
        public static String Group(IContext context)
        {
            if(!context.Bot.Authentication.IsIdentified(context.Sender))
                throw new InvalidOperationException("You are not identified.");
            return context.User.Group.Name;
        }
    }
}

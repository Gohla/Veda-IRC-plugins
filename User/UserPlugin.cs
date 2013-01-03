using System;
using System.Collections.Generic;
using ReactiveIRC.Interface;
using Veda.Interface;

namespace Veda.Plugins.User
{
    [Plugin(Name = "User", Description = "User identification and management.")]
    public static class UserPlugin
    {
        [Command(Description = "Register a user account with given username and password.", Private = true)]
        public static void Register(IContext context, String username, String password)
        {
            context.Bot.Authentication.Register(context.Sender, username, password);
        }

        [Command(Description = "Unregister your account. Username and password must be given for confirmation.", Private = true)]
        public static void Unregister(IContext context, String username, String password)
        {
            EnsureIdentified(context);
            context.Bot.Authentication.Unregister(context.Sender, context.User, username, password);
        }

        [Command(Description = "Identify as user with given username and password.", Private = true)]
        public static void Identify(IContext context, String username, String password)
        {
            context.Bot.Authentication.Identify(context.Sender, username, password);
        }

        [Command(Description = "Unidentifies you from the bot.")]
        public static void Unidentify(IContext context)
        {
            context.Bot.Authentication.Unidentify(context.Sender);
        }

        [Command(Description = "Adds your current identity mask to your identity masks. Any commands invoked under that mask are automatically identified as you.")]
        public static void AddMask(IContext context)
        {
            EnsureIdentified(context);
            context.Bot.Authentication.AddMask(context.User, new IdentityMask(context.Sender.Identity));
        }

        [Command(Description = "Adds an identity mask to your identity masks. Any commands invoked under that mask are automatically identified as you.")]
        public static void AddMask(IContext context, IdentityMask mask)
        {
            EnsureIdentified(context);
            context.Bot.Authentication.AddMask(context.User, mask);
        }

        [Command(Description = "Remove a mask from your identity masks.")]
        public static void RemoveMask(IContext context, IdentityMask mask)
        {
            EnsureIdentified(context);
            context.Bot.Authentication.RemoveMask(context.User, mask);
        }

        [Command(Description = "Lists your identity masks.")]
        public static IEnumerable<IdentityMask> ListMasks(IContext context)
        {
            EnsureIdentified(context);
            return context.Bot.Authentication.Masks(context.User);
        }

        [Command(Description = "Returns your identified username.")]
        public static String Username(IContext context)
        {
            EnsureIdentified(context);
            return context.User.Username;
        }

        [Command(Description = "Returns the identified username for given user.")]
        public static String Username(IContext context, IUser user)
        {
            EnsureIdentified(context, user);
            return context.Bot.Authentication.GetUser(user).Username;
        }

        [Command(Description = "Returns your identified group.")]
        public static String Group(IContext context)
        {
            EnsureIdentified(context);
            return context.User.Group.Name;
        }

        [Command(Description = "Returns the identified group for given user.")]
        public static String Group(IContext context, IUser user)
        {
            EnsureIdentified(context, user);
            return context.Bot.Authentication.GetUser(user).Group.Name;
        }

        [Command(Description = "Sets the group for given user."), Permission(Veda.Interface.Group.Administrator, 
            Allowed = true), Permission(Veda.Interface.Group.Owner, Allowed = true)]
        public static void SetGroup(IContext context, IBotUser botUser, IBotGroup group)
        {
            EnsureIdentified(context);

            if(context.User.Equals(botUser))
                throw new InvalidOperationException("Cannot set your own group.");

            if(botUser.Group.IsMoreOrSamePrivileged(context.User.Group))
                throw new InvalidOperationException("Cannot set group for users that have the same or more privileges than yourself.");

            if(group.IsMoreOrSamePrivileged(context.User.Group))
                throw new InvalidOperationException("Cannot set group to one that has the same or more privileges than yourself.");

            botUser.Group = group;
        }

        private static void EnsureIdentified(IContext context, IUser user)
        {
            if(!context.Bot.Authentication.IsIdentified(user))
                throw new InvalidOperationException("User is not identified.");
        }

        private static void EnsureIdentified(IContext context)
        {
            if(!context.Bot.Authentication.IsIdentified(context.Sender))
                throw new InvalidOperationException("You are not identified. Use Identify to identify yourself with the bot, or use Register to register an account with the bot.");
        }
    }
}

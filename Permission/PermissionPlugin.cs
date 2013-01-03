using System;
using System.Collections.Generic;
using Veda.Interface;
using System.Linq;

namespace Veda.Plugins.Permission
{
    [Plugin(Name = "Permission", Description = "Permission management.")]
    public static class PermissionPlugin
    {
        [Command(Description = "Sets if given group may execute given command."), Permission(Group.Administrator, 
            Allowed = true), Permission(Group.Owner, Allowed = true)]
        public static void SetAllowed(IContext context, ICommand command, IBotGroup group, bool allowed)
        {
            EnsureAllowGroup(context, group);
            context.Bot.Permission.GetPermission(command, group).Allowed = allowed;
        }

        [Command(Description = "Sets a limit on the number of times given command maybe executed by members from given group, within a timespan (in milliseconds)."), Permission(Group.Administrator, Allowed = true), Permission(Group.Owner, Allowed = true)]
        public static void SetLimit(IContext context, ICommand command, IBotGroup group, ushort limit, 
            ulong timespan)
        {
            EnsureAllowGroup(context, group);
            IPermission permission = context.Bot.Permission.GetPermission(command, group);
            permission.Limit = limit;
            permission.Timespan = TimeSpan.FromMilliseconds(timespan);
        }

        [Command(Description = "Gets the permissions for given command.")]
        public static IEnumerable<IPermission> Permissions(IContext context, ICommand command)
        {
            IEnumerable<IPermission> permissions = context.Bot.Authentication.Groups
                .Where(g => context.Bot.Permission.HasPermission(command, g))
                .Select(g => context.Bot.Permission.GetPermission(command, g))
                ;

            if(permissions.IsEmpty())
                throw new ArgumentException("Given command has no permissions.");

            return permissions;
        }

        private static void EnsureAllowGroup(IContext context, IBotGroup group)
        {
            if(group.IsMoreOrSamePrivileged(context.User.Group))
                throw new InvalidOperationException("Cannot set permissions for a group with the same or more privileges than yours.");
        }
    }
}

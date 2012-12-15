using System;
using System.Collections.Generic;
using System.Linq;
using Gohla.Shared;
using Veda.Interface;

namespace Veda.Plugins.Info
{
    [Plugin(Name = "Info", Description = "Provides information about plugins and commands.")]
    public static class InfoPlugin
    {
        [Command(Description = "Shows information about a command.")]
        public static IEnumerable<String> Help(IContext context, IEnumerable<ICommand> command)
        {
            return command
                .Select(c => c.ToString() + ": " + c.Description)
                ;
        }

        [Command(Description = "Shows information about a plugin.")]
        public static String Help(IContext context, IPlugin plugin)
        {
            return plugin.Description;
        }

        [Command(Description = "Shows information about a command in a plugin.")]
        public static IEnumerable<String> Help(IContext context, IPlugin plugin, String command)
        {
            IEnumerable<ICommand> candidates = context.Bot.Command.GetCommands(plugin.Name, command);

            if(candidates.IsEmpty())
                throw new ArgumentException("Command " + command + " not found in " + plugin.Name + ".");

            return candidates
                .Select(c => c.ToString() + ": " + c.Description)
                ;
        }
    }
}

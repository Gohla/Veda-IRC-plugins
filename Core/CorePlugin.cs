using System;
using System.Collections.Generic;
using System.Linq;
using Veda.Interface;

namespace Veda.Plugins.Core
{
    [Plugin(Name = "Core", Description = "Provides several core functionality such as help and echoing.")]
    public static class CorePlugin
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

        [Command(Description = "Echoes message to the channel the command was sent to, or to the user that sent the command.")]
        public static String Echo(IContext context, String message)
        {
            return message;
        }

        [Command(Description = "Replies to the user that sent this command.")]
        public static String Reply(IContext context, String reply)
        {
            return context.Message.Sender.Name + ": " + reply;
        }
    }
}

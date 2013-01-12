using System;
using System.Collections.Generic;
using System.Linq;
using Veda.Interface;

namespace Veda.Plugins.Plugin
{
    [Plugin(Name = "Plugin", Description = "Manages and retrieves information about plugins.")]
    public static class PluginPlugin
    {
        [Command(Description = "Lists all plugins.")]
        public static IEnumerable<String> List(IContext context)
        {
            IEnumerable<String> pluginNames = context.Bot.Plugin.Plugins
                .Select(p => p.Name)
                ;

            context.Seperator = " ";

            return pluginNames;
        }

        [Command(Description = "Lists all command in a plugin.")]
        public static IEnumerable<String> List(IContext context, IPlugin plugin)
        {
            IEnumerable<String> commandNames = context.Bot.Command.GetCommands(plugin)
                .Select(p => p.Name.ToLower())
                .Distinct()
                ;

            context.Seperator = ", ";

            return commandNames;
        }
    }
}

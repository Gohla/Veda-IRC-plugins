using System;
using System.Linq;
using Veda.Interface;

namespace Veda.Plugins.Plugin
{
    [Plugin(Name = "Plugin", Description = "Manages and retrieves information about plugins.")]
    public static class PluginPlugin
    {
        [Command(Description = "Lists all plugins.")]
        public static String List(IContext context)
        {
            return context.Bot.Plugin.Plugins
                .Select(p => p.Name)
                .ToString(" ")
                ;
        }

        [Command(Description = "Lists all command in a plugin.")]
        public static String List(IContext context, IPlugin plugin)
        {
            return plugin.Commands
                .Select(p => p.Name)
                .Distinct()
                .ToString(" ")
                ;
        }
    }
}

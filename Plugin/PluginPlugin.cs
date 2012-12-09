using System;
using System.Linq;
using Veda.Plugin;

namespace Veda.Plugins.Plugin
{
    [Plugin(Name = "Plugin", Description = "Manages and retrieves information about plugins.")]
    public class PluginPlugin
    {
        [Command(Name = "list", Description = "Lists all plugins.")]
        public String List(IContext context)
        {
            return context.Bot.PluginManager.Plugins
                .Select(p => p.Name)
                .ToString(" ")
                ;
        }

        [Command(Name = "list", Description = "Lists all command in a plugin.")]
        public String List(IContext context, IPlugin plugin)
        {
            return plugin.Commands
                .Select(p => p.Name)
                .Distinct()
                .ToString(" ")
                ;
        }
    }
}

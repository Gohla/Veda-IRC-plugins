using System;
using System.Collections.Generic;
using System.Linq;
using Veda.Command;
using Veda.Plugin;

namespace Veda.Plugins.Plugin
{
    public class PluginPlugin : IPlugin
    {
        private List<ICommand> _commands = new List<ICommand>();

        public String Name { get; set; }
        public String Description { get; set; }
        public IEnumerable<ICommand> Commands { get { return _commands; } }
        public String Key { get { return Name; } }

        public PluginPlugin()
        {
            Name = "Plugin";
            Description = "Manages and retrieves information about plugins.";

            _commands.Add(CommandBuilder.CreateCommand<Func<IContext, String>>
            (
                "list", 
                "Lists all plugins.",
                context => List(context))
            );
            _commands.Add(CommandBuilder.CreateCommand<Func<IContext, IPlugin, String>>
            (
                "list",
                "Lists all commands in a plugin.",
                (context, plugin) => List(context, plugin))
            );
        }

        public void Dispose()
        {
            
        }

        public String List(IContext context)
        {
            return context.Bot.PluginManager.Plugins
                .Select(p => p.Name)
                .ToString(" ")
                ;
        }

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

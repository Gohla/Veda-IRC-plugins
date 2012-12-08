using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veda.Command;
using Veda.Plugin;
using Gohla.Shared;

namespace Veda.Plugins.Help
{
    public class HelpPlugin : IPlugin
    {
        private List<ICommand> _commands = new List<ICommand>();

        public String Name { get; set; }
        public String Description { get; set; }
        public IEnumerable<ICommand> Commands { get { return _commands; } }
        public String Key { get { return Name; } }

        public HelpPlugin()
        {
            Name = "Help";
            Description = "Provides help for plugins and commands.";

            _commands.Add(CommandBuilder.CreateCommand<Func<IContext, IPlugin, String>>
            (
                "help", 
                "Shows help about a plugin.", 
                (context, plugin) => Help(context, plugin))
            );
            _commands.Add(CommandBuilder.CreateCommand<Func<IContext, IPlugin, String, String>>
            (
                "help",
                "Shows help about a command in a plugin.", 
                (context, plugin, command) => Help(context, plugin, command))
            );
        }

        public void Dispose()
        {
            
        }

        public String Help(IContext context, IPlugin plugin)
        {
            return plugin.Description;
        }

        public String Help(IContext context, IPlugin plugin, String command)
        {
            IEnumerable<ICommand> candidates = plugin.Commands
                .Where(c => c.Name.Equals(command))
                ;

            if(candidates.IsEmpty())
                return "Command " + command + " not found in " + plugin.Name + ".";

            return candidates
                .Select(c => "(" + c.Name + " " + c.ParameterTypes.Skip(1).ToString(", ") + "): " + c.Description)
                .ToString(" | ")
                ;
        }
    }
}

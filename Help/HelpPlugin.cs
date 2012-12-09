using System;
using System.Collections.Generic;
using System.Linq;
using Gohla.Shared;
using Veda.Interface;

namespace Veda.Plugins.Help
{
    [Plugin(Name = "Help", Description = "Provides help for plugins and commands.")]
    public class HelpPlugin
    {
        [Command(Name = "help", Description = "Shows help about a plugin.")]
        public String Help(IContext context, IPlugin plugin)
        {
            return plugin.Description;
        }

        [Command(Name = "help", Description = "Shows help about command in a plugin.")]
        public String Help(IContext context, IPlugin plugin, String command)
        {
            IEnumerable<ICommand> candidates = plugin.Commands
                .Where(c => c.Name.Equals(command))
                ;

            if(candidates.IsEmpty())
                throw new ArgumentException("Command " + command + " not found in " + plugin.Name + ".");

            return candidates
                .Select(c => "(" + c.Name + " " + c.ParameterTypes.Skip(1).Select(t => t.Name).ToString(", ") + "): " + c.Description)
                .ToString(" | ")
                ;
        }
    }
}

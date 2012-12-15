using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veda.Command;
using Veda.Interface;

namespace Veda.Plugins.Alias
{
    public class AliasCommand : AbstractCommand
    {
        private AliasExpression _alias;

        public AliasCommand(IPlugin plugin, String name, String description, AliasExpression alias) :
            base(plugin, name, description,
                Enumerable.Repeat(typeof(String), alias.Arity).ToArray(),
                Enumerable.Range(1, alias.Arity).Select(i => i.ToString()).ToArray()
            )
        {
            _alias = alias;
        }

        public override object Call(IContext context, params object[] arguments)
        {
            return _alias.Evaluate(context, arguments);
        }
    }
}

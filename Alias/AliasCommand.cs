using System;
using System.Linq;
using Veda.Command;
using Veda.Interface;

namespace Veda.Plugins.Alias
{
    public class AliasCommand : AbstractCommand
    {
        private AliasExpression _alias;

        public AliasCommand(IPlugin plugin, String name, String description, bool @private, AliasExpression alias) :
            base(plugin, name)
        {
            Description = description;
            ParameterTypes = Enumerable.Repeat(typeof(String), alias.Arity).ToArray();
            ParameterNames = Enumerable.Range(1, alias.Arity).Select(i => i.ToString()).ToArray();
            DefaultPermissions = new PermissionAttribute[0];
            Private = @private;

            _alias = alias;
        }

        public override object Call(IContext context, params object[] arguments)
        {
            return _alias.Evaluate(context, arguments);
        }
    }
}

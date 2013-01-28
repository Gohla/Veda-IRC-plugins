using System;
using System.Linq;
using Veda.Command;
using Veda.Interface;

namespace Veda.Plugins.Alias
{
    public class AliasCommand : AbstractCommand
    {
        private IExpression _expression;

        public AliasCommand(IPlugin plugin, String name, String description, bool @private, IExpression expression) :
            base(plugin, name)
        {
            Description = description;
            ParameterTypes = Enumerable.Repeat(typeof(String), expression.Arity).ToArray();
            ParameterNames = Enumerable.Range(1, expression.Arity).Select(i => i.ToString()).ToArray();
            DefaultPermissions = new PermissionAttribute[0];
            Private = @private;

            _expression = expression;
        }

        public override object Call(IContext context, params object[] arguments)
        {
            return _expression.Evaluate(context, arguments);
        }
    }
}

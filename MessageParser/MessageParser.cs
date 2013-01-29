using System;
using System.Linq;
using System.Text.RegularExpressions;
using ReactiveIRC.Interface;
using Veda.Interface;

namespace Veda.Plugins.MessageParser
{
    public class MessageParser
    {
        public readonly Regex Regex;
        public readonly IExpression Expression;

        public MessageParser(Regex regex, IExpression expression)
        {
            Regex = regex;
            Expression = expression;
        }

        public void Match(IContext context, IReceiveMessage message)
        {
            Match match = Regex.Match(message.Contents);
            if(!match.Success)
                return;
            String[] arguments = match.Groups
                .As<System.Text.RegularExpressions.Group>()
                .Select(g => g.Value)
                .ToArray()
                ;
            context.Bot.Output(context, message, Expression.Evaluate(context, arguments), false);
        }
    }
}

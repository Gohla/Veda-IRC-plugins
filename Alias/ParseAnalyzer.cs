using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using PerCederberg.Grammatica.Runtime;
using Veda.Interface;
using Veda.Plugins.Alias.Grammar;

namespace Veda.Plugins.Alias
{
    public interface Expression
    {
        IObservable<object> Evaluate(IContext context, object[] arguments);
    }

    public class AliasExpression
    {
        public List<CommandExpression> Commands = new List<CommandExpression>();
        public String Name;
        public String Expression;
        public ushort Arity = 0;

        public IObservable<object> Evaluate(IContext context, object[] arguments)
        {
            return Commands
                .Select(command => context.Evaluate(command.Evaluate(context, arguments)))
                .Concat()
                ;
        }
    }

    public class CommandExpression : Expression
    {
        public List<Expression> Expressions = new List<Expression>();

        public IObservable<object> Evaluate(IContext context, object[] arguments)
        {
            var results = Expressions
                .Select(e => context.Evaluate(e.Evaluate(context, arguments)).Wait()) // TODO: Wait() is blocking!
                .ToArray()
                ;

            try
            {
                ICallable callable = context.Bot.Command.CallParsed(context.ConversionContext, results);
                return context.Evaluate(callable);
            }
            catch(Exception e)
            {
                return Observable.Throw<String>(e);
            }
        }
    }

    public class Literal : Expression
    {
        public String Text;

        public IObservable<object> Evaluate(IContext context, object[] arguments)
        {
            return Observable.Return(Text);
        }
    }

    public class Parameter : Expression
    {
        public ushort Index;

        public IObservable<object> Evaluate(IContext context, object[] arguments)
        {
            return Observable.Return(arguments[Index - 1]);
        }
    }

    internal class ParseAnalyzer : AliasGrammarAnalyzer
    {
        private Stack<CommandExpression> _commandStack = new Stack<CommandExpression>();

        public AliasExpression Alias { get; private set; }

        public ParseAnalyzer()
        {
            Alias = new AliasExpression();
        }

        public override void Reset()
        {
            base.Reset();

            _commandStack.Clear();
            Alias = new AliasExpression();
        }

        public override void EnterCommand(Production node)
        {
            _commandStack.Push(new CommandExpression());
        }

        public override Node ExitCommand(Production node)
        {
            CommandExpression command = _commandStack.Pop();
            if(_commandStack.Count == 0)
                Alias.Commands.Add(command);
            else
                _commandStack.Peek().Expressions.Add(command);

            return node;
        }

        public override void EnterParameter(Token node)
        {
            _commandStack.Peek().Expressions.Add(new Parameter { Index = UInt16.Parse(node.Image.Substring(1)) });
            ++Alias.Arity;
        }

        public override void EnterString(Token node)
        {
            _commandStack.Peek().Expressions.Add(new Literal { Text = node.Image.Substring(1, node.Image.Length - 2) });
        }

        public override void EnterText(Token node)
        {
            _commandStack.Peek().Expressions.Add(new Literal { Text = node.Image });
        }
    }
}

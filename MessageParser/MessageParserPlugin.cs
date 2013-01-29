using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Gohla.Shared;
using ReactiveIRC.Interface;
using Veda.Interface;

namespace Veda.Plugins.MessageParser
{
    [Plugin(Name = "MessageParser", Description = "Parse message with regular expressions and execute a command when matched.")]
    public class MessageParserPlugin : IDisposable
    {
        private readonly Dictionary<String, MessageParser> _messageParsers =
            new Dictionary<String, MessageParser>();
        private IDisposable _messageDisposable;

        public KeyedCollection<String, MessageParserData> Data { get; set; }

        public MessageParserPlugin()
        {
            Data = new KeyedCollection<String, MessageParserData>();
        }

        public void Dispose()
        {
            _messageDisposable.Dispose();
        }

        [Loaded]
        public void Loaded(IPlugin plugin, IBot bot)
        {
            foreach(MessageParserData data in Data)
            {
                Regex regex = ParseRegex(data.Pattern);
                IExpression expression = ParseExpression(bot.Command, data.Expression);
            }

            _messageDisposable = bot.Messages.Subscribe(ParseMessage);
        }

        [Command(Description = "Adds a message parser that executes given expression when given pattern is matched.")]
        public void Add(IContext context, String pattern, String expression)
        {
            Regex regex = ParseRegex(pattern);
            IExpression parsedExpression = ParseExpression(context.Bot.Command, expression);
            Add(new MessageParserData(pattern, expression), regex, parsedExpression, true);
        }

        [Command(Description = "Replaces the expression of message parser with given pattern.")]
        public void Set(IContext context, String pattern, String expression)
        {
            Regex regex = ParseRegex(pattern);
            IExpression parsedExpression = ParseExpression(context.Bot.Command, expression);
            Remove(pattern);
            Add(new MessageParserData(pattern, expression), regex, parsedExpression, true);
        }

        [Command(Description = "Removes message parser with given pattern.")]
        public void Remove(IContext context, String pattern)
        {
            Remove(pattern);
        }

        private void ParseMessage(Tuple<IContext, IReceiveMessage> tuple)
        {
            foreach(MessageParser parser in _messageParsers.Values)
            {
                parser.Match(tuple.Item1, tuple.Item2);
            }
        }

        private IExpression ParseExpression(ICommandManager commandManager, String expression)
        {
            return commandManager.Parse(expression);
        }

        private Regex ParseRegex(String pattern)
        {
            return new Regex(pattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(100));
        }

        private void Add(MessageParserData data, Regex regex, IExpression expression, bool store)
        {
            if(_messageParsers.ContainsKey(data.Pattern))
                throw new InvalidOperationException("Message parser for pattern " + data.Pattern + " already exists.");

            MessageParser parser = new MessageParser(regex, expression);
            _messageParsers.Add(data.Pattern, parser);
            if(store)
                Data.Add(data);
        }

        private void Remove(String pattern)
        {
            if(!_messageParsers.ContainsKey(pattern))
                throw new InvalidOperationException("Message parser for pattern " + pattern + " does not exists.");

            _messageParsers.Remove(pattern);
            Data.Remove(pattern);
        }
    }
}

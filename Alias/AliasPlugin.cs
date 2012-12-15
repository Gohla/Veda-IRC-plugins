using System;
using System.IO;
using ReactiveIRC.Interface;
using Veda.Interface;
using Veda.Plugins.Alias.Grammar;

namespace Veda.Plugins.Alias
{
    [Plugin(Name = "Alias", Description = "Creates new commands with an alias.")]
    public class AliasPlugin
    {
        private readonly ParseAnalyzer _analyzer = new ParseAnalyzer();
        private readonly AliasGrammarParser _parser;

        public AliasPlugin()
        {
            _parser = new AliasGrammarParser(new StringReader(String.Empty), _analyzer);
            _parser.Prepare();
        }

        [Command(Description = "Adds an alias.")]
        public void Add(IContext context, String name, String alias)
        {
            _parser.Reset(new StringReader(alias));
            _parser.Parse();
            AliasCommand command = new AliasCommand(context.Command.Plugin, name, "Alias for (" 
                + ControlCodes.Bold(alias) + ")", _analyzer.Alias);
            context.Bot.Command.Add(command);
        }
    }
}

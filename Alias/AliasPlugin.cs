using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gohla.Shared;
using ReactiveIRC.Interface;
using Veda.Command;
using Veda.Interface;
using Veda.Plugins.Alias.Grammar;

namespace Veda.Plugins.Alias
{
    [Plugin(Name = "Alias", Description = "Creates new commands under an alias that can call and combine other commands.")]
    public class AliasPlugin
    {
        private readonly ParseAnalyzer _analyzer = new ParseAnalyzer();
        private readonly AliasGrammarParser _parser;
        private readonly MultiValueDictionary<String, ushort> _unqualifiedAliases =
            new MultiValueDictionary<String, ushort>();
        private readonly Dictionary<QualifiedNameTypes, UnparsedAlias> _aliases =
            new Dictionary<QualifiedNameTypes, UnparsedAlias>();
        private readonly Dictionary<QualifiedNameTypes, AliasCommand> _commands = 
            new Dictionary<QualifiedNameTypes, AliasCommand>();

        public HashSet<UnparsedAlias> Aliases { get; set; }

        public AliasPlugin()
        {
            _parser = new AliasGrammarParser(new StringReader(String.Empty), _analyzer);
            _parser.Prepare();

            Aliases = new HashSet<UnparsedAlias>();
        }

        [Loaded]
        public void Loaded(IPlugin plugin, IBot bot)
        {
            foreach(UnparsedAlias alias in Aliases)
            {
                AliasExpression aliasExpression = Parse(alias.Expression);
                Add(bot.Command, plugin, alias, aliasExpression, false);
            }
        }

        [Command(Description = "Adds an alias with given name.")]
        public void Add(IContext context, String name, String expression)
        {
            AliasExpression aliasExpression = Parse(expression);
            Add(context.Bot.Command, context.Command.Plugin, new UnparsedAlias(name, expression), aliasExpression, 
                true);
        }

        [Command(Description = "Replaces alias with given name.")]
        public void Set(IContext context, String name, String expression)
        {
            AliasExpression aliasExpression = Parse(expression);
            Remove(context.Bot.Command, context.Command.Plugin, name, aliasExpression.Arity);
            Add(context.Bot.Command, context.Command.Plugin, new UnparsedAlias(name, expression), aliasExpression, 
                true);
        }

        [Command(Description = "Removes an alias with given name.")]
        public void Remove(IContext context, String name)
        {
            IEnumerable<ushort> arities = _unqualifiedAliases[name];
            if(arities.IsEmpty())
                throw new ArgumentException("Alias with name " + name + " does not exist.", "name");
            if(arities.Count() > 1)
                throw new InvalidOperationException("There are multiple aliases with name " + name
                    + ", qualify the alias to remove by providing the arity of the alias.");

            Remove(context.Bot.Command, context.Command.Plugin, name, arities.First());
        }

        [Command(Description = "Removes an alias with given name and arity.")]
        public void Remove(IContext context, String name, ushort arity)
        {
            Remove(context.Bot.Command, context.Command.Plugin, name, arity);
        }

        private AliasExpression Parse(String expression)
        {
            // TODO: NOT reentrant (for performance).
            _parser.Reset(new StringReader(expression));
            _parser.Parse();
            return _analyzer.Alias;
        }

        private void Add(ICommandManager commandManager, IPlugin plugin, UnparsedAlias unparsedAlias, 
            AliasExpression alias, bool store)
        {
            ushort arity = alias.Arity;
            QualifiedNameTypes qualifiedName =
                QualifiedAliasName(plugin.Name, unparsedAlias.Name, arity);

            if(_aliases.ContainsKey(qualifiedName) || _commands.ContainsKey(qualifiedName))
                throw new InvalidOperationException("Alias with name " + unparsedAlias.Name + " and arity " + arity
                    + " already exists.");

            AliasCommand command = new AliasCommand(plugin, unparsedAlias.Name,
                "Alias for (" + ControlCodes.Bold(unparsedAlias.Expression) + ")", false, alias);
            commandManager.Add(command);

            _unqualifiedAliases.Add(unparsedAlias.Name, arity);
            _commands.Add(qualifiedName, command);
            _aliases.Add(qualifiedName, unparsedAlias);
            if(store)
                Aliases.Add(unparsedAlias);
        }

        private void Remove(ICommandManager commandManager, IPlugin plugin, String name, ushort arity)
        {
            QualifiedNameTypes qualifiedName = QualifiedAliasName(plugin.Name, name, arity);

            if(!_aliases.ContainsKey(qualifiedName) || !_commands.ContainsKey(qualifiedName))
                throw new InvalidOperationException("Alias with name " + name + " and arity " + arity
                    + " does not exist.");

            UnparsedAlias alias = _aliases[qualifiedName];
            AliasCommand command = _commands[qualifiedName];
            commandManager.Remove(command);

            _unqualifiedAliases.Remove(name, arity);
            _commands.Remove(qualifiedName);
            _aliases.Remove(qualifiedName);
            Aliases.Remove(alias);
        }

        private QualifiedNameTypes QualifiedAliasName(String pluginName, String name, ushort arity)
        {
            return new QualifiedNameTypes(pluginName, name, ParameterTypes(arity));
        }

        private Type[] ParameterTypes(ushort arity)
        {
            return Enumerable.Repeat<Type>(typeof(String), arity).ToArray();
        }
    }
}

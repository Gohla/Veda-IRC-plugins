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
    [Plugin(Name = "Alias", Description = "Creates new commands with an alias.")]
    public class AliasPlugin
    {
        private readonly ParseAnalyzer _analyzer = new ParseAnalyzer();
        private readonly AliasGrammarParser _parser;
        private readonly MultiValueDictionary<String, ushort> _unqualifiedAliasses =
            new MultiValueDictionary<String, ushort>();
        private readonly Dictionary<QualifiedNameTypes, AliasCommand> _commands = 
            new Dictionary<QualifiedNameTypes, AliasCommand>(); 

        public Dictionary<QualifiedNameTypes, AliasExpression> Aliasses { get; set; }

        public AliasPlugin()
        {
            _parser = new AliasGrammarParser(new StringReader(String.Empty), _analyzer);
            _parser.Prepare();

            Aliasses = new Dictionary<QualifiedNameTypes, AliasExpression>();
        }

        [Command(Description = "Adds an alias with given name.")]
        public void Add(IContext context, String name, String alias)
        {
            _parser.Reset(new StringReader(alias));
            _parser.Parse();
            Add(context.Bot.Command, context.Command.Plugin, name, alias, _analyzer.Alias);
        }

        [Command(Description = "Replaces alias with given name.")]
        public void Set(IContext context, String name, String alias)
        {
            _parser.Reset(new StringReader(alias));
            _parser.Parse();
            Remove(context.Bot.Command, context.Command.Plugin, name, _analyzer.Alias.Arity);
            Add(context.Bot.Command, context.Command.Plugin, name, alias, _analyzer.Alias);
        }

        [Command(Description = "Removes an alias with given name.")]
        public void Remove(IContext context, String name)
        {
            IEnumerable<ushort> arities = _unqualifiedAliasses.Get(name);
            if(arities.IsEmpty())
                throw new ArgumentException("Alias with name " + name + " does not exist.", "name");
            if(arities.Count() > 1)
                throw new ArgumentException("There are multiple aliases with name " + name
                    + ", qualify the alias to remove by providing the arity of the alias.", "name");

            Remove(context.Bot.Command, context.Command.Plugin, name, arities.First());
        }

        [Command(Description = "Removes an alias with given name and arity.")]
        public void Remove(IContext context, String name, ushort arity)
        {
            Remove(context.Bot.Command, context.Command.Plugin, name, arity);
        }

        private void Add(ICommandManager commandManager, IPlugin plugin, String name, String alias, 
            AliasExpression aliasExpression)
        {
            ushort arity = aliasExpression.Arity;
            QualifiedNameTypes qualifiedName =
                QualifiedAliasName(plugin.Name, name, arity);

            if(Aliasses.ContainsKey(qualifiedName))
                throw new InvalidOperationException("Alias with name " + name + " and arity " + arity
                    + " already exists.");

            AliasCommand command = new AliasCommand(plugin, name, "Alias for ("
                + ControlCodes.Bold(alias) + ")", aliasExpression);
            commandManager.Add(command);

            _unqualifiedAliasses.Add(name, arity);
            _commands.Add(qualifiedName, command);
            Aliasses.Add(qualifiedName, aliasExpression);
        }

        private void Remove(ICommandManager commandManager, IPlugin plugin, String name, ushort arity)
        {
            QualifiedNameTypes qualifiedName = QualifiedAliasName(plugin.Name, name, arity);

            if(!Aliasses.ContainsKey(qualifiedName) || !_commands.ContainsKey(qualifiedName))
                throw new InvalidOperationException("Alias with name " + name + " and arity " + arity
                    + " does not exist.");

            AliasCommand command = _commands[qualifiedName];
            commandManager.Remove(command);

            _unqualifiedAliasses.Remove(name, arity);
            _commands.Remove(qualifiedName);
            Aliasses.Remove(qualifiedName);
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

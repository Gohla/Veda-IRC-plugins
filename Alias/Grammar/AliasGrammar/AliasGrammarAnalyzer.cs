/*
 * AliasGrammarAnalyzer.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using PerCederberg.Grammatica.Runtime;

namespace Veda.Plugins.Alias.Grammar {

    /**
     * <remarks>A class providing callback methods for the
     * parser.</remarks>
     */
    internal abstract class AliasGrammarAnalyzer : Analyzer {

        /**
         * <summary>Called when entering a parse tree node.</summary>
         *
         * <param name='node'>the node being entered</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public override void Enter(Node node) {
            switch (node.Id) {
            case (int) AliasGrammarConstants.STRING:
                EnterString((Token) node);
                break;
            case (int) AliasGrammarConstants.TEXT:
                EnterText((Token) node);
                break;
            case (int) AliasGrammarConstants.PARAMETER:
                EnterParameter((Token) node);
                break;
            case (int) AliasGrammarConstants.STATEMENT_START:
                EnterStatementStart((Token) node);
                break;
            case (int) AliasGrammarConstants.STATEMENT_END:
                EnterStatementEnd((Token) node);
                break;
            case (int) AliasGrammarConstants.COMMAND:
                EnterCommand((Production) node);
                break;
            case (int) AliasGrammarConstants.STATEMENT:
                EnterStatement((Production) node);
                break;
            case (int) AliasGrammarConstants.EXPRESSION:
                EnterExpression((Production) node);
                break;
            }
        }

        /**
         * <summary>Called when exiting a parse tree node.</summary>
         *
         * <param name='node'>the node being exited</param>
         *
         * <returns>the node to add to the parse tree, or
         *          null if no parse tree should be created</returns>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public override Node Exit(Node node) {
            switch (node.Id) {
            case (int) AliasGrammarConstants.STRING:
                return ExitString((Token) node);
            case (int) AliasGrammarConstants.TEXT:
                return ExitText((Token) node);
            case (int) AliasGrammarConstants.PARAMETER:
                return ExitParameter((Token) node);
            case (int) AliasGrammarConstants.STATEMENT_START:
                return ExitStatementStart((Token) node);
            case (int) AliasGrammarConstants.STATEMENT_END:
                return ExitStatementEnd((Token) node);
            case (int) AliasGrammarConstants.COMMAND:
                return ExitCommand((Production) node);
            case (int) AliasGrammarConstants.STATEMENT:
                return ExitStatement((Production) node);
            case (int) AliasGrammarConstants.EXPRESSION:
                return ExitExpression((Production) node);
            }
            return node;
        }

        /**
         * <summary>Called when adding a child to a parse tree
         * node.</summary>
         *
         * <param name='node'>the parent node</param>
         * <param name='child'>the child node, or null</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public override void Child(Production node, Node child) {
            switch (node.Id) {
            case (int) AliasGrammarConstants.COMMAND:
                ChildCommand(node, child);
                break;
            case (int) AliasGrammarConstants.STATEMENT:
                ChildStatement(node, child);
                break;
            case (int) AliasGrammarConstants.EXPRESSION:
                ChildExpression(node, child);
                break;
            }
        }

        /**
         * <summary>Called when entering a parse tree node.</summary>
         *
         * <param name='node'>the node being entered</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual void EnterString(Token node) {
        }

        /**
         * <summary>Called when exiting a parse tree node.</summary>
         *
         * <param name='node'>the node being exited</param>
         *
         * <returns>the node to add to the parse tree, or
         *          null if no parse tree should be created</returns>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual Node ExitString(Token node) {
            return node;
        }

        /**
         * <summary>Called when entering a parse tree node.</summary>
         *
         * <param name='node'>the node being entered</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual void EnterText(Token node) {
        }

        /**
         * <summary>Called when exiting a parse tree node.</summary>
         *
         * <param name='node'>the node being exited</param>
         *
         * <returns>the node to add to the parse tree, or
         *          null if no parse tree should be created</returns>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual Node ExitText(Token node) {
            return node;
        }

        /**
         * <summary>Called when entering a parse tree node.</summary>
         *
         * <param name='node'>the node being entered</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual void EnterParameter(Token node) {
        }

        /**
         * <summary>Called when exiting a parse tree node.</summary>
         *
         * <param name='node'>the node being exited</param>
         *
         * <returns>the node to add to the parse tree, or
         *          null if no parse tree should be created</returns>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual Node ExitParameter(Token node) {
            return node;
        }

        /**
         * <summary>Called when entering a parse tree node.</summary>
         *
         * <param name='node'>the node being entered</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual void EnterStatementStart(Token node) {
        }

        /**
         * <summary>Called when exiting a parse tree node.</summary>
         *
         * <param name='node'>the node being exited</param>
         *
         * <returns>the node to add to the parse tree, or
         *          null if no parse tree should be created</returns>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual Node ExitStatementStart(Token node) {
            return node;
        }

        /**
         * <summary>Called when entering a parse tree node.</summary>
         *
         * <param name='node'>the node being entered</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual void EnterStatementEnd(Token node) {
        }

        /**
         * <summary>Called when exiting a parse tree node.</summary>
         *
         * <param name='node'>the node being exited</param>
         *
         * <returns>the node to add to the parse tree, or
         *          null if no parse tree should be created</returns>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual Node ExitStatementEnd(Token node) {
            return node;
        }

        /**
         * <summary>Called when entering a parse tree node.</summary>
         *
         * <param name='node'>the node being entered</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual void EnterCommand(Production node) {
        }

        /**
         * <summary>Called when exiting a parse tree node.</summary>
         *
         * <param name='node'>the node being exited</param>
         *
         * <returns>the node to add to the parse tree, or
         *          null if no parse tree should be created</returns>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual Node ExitCommand(Production node) {
            return node;
        }

        /**
         * <summary>Called when adding a child to a parse tree
         * node.</summary>
         *
         * <param name='node'>the parent node</param>
         * <param name='child'>the child node, or null</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual void ChildCommand(Production node, Node child) {
            node.AddChild(child);
        }

        /**
         * <summary>Called when entering a parse tree node.</summary>
         *
         * <param name='node'>the node being entered</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual void EnterStatement(Production node) {
        }

        /**
         * <summary>Called when exiting a parse tree node.</summary>
         *
         * <param name='node'>the node being exited</param>
         *
         * <returns>the node to add to the parse tree, or
         *          null if no parse tree should be created</returns>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual Node ExitStatement(Production node) {
            return node;
        }

        /**
         * <summary>Called when adding a child to a parse tree
         * node.</summary>
         *
         * <param name='node'>the parent node</param>
         * <param name='child'>the child node, or null</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual void ChildStatement(Production node, Node child) {
            node.AddChild(child);
        }

        /**
         * <summary>Called when entering a parse tree node.</summary>
         *
         * <param name='node'>the node being entered</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual void EnterExpression(Production node) {
        }

        /**
         * <summary>Called when exiting a parse tree node.</summary>
         *
         * <param name='node'>the node being exited</param>
         *
         * <returns>the node to add to the parse tree, or
         *          null if no parse tree should be created</returns>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual Node ExitExpression(Production node) {
            return node;
        }

        /**
         * <summary>Called when adding a child to a parse tree
         * node.</summary>
         *
         * <param name='node'>the parent node</param>
         * <param name='child'>the child node, or null</param>
         *
         * <exception cref='ParseException'>if the node analysis
         * discovered errors</exception>
         */
        public virtual void ChildExpression(Production node, Node child) {
            node.AddChild(child);
        }
    }
}

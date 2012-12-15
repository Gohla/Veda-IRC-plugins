/*
 * AliasGrammarParser.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

namespace Veda.Plugins.Alias.Grammar {

    /**
     * <remarks>A token stream parser.</remarks>
     */
    internal class AliasGrammarParser : RecursiveDescentParser {

        /**
         * <summary>An enumeration with the generated production node
         * identity constants.</summary>
         */
        private enum SynteticPatterns {
        }

        /**
         * <summary>Creates a new parser with a default analyzer.</summary>
         *
         * <param name='input'>the input stream to read from</param>
         *
         * <exception cref='ParserCreationException'>if the parser
         * couldn't be initialized correctly</exception>
         */
        public AliasGrammarParser(TextReader input)
            : base(input) {

            CreatePatterns();
        }

        /**
         * <summary>Creates a new parser.</summary>
         *
         * <param name='input'>the input stream to read from</param>
         *
         * <param name='analyzer'>the analyzer to parse with</param>
         *
         * <exception cref='ParserCreationException'>if the parser
         * couldn't be initialized correctly</exception>
         */
        public AliasGrammarParser(TextReader input, AliasGrammarAnalyzer analyzer)
            : base(input, analyzer) {

            CreatePatterns();
        }

        /**
         * <summary>Creates a new tokenizer for this parser. Can be overridden
         * by a subclass to provide a custom implementation.</summary>
         *
         * <param name='input'>the input stream to read from</param>
         *
         * <returns>the tokenizer created</returns>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        protected override Tokenizer NewTokenizer(TextReader input) {
            return new AliasGrammarTokenizer(input);
        }

        /**
         * <summary>Initializes the parser by creating all the production
         * patterns.</summary>
         *
         * <exception cref='ParserCreationException'>if the parser
         * couldn't be initialized correctly</exception>
         */
        private void CreatePatterns() {
            ProductionPattern             pattern;
            ProductionPatternAlternative  alt;

            pattern = new ProductionPattern((int) AliasGrammarConstants.COMMAND,
                                            "Command");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) AliasGrammarConstants.STATEMENT, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) AliasGrammarConstants.STATEMENT,
                                            "Statement");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) AliasGrammarConstants.EXPRESSION, 1, -1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) AliasGrammarConstants.STATEMENT_START, 1, 1);
            alt.AddProduction((int) AliasGrammarConstants.EXPRESSION, 1, -1);
            alt.AddToken((int) AliasGrammarConstants.STATEMENT_END, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) AliasGrammarConstants.EXPRESSION,
                                            "Expression");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) AliasGrammarConstants.TEXT, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) AliasGrammarConstants.STRING, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) AliasGrammarConstants.PARAMETER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);
        }
    }
}

/*
 * AliasGrammarTokenizer.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

namespace Veda.Plugins.Alias.Grammar {

    /**
     * <remarks>A character stream tokenizer.</remarks>
     */
    internal class AliasGrammarTokenizer : Tokenizer {

        /**
         * <summary>Creates a new tokenizer for the specified input
         * stream.</summary>
         *
         * <param name='input'>the input stream to read</param>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        public AliasGrammarTokenizer(TextReader input)
            : base(input, false) {

            CreatePatterns();
        }

        /**
         * <summary>Initializes the tokenizer by creating all the token
         * patterns.</summary>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        private void CreatePatterns() {
            TokenPattern  pattern;

            pattern = new TokenPattern((int) AliasGrammarConstants.STRING,
                                       "STRING",
                                       TokenPattern.PatternType.REGEXP,
                                       "\"([^\"\\\\$;\\[\\]]|\"\"|\\\\.)*\"");
            AddPattern(pattern);

            pattern = new TokenPattern((int) AliasGrammarConstants.TEXT,
                                       "TEXT",
                                       TokenPattern.PatternType.REGEXP,
                                       "[^ \\t\\n\\r\"$;\\[\\]]+");
            AddPattern(pattern);

            pattern = new TokenPattern((int) AliasGrammarConstants.PARAMETER,
                                       "PARAMETER",
                                       TokenPattern.PatternType.REGEXP,
                                       "\\$[1-9]");
            AddPattern(pattern);

            pattern = new TokenPattern((int) AliasGrammarConstants.COMMAND_START,
                                       "COMMAND_START",
                                       TokenPattern.PatternType.STRING,
                                       "[");
            AddPattern(pattern);

            pattern = new TokenPattern((int) AliasGrammarConstants.COMMAND_END,
                                       "COMMAND_END",
                                       TokenPattern.PatternType.STRING,
                                       "]");
            AddPattern(pattern);

            pattern = new TokenPattern((int) AliasGrammarConstants.COMMAND_SEPARATOR,
                                       "COMMAND_SEPARATOR",
                                       TokenPattern.PatternType.STRING,
                                       ";");
            AddPattern(pattern);

            pattern = new TokenPattern((int) AliasGrammarConstants.LAYOUT,
                                       "LAYOUT",
                                       TokenPattern.PatternType.REGEXP,
                                       "[ \\t\\n\\r]+");
            pattern.Ignore = true;
            AddPattern(pattern);
        }
    }
}

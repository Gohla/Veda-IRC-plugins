using System;
using System.Collections.Generic;
using Gohla.Shared;

namespace Veda.Plugins.MessageParser
{
    public class MessageParserData : IKeyedObject<String>
    {
        public readonly String Pattern;
        public readonly String Expression;

        public String Key
        {
            get { return Pattern; }
        }

        public MessageParserData(String pattern, String expression)
        {
            Pattern = pattern;
            Expression = expression;
        }

        public override bool Equals(object other)
        {
        	if(ReferenceEquals(other, null))
        		return false;

            return Equals(other as MessageParserData);
        }

        public bool Equals(MessageParserData other)
        {
        	if(ReferenceEquals(other, null))
        		return false;
        
        	return
                EqualityComparer<String>.Default.Equals(this.Pattern, other.Pattern)
        	 ;
        }
        
        public override int GetHashCode()
        {
        	unchecked
        	{
        		int hash = 17;
                hash = hash * 23 + EqualityComparer<String>.Default.GetHashCode(this.Pattern);
        		return hash;
        	}
        }
        
        public override String ToString()
        {
            return this.Pattern + " => " + this.Expression;
        }
    }
}

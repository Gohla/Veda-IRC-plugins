using System;
using System.Collections.Generic;

namespace Veda.Plugins.Alias
{
    public class AliasData
    {
        public readonly String Name;
        public readonly String Expression;

        public AliasData(String name, String expression)
        {
            Name = name;
            Expression = expression;
        }

        public override bool Equals(object other)
        {
        	if(ReferenceEquals(other, null))
        		return false;
        
        	return Equals(other as AliasData);
        }
        
        public bool Equals(AliasData other)
        {
        	if(ReferenceEquals(other, null))
        		return false;
        
        	return
        		EqualityComparer<String>.Default.Equals(this.Name, other.Name)
        	 && EqualityComparer<String>.Default.Equals(this.Expression, other.Expression)
        	 ;
        }
        
        public override int GetHashCode()
        {
        	unchecked
        	{
        		int hash = 17;
                hash = hash * 23 + EqualityComparer<String>.Default.GetHashCode(this.Name);
                hash = hash * 23 + EqualityComparer<String>.Default.GetHashCode(this.Expression);
        		return hash;
        	}
        }
        
        public override String ToString()
        {
        	return this.Expression;
        }
    }
}

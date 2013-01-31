using Veda.Interface;

namespace Veda.Plugins.Conditional
{
    [Plugin(Name = "Conditional", Description = "Provides basic conditional commands.")]
    public static class ConditionalPlugin
    {
        [Command(Description = "Determines whether the objects are considered equal.")]
        public static bool Equals(IContext context, object obj1, object obj2)
        {
            return Equals(obj1, obj2);
        }

        [Command(Description = "Returns the first object if condition true, the second object if condition is false.")]
        public static object IfElse(IContext context, bool condition, object @if, object @else)
        {
            if(condition)
                return @if;
            else
                return @else;
        }
    }
}

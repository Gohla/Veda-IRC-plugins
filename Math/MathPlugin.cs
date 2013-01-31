using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veda.Interface;

namespace Veda.Plugins.Mathemathics
{
    [Plugin(Name = "Math", Description = "Provides basic math commands.")]
    public static class MathPlugin
    {
        public static Random _random = new Random();

        [Command(Description = "Returns a nonnegative random integer less than given maximum.")]
        public static int Random(IContext context, int max)
        {
            return _random.Next(max);
        }

        [Command(Description = "Returns a random integer within a specified range.")]
        public static int Random(IContext context, int min, int max)
        {
            return _random.Next(min, max);
        }

        [Command(Description = "Returns a random double between 0.0 and 1.0.")]
        public static double Random(IContext context)
        {
            return _random.NextDouble();
        }
    }
}

using System.Collections.Generic;

namespace Gerontocracy.Shared.Extensions
{
    public static class GenericsExtensions
    {
        public static List<T> AsList<T>(this T obj)
            => new List<T> { obj };

        public static T[] AsArray<T>(this T obj)
            => new[] { obj };
    }
}

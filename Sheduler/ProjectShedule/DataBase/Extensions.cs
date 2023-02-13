using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.DataBase
{
    public static class Extensions
    {
        public static IEnumerable<T> DeepClone<T>(this IEnumerable<T> source) where T : ICloneable
        {
            return source.Select(item => (T)item.Clone()).ToArray();
        }
    }
}

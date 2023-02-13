using System;
using System.Collections.Generic;

namespace ProjectShedule.Core.Sorting
{
    public interface ISort
    {
        IEnumerable<T> Sort<T, TResult>(IEnumerable<T> item, Func<T, TResult> orderBy);
    }
}
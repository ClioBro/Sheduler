using System;
using System.Collections.Generic;

namespace ProjectShedule.Core.Sorting
{
    public abstract class OrderState : ISort
    {
        public abstract bool Descending { get; }
        public abstract IEnumerable<T> Sort<T, TResult>(IEnumerable<T> item, Func<T, TResult> orderBy);
        public abstract void SwichOrderBy(IHasOrderByState hasOrderByState);
    }
}
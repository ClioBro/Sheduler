using ProjectShedule.Core.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.States
{
    public class AscendingOrderByState : OrderState
    {
        public override bool Descending { get; } = false;

        public override IEnumerable<T> Sort<T, TResult>(IEnumerable<T> item, Func<T, TResult> orderBy)
        {
            return item.OrderBy(orderBy);
        }
        public override void SwichOrderBy(IHasOrderByState hasOrderByState)
        {
            hasOrderByState.OrderByState = new DescendingOrderByState();
        }
    }
}
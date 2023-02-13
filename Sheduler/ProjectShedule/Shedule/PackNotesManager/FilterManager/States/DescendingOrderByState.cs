using ProjectShedule.Core.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.States
{
    public class DescendingOrderByState : OrderState
    {
        public override bool Descending { get; } = true;

        public override IEnumerable<T> Sort<T, TResult>(IEnumerable<T> item, Func<T, TResult> orderBy)
        {
            return item.OrderByDescending(orderBy);
        }
        public override void SwichOrderBy(IHasOrderByState hasOrderByState)
        {
            hasOrderByState.OrderByState = new AscendingOrderByState();
        }
    }
}
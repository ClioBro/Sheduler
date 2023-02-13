using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ProjectShedule.Shedule.ViewModels.Base
{
    public class SmallTasksCollectionChangedEventArgs<T> : EventArgs
    {
        public SmallTasksCollectionChangedEventArgs(IEnumerable<T> oldItems, IEnumerable<T> newItems, NotifyCollectionChangedAction action)
        {
            Action = action;
            OldItems = oldItems;
            NewItems = newItems;
        }

        public IEnumerable<T> OldItems { get; }
        public IEnumerable<T> NewItems { get; }
        public NotifyCollectionChangedAction Action { get; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.Calendar.Controls
{
    public class EventCollection : Dictionary<DateTime, ICollection>
    {
        public EventCollection() : base()
        { }
        public EventCollection(int capacity) : base(capacity)
        { }
        public new bool Remove(DateTime key)
        {
            var removed = base.Remove(key.Date);

            if (removed)
                CollectionChanged?.Invoke(this, new EventCollectionChangedArgs { Item = key.Date, Type = EventCollectionChangedType.Remove });

            return removed;
        }
        public new void Add(DateTime key, ICollection value)
        {
            base.Add(key.Date, value);
            CollectionChanged?.Invoke(this, new EventCollectionChangedArgs { Item = key.Date, Type = EventCollectionChangedType.Add });
        }
        public new ICollection this[DateTime key]
        {
            get => base[key.Date];
            set
            {
                base[key.Date] = value;
                CollectionChanged?.Invoke(this, new EventCollectionChangedArgs { Item = key.Date, Type = EventCollectionChangedType.Set });
            }
        }
        public new bool ContainsKey(DateTime key)
        {
            return base.ContainsKey(key.Date);
        }

        public new bool TryGetValue(DateTime key, out ICollection value)
        {
            return base.TryGetValue(key.Date, out value);
        }
        public bool TryGetValues(ICollection<DateTime> keys, out ICollection values)
        {
            var listToReturn = new List<object>();

            foreach (var currentDate in keys)
                if (base.TryGetValue(currentDate, out var dayEvents))
                    foreach (var singleEvent in dayEvents)
                        listToReturn.Add(singleEvent);

            if (listToReturn.Count > 0)
            {
                values = listToReturn;
                return true;
            }
            else
            {
                values = null;
                return false;
            }
        }
        public new void Clear()
        {
            if (Count == 0)
                return;

            base.Clear();
            CollectionChanged?.Invoke(this, new EventCollectionChangedArgs { Item = default, Type = EventCollectionChangedType.Clear });
        }

        internal event EventHandler<EventCollectionChangedArgs> CollectionChanged;

        internal class EventCollectionChangedArgs
        {
            public DateTime Item { get; set; }
            public EventCollectionChangedType Type { get; set; }
        }

        internal enum EventCollectionChangedType
        {
            Add,
            Set,
            Remove,
            Clear
        }
    }
}

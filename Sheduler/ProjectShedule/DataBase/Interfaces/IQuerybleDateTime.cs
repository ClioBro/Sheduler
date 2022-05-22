using System;
using System.Collections.Generic;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface IQuerybleDateTime<T>
    {
        public List<T> GetForDate(DateTime dateTime);
        public List<T> GetForDates(IEnumerable<DateTime> dateTimes);
        public List<T> GetForRangeDate(DateTime from, DateTime till);
    }
}

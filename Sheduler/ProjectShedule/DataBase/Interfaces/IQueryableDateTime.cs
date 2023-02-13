using ProjectShedule.Core;
using System;
using System.Collections.Generic;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface IGetByDateTime<out T>
    {
        public IEnumerable<T> GetByDate(DateTime dateTime);
        public IEnumerable<T> GetByDates(IEnumerable<DateTime> dateTimes);
        public IEnumerable<T> GetByDateRange(DateTime from, DateTime till);
        public IEnumerable<T> GetByDateRange(DateTimeRange dateTimeRange);
    }
}

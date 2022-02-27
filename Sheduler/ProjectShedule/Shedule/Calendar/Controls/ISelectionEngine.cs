using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace ProjectShedule.Shedule.Calendar.Controls
{
    internal interface ISelectionEngine
    {
        string GetSelectedDateText(string selectedDateTextFormat, CultureInfo culture);
        bool TryGetSelectedEvents(EventCollection allEvents, out ICollection selectedEvents);
        bool IsDateSelected(DateTime dateToCheck);

        List<DateTime> PerformDateSelection(DateTime dateToSelect);
        void UpdateDateSelection(List<DateTime> datesToSelect);
    }
}

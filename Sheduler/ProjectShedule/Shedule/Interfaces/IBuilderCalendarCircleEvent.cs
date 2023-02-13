using ProjectShedule.Core;
using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.ShapeEvents
{
    public interface IBuilderCalendarCircleEvent : IBuilder<IEnumerable<CircleEventModel>>
    {
        public IBuilderCalendarCircleEvent SetRange(DateTime start, DateTime end);
        public IBuilderCalendarCircleEvent SetRange(DateTimeRange dateTimeRange);
    }
}

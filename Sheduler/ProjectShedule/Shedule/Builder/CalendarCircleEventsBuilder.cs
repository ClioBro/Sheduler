using ProjectShedule.Core;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.GlobalSetting.Settings.SheduleEvents;
using ProjectShedule.Shedule.Builder.Base;
using ProjectShedule.Shedule.Calendar.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ShapeEvents
{
    public class CalendarCircleEventsBuilder : IBuilderCalendarCircleEvent
    {
        private readonly ISimpleShape _shapeEventSetting;
        private readonly IGetItemsDateTime<INote> _noteDataBaseRepository;
        private readonly BaseCircleEventModelBuilder<CircleEventModel> _circleEventModelBuilder;
        private DateTimeRange? _dateTimeRange;
        public CalendarCircleEventsBuilder(IGetItemsDateTime<INote> noteDateBaseRepository)
        {
            _noteDataBaseRepository = noteDateBaseRepository;

            _shapeEventSetting = new ShapeEventSetting();
            _circleEventModelBuilder = new CircleEventModelBuilder();
        }
        public IEnumerable<CircleEventModel> Build()
        {
            IEnumerable<INote> packNoteModels;
            packNoteModels = _dateTimeRange is null
                ? _noteDataBaseRepository.GetAllItems()
                : GetNotesByDateRange();
            return GetEvents(packNoteModels);
        }
        public IBuilderCalendarCircleEvent SetRange(DateTime start, DateTime end)
        {
            _dateTimeRange = new DateTimeRange(start, end);
            return this;
        }
        public IBuilderCalendarCircleEvent SetRange(DateTimeRange dateTimeRange)
        {
            _dateTimeRange = dateTimeRange;
            return this;
        }

        private IEnumerable<INote> GetNotesByDateRange()
        {
            return _noteDataBaseRepository.GetByDateRange(_dateTimeRange.Value.Start, _dateTimeRange.Value.End);
        }
        private IEnumerable<CircleEventModel> GetEvents(IEnumerable<INote> notes)
        {
            List<CircleEventModel> anyEvents = new List<CircleEventModel>();

            Size size = _shapeEventSetting.GetSize();
            double opacity = _shapeEventSetting.GetOpacity();
            float cornerRadius = _shapeEventSetting.GetCornerRadius();

            foreach (INote note in notes)
            {
                anyEvents.Add(_circleEventModelBuilder
                .SetId(note.Id)
                .SetDateTime(note.AppointmentDate.Value)
                .SetBackGroundColor(Color.FromHex(note.BackgroundColorKey))
                .SetBorderColor(Color.FromHex(note.LineColorKey))
                .SetSize(size)
                .SetCornerRadius(cornerRadius)
                .SetOpacity(opacity)
                .SetVisible(true)
                .Build());
            }

            return anyEvents;
        }
    }
}

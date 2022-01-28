using ProjectShedule.Calendar.Models;
using ProjectShedule.GlobalSetting;
using ProjectShedule.Shedule.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ShapeEvents
{
    public class SheduleEventsCreater
    {
        private readonly ISimpleShape _shapeEventSetting;
        public SheduleEventsCreater()
        {
            _shapeEventSetting = new ShapeEventSetting();
        }
        public IEnumerable<ICircleEvent> Create()
        {
            var manager = new PackNoteDBManager();
            List<PackNoteModel> packNoteModels = manager.GetAll();

            return GetEvents(packNoteModels);
        }
        public IEnumerable<ICircleEvent> Create(DateTime start, DateTime end)
        {
            var manager = new PackNoteDBManager();
            List<PackNoteModel> packNoteModels = manager.GetForDate(start, end);

            return GetEvents(packNoteModels);
        }
        private IEnumerable<ICircleEvent> GetEvents(List<PackNoteModel> packNoteModels)
        {
            List<ICircleEvent> anyEvents = new List<ICircleEvent>();

            Size size = _shapeEventSetting.GetSize();
            double opacity = _shapeEventSetting.GetOpacity();
            float cornerRadius = _shapeEventSetting.GetCornerRadius();

            foreach (var packNoteModel in packNoteModels)
            {
                anyEvents.Add(
                    new CircleEventModel()
                    {
                        DateTime = packNoteModel.Note.AppointmentDate,
                        BackGrountColor = packNoteModel.BackGroundColor,
                        BorderColor = packNoteModel.LineColor,
                        Size = size,
                        CornerRadius = cornerRadius,
                        Opacity = opacity,
                        IsVisible = true
                    });
            }
            return anyEvents;
        }

    }
}

using ProjectShedule.Calendar.Models;
using ProjectShedule.DataNote;
using ProjectShedule.GlobalSetting.Settings.SheduleEvents;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ShapeEvents
{
    public class SheduleEventsCreater
    {
        private readonly ISimpleShape _shapeEventSetting;
        private readonly DataBase.PackNoteData _packNoteData;
        public SheduleEventsCreater()
        {
            _shapeEventSetting = new ShapeEventSetting();
            _packNoteData = App.SchedulerPackNoteDataBase;
        }
        public IEnumerable<ICircleEvent> Create()
        {
            List<Note> notes = _packNoteData.Note.GetItems();

            return GetEvents(notes);
        }
        public IEnumerable<ICircleEvent> Create(DateTime start, DateTime end)
        {
            IQuerybleDateTime<Note> quereble = _packNoteData.Note as IQuerybleDateTime<Note>;
            List<Note> packNoteModels = quereble.Query(start, end);

            return GetEvents(packNoteModels);
        }
        private IEnumerable<ICircleEvent> GetEvents(IEnumerable<INote> notes)
        {
            List<ICircleEvent> anyEvents = new List<ICircleEvent>();

            Size size = _shapeEventSetting.GetSize();
            double opacity = _shapeEventSetting.GetOpacity();
            float cornerRadius = _shapeEventSetting.GetCornerRadius();

            foreach (var note in notes)
            {
                anyEvents.Add(
                    new CircleEventModel()
                    {
                        DateTime = note.AppointmentDate,
                        BackGrountColor = Color.FromHex(note.BackgroundColorKey),
                        BorderColor = Color.FromHex(note.LineColorKey),
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

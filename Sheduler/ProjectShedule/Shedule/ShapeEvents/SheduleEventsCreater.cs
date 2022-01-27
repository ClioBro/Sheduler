using ProjectShedule.Calendar.Models;
using ProjectShedule.GlobalSetting;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.PackNotesManager.FilterManager;
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
            ISortInDate<SortInDate> sort = new SortInDate();
            List<PackNoteModel> packNoteModels = sort.GetItems();

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

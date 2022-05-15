using ProjectShedule.DataBase.Entities;
using ProjectShedule.DataBase.Entities.Base;
using ProjectShedule.DataBase.Repositories;
using ProjectShedule.GlobalSetting.Settings.SheduleEvents;
using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ShapeEvents
{
    public abstract class BaseSheduleCircleEventBuilder : IBuilder<CircleEventModel>
    {
        protected DateTime _dateTime;
        protected Color _backGrounColor;
        protected Color _borderColor;
        protected Size _size;
        protected float _cornerRadius;
        protected double _opacity;
        protected bool _isVisible;
        public abstract CircleEventModel Build();
        public virtual BaseSheduleCircleEventBuilder SetDateTime(DateTime dateTime)
        {
            _dateTime = dateTime;
            return this;
        }
        public virtual BaseSheduleCircleEventBuilder SetBackGroundColor(Color color)
        {
            _backGrounColor = color;
            return this;
        }
        public virtual BaseSheduleCircleEventBuilder SetBorderColor(Color color)
        {
            _borderColor = color;
            return this;
        }
        public virtual BaseSheduleCircleEventBuilder SetSize(Size size)
        {
            _size = size;
            return this;
        }
        public virtual BaseSheduleCircleEventBuilder SetCornerRadius(float cornerRadius)
        {
            _cornerRadius = cornerRadius;
            return this;
        }
        public virtual BaseSheduleCircleEventBuilder SetOpacity(double opacity)
        {
            _opacity = opacity;
            return this;
        }
        public virtual BaseSheduleCircleEventBuilder SetVisible(bool visible)
        {
            _isVisible = visible;
            return this;
        }
    }
    public class SheduleCircleEventModelBuilder : BaseSheduleCircleEventBuilder
    {
        public override CircleEventModel Build()
        {
            return new CircleEventModel()
            {
                DateTime = _dateTime,
                BackGrountColor = _backGrounColor,
                BorderColor = _borderColor,
                Size = _size,
                CornerRadius = _cornerRadius,
                Opacity = _opacity,
                IsVisible = _isVisible
            };
        }
    }
    public interface IBuilderCalendarCircleEvent
    {
        public IEnumerable<CircleEventModel> Build();
        public IEnumerable<CircleEventModel> Build(DateTime start, DateTime end);
    }
    public class BuilderCalendarCircleEvents : IBuilderCalendarCircleEvent
    {
        private readonly ISimpleShape _shapeEventSetting;
        private readonly INoteRepository _noteDataBaseRepository;
        private readonly BaseSheduleCircleEventBuilder _circleEventBuilder;
        public BuilderCalendarCircleEvents(INoteRepository noteDateBaseRepository)
        {
            _shapeEventSetting = new ShapeEventSetting();
            _circleEventBuilder = new SheduleCircleEventModelBuilder();
            _noteDataBaseRepository = noteDateBaseRepository;
        }
        public IEnumerable<CircleEventModel> Build()
        {
            return GetEvents(_noteDataBaseRepository.GetItems());
        }
        public IEnumerable<CircleEventModel> Build(DateTime start, DateTime end)
        {
            List<Note> packNoteModels = _noteDataBaseRepository.GetForDateTime(start, end);

            return GetEvents(packNoteModels);
        }
        private IEnumerable<CircleEventModel> GetEvents(IEnumerable<BaseNote> notes)
        {
            List<CircleEventModel> anyEvents = new List<CircleEventModel>();

            Size size = _shapeEventSetting.GetSize();
            double opacity = _shapeEventSetting.GetOpacity();
            float cornerRadius = _shapeEventSetting.GetCornerRadius();

            foreach (var note in notes)
            {
                anyEvents.Add(_circleEventBuilder
                .SetDateTime(note.AppointmentDate)
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

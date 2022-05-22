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
    public abstract class BaseCircleEventModelBuilder : IBuilder<CircleEventModel>
    {
        protected int _id;
        protected DateTime _dateTime;
        protected Color _backGrounColor;
        protected Color _borderColor;
        protected Size _size;
        protected float _cornerRadius;
        protected double _opacity;
        protected bool _isVisible;
        public abstract CircleEventModel Build();
        public virtual BaseCircleEventModelBuilder SetId(int id)
        {
            _id = id;
            return this;
        }
        public virtual BaseCircleEventModelBuilder SetDateTime(DateTime dateTime)
        {
            _dateTime = dateTime;
            return this;
        }
        public virtual BaseCircleEventModelBuilder SetBackGroundColor(Color color)
        {
            _backGrounColor = color;
            return this;
        }
        public virtual BaseCircleEventModelBuilder SetBorderColor(Color color)
        {
            _borderColor = color;
            return this;
        }
        public virtual BaseCircleEventModelBuilder SetSize(Size size)
        {
            _size = size;
            return this;
        }
        public virtual BaseCircleEventModelBuilder SetCornerRadius(float cornerRadius)
        {
            _cornerRadius = cornerRadius;
            return this;
        }
        public virtual BaseCircleEventModelBuilder SetOpacity(double opacity)
        {
            _opacity = opacity;
            return this;
        }
        public virtual BaseCircleEventModelBuilder SetVisible(bool visible)
        {
            _isVisible = visible;
            return this;
        }
    }
    public class SheduleCircleEventModelBuilder : BaseCircleEventModelBuilder
    {
        public override CircleEventModel Build()
        {
            return new CircleEventModel()
            {
                ID = _id,
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
    public interface IBuilderCalendarCircleEvent : IBuilder<IEnumerable<CircleEventModel>>
    {
        public IEnumerable<CircleEventModel> Build(DateTime start, DateTime end);
    }
    public class BuilderCalendarCircleEvents : IBuilderCalendarCircleEvent
    {
        private readonly ISimpleShape _shapeEventSetting;
        private readonly INoteRepository _noteDataBaseRepository;
        private readonly BaseCircleEventModelBuilder _circleEventModelBuilder;
        public BuilderCalendarCircleEvents(INoteRepository noteDateBaseRepository)
        {
            _noteDataBaseRepository = noteDateBaseRepository;
            _shapeEventSetting = new ShapeEventSetting();
            _circleEventModelBuilder = new SheduleCircleEventModelBuilder();
        }
        public IEnumerable<CircleEventModel> Build()
        {
            return GetEvents(_noteDataBaseRepository.GetItems());
        }
        public IEnumerable<CircleEventModel> Build(DateTime start, DateTime end)
        {
            List<Note> packNoteModels = _noteDataBaseRepository.GetForRangeDate(start, end);

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
                anyEvents.Add(_circleEventModelBuilder
                .SetId(note.Id)
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

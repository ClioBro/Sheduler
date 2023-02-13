using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.Interfaces;
using System;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Builder.Base
{
    public abstract class BaseCircleEventModelBuilder<T> : IBuilder<T> where T : CircleEventModel, new()
    {
        private int _id;
        private DateTime _dateTime;
        private Color _backGrounColor;
        private Color _borderColor;
        private Size _size;
        private float _cornerRadius;
        private double _opacity;
        private bool _isVisible;

        public int ID => _id;
        public DateTime DateTime => _dateTime;
        public Color ColorColor => _backGrounColor;
        public Color BorderColor => _borderColor;
        public Size Size => _size;
        public float CornerRadius => _cornerRadius;
        public double Opacity => _opacity;
        public bool IsVisible => _isVisible;

        public virtual T Build()
        {
            return new T()
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
        public virtual BaseCircleEventModelBuilder<T> SetId(int id)
        {
            _id = id;
            return this;
        }
        public virtual BaseCircleEventModelBuilder<T> SetDateTime(DateTime dateTime)
        {
            _dateTime = dateTime;
            return this;
        }
        public virtual BaseCircleEventModelBuilder<T> SetBackGroundColor(Color color)
        {
            _backGrounColor = color;
            return this;
        }
        public virtual BaseCircleEventModelBuilder<T> SetBorderColor(Color color)
        {
            _borderColor = color;
            return this;
        }
        public virtual BaseCircleEventModelBuilder<T> SetSize(Size size)
        {
            _size = size;
            return this;
        }
        public virtual BaseCircleEventModelBuilder<T> SetCornerRadius(float cornerRadius)
        {
            _cornerRadius = cornerRadius;
            return this;
        }
        public virtual BaseCircleEventModelBuilder<T> SetOpacity(double opacity)
        {
            _opacity = opacity;
            return this;
        }
        public virtual BaseCircleEventModelBuilder<T> SetVisible(bool visible)
        {
            _isVisible = visible;
            return this;
        }
    }
}

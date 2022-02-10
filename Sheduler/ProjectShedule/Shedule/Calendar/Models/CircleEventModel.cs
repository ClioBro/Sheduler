using System;
using Xamarin.Forms;

namespace ProjectShedule.Calendar.Models
{
    public class CircleEventModel : ICircleEvent
    {
        public DateTime DateTime { get; set; }
        public Color BackGrountColor { get; set; }
        public Color BorderColor { get; set; }

        public bool IsVisible { get; set; }
        public double Opacity { get; set; }
        public Size Size { get; set; }
        public float CornerRadius { get; set; }
    }
    public interface ICircleEvent : ICircle, IEvent
    {

    }
    public interface ICircle
    {
        public Color BackGrountColor { get; set; }
        public Color BorderColor { get; set; }

        public bool IsVisible { get; set; }
        public double Opacity { get; set; }
        public Size Size { get; set; }
        public float CornerRadius { get; set; }
    }
    public interface IEvent
    {
        public DateTime DateTime { get; set; }
    }
}

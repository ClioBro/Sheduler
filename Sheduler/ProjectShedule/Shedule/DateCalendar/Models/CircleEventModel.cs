using System;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Calendar.Models
{
    public class CircleEventModel
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public Color BackGrountColor { get; set; }
        public Color BorderColor { get; set; }

        public bool IsVisible { get; set; }
        public double Opacity { get; set; }
        public Size Size { get; set; }
        public float CornerRadius { get; set; }
    }
}

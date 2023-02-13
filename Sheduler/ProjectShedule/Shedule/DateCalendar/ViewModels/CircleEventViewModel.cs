using ProjectShedule.Core;
using ProjectShedule.Shedule.Calendar.Models;
using System.ComponentModel;

namespace ProjectShedule.Shedule.Calendar.ViewModels
{
    public class CircleEventViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly CircleEventModel _circleEventModel;

        public CircleEventViewModel(CircleEventModel circleEventModel)
        {
            _circleEventModel = circleEventModel;
        }

        public double Opacity
        {
            get => _circleEventModel.Opacity;
            set
            {
                if (_circleEventModel.Opacity != value)
                {
                    _circleEventModel.Opacity = value;
                    OnPropertyChanged(this);
                }
            }
        }
        public double Size
        {
            get => _circleEventModel.Size.Height;
            set
            {
                if (_circleEventModel.Size.Height != value)
                {
                    _circleEventModel.Size = new Xamarin.Forms.Size(value, value);
                    OnPropertyChanged(this);
                }
            }
        }
        public float CornerRadius
        {
            get => _circleEventModel.CornerRadius;
            set
            {
                if (_circleEventModel.CornerRadius != value)
                {
                    _circleEventModel.CornerRadius = value;
                    OnPropertyChanged(this);
                }
            }
        }
    }
}

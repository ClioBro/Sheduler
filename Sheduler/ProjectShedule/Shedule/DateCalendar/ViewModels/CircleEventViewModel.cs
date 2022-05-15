using ProjectShedule.Shedule.Calendar.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectShedule.Shedule.Calendar.ViewModels
{
    public class CircleEventViewModel : INotifyPropertyChanged
    {
        private readonly CircleEventModel _circleEventModel;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }
    }
}

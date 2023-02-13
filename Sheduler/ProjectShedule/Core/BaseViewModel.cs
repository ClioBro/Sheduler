using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectShedule.Core
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(BaseViewModel baseViewModel, [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(baseViewModel, new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged(BaseViewModel baseViewModel, params string[] propertyNames)
        {
            for (int index = 0; propertyNames.Length > index; index++)
            {
                OnPropertyChanged(baseViewModel, propertyNames[index]);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged(params string[] propertyNames)
        {
            for (int index = 0; propertyNames.Length > index; index++)
            {
                OnPropertyChanged(propertyNames[index]);
            }
        }

    }
}

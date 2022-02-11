using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectShedule.GlobalSetting.ViewModel
{
    public class SettingBoxViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public string Header { get; set; }

    }
}

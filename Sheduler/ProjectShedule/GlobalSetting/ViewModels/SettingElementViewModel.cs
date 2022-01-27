using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectShedule.GlobalSetting.ViewModels
{
    public class SettingElementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Title { get; set; }
        public string MainText { get; set; }
        protected virtual void OnPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}

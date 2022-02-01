using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectShedule.GlobalSetting.Models
{
    public class SwitchsSettingModel : INotifyPropertyChanged
    {
        private bool _status;
        public event EventHandler<bool> StatusChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public string MainText { get; set; }
        public bool Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    StatusChanged?.Invoke(this, value);
                }
            } 
        }
        protected void OnPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}

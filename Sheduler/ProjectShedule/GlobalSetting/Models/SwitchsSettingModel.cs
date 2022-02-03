using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectShedule.GlobalSetting.Models
{
    public class SwitchsSettingModel : INotifyPropertyChanged
    {
        private bool _status;
        private readonly string _falseText;
        private readonly string _trueText;
        public event EventHandler<bool> StatusChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public SwitchsSettingModel() : this("False", "True") { }
        public SwitchsSettingModel(string falseText, string trueText)
        {
            _falseText = falseText;
            _trueText = trueText;
        }

        public string MainText { get; set; }
        public string StatusText { get; set; }
        public bool Status
        {
            get => _status;
            set
            {
                _status = value;
                StatusText = value is true ? _trueText : _falseText;
                StatusChanged?.Invoke(this, value);
            } 
        }
        protected void OnPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using ProjectShedule.Core;

namespace ProjectShedule.GlobalSetting.ViewModel
{
    public abstract class SettingBoxViewModel : BaseViewModel
    {
        private string _header = "Header:";

        public string Header
        {
            get => _header;
            set
            {
                if (_header != value)
                {
                    _header = value;
                    OnPropertyChanged(this, nameof(Header));
                }
            }
        }
    }
}

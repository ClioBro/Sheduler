using ProjectShedule.Core;
using ProjectShedule.GlobalSetting.Models;

namespace ProjectShedule.GlobalSetting.Base.ViewModel
{
    public class SwitchSettingViewModel : BaseViewModel, IElementCell<bool>
    {
        private readonly SwitchSettingModel _switchSettingModel;

        public SwitchSettingViewModel(SwitchSettingModel switchSettingModel)
        {
            _switchSettingModel = switchSettingModel;
            _switchSettingModel.ValueChanged += OnSwitchSettingModel_ValueChanged;
        }

        public string MainText => _switchSettingModel.MainText;
        public string ValueText => _switchSettingModel.ValueText;
        public bool Value
        {
            get => _switchSettingModel.Value;
            set
            {
                if (_switchSettingModel.Value == value)
                    return;
                _switchSettingModel.Value = value;
            }
        }

        private void OnSwitchSettingModel_ValueChanged(object sender, bool e)
        {
            OnPropertyChanged(this, nameof(Value), nameof(ValueText));
        }
    }
}

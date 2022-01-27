using System;

namespace ProjectShedule.GlobalSetting.ViewModels
{
    public class SwitchSettingViewModel : SettingElementViewModel
    {
        private bool _askQuestion;
        public Action<bool> ActionChangedSwipe;
        public bool SwipeValue
        {
            get => _askQuestion; 
            set
            {
                _askQuestion = value;
                ActionChangedSwipe?.Invoke(value);
            }
        }
    }
}
using ProjectShedule.Core;
using ProjectShedule.GlobalSetting.Base;
using ProjectShedule.GlobalSetting.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.Base.ViewModel
{
    public class SlideSettingViewModel : BaseViewModel, IElementCell<double>
    {
        private readonly BaseSlideSettingModel _slideSettingModel;

        public SlideSettingViewModel(BaseSlideSettingModel slideSettingModel)
        {
            _slideSettingModel = slideSettingModel;
            DragCompletedCommand = new Command(_slideSettingModel.SaveSettings);
        }

        public string MainText => _slideSettingModel.MainText;
        public double MaxValue => _slideSettingModel.MaxValue;
        public double MinValue => _slideSettingModel.MinValue;
        public double Value
        {
            get => _slideSettingModel.Value;
            set
            {
                if (_slideSettingModel.Value == value)
                    return;
                _slideSettingModel.Value = value;
                OnPropertyChanged(this, nameof(Value));
            }
        }
        public ICommand DragCompletedCommand { get; }
    }
}

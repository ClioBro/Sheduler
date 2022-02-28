using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models;
using ProjectShedule.GlobalSetting.ViewModel;
using ProjectShedule.Language.Resources.Pages.Setting;
using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.Calendar.ViewModels;
using System.ComponentModel;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.ViewModels
{
    internal class SheduleEventsSettingViewModel : SettingBox, INotifyPropertyChanged
    {
        #region
        public event PropertyChangedEventHandler PropertyChanged; 
        private void OnPropertyChanged(object sender, string propName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private readonly ShapeEventSetting _shapeSetting;

        public SheduleEventsSettingViewModel()
        {
            _shapeSetting = new ShapeEventSetting();

            Header = SettingResources.EventsHeaderLabel;

            OpacityEventSettingModel = new OpacityEventSettingModel(_shapeSetting);
            CornerRadiusEventSettingModel = new CornerRadiusEventSettingModel(_shapeSetting);
            SizeEventSettingModel = new SizeEventSettingModel(_shapeSetting);

            CircleEventViewModel = new CircleEventViewModel(new CircleEventModel());

            OpacityEventSettingModel.ValueChanged += (object sender, double result) =>
            {
                CircleEventViewModel.Opacity = OpacityEventSettingModel.ConvertToMemory(result, _shapeSetting.MaxOpacity);
            };
            CornerRadiusEventSettingModel.ValueChanged += (object sender, double result) =>
            {
                CircleEventViewModel.CornerRadius = (float)CornerRadiusEventSettingModel.ConvertToMemory(result, _shapeSetting.MaxCornerRadius);
            };
            SizeEventSettingModel.ValueChanged += (object sender, double result) =>
            {
                CircleEventViewModel.Size = SizeEventSettingModel.ConvertToMemory(result, _shapeSetting.MaxSize);
            };
        }
        public CircleEventViewModel CircleEventViewModel { get; set; }
        public SlideSettingModel OpacityEventSettingModel { get; set; }
        public SlideSettingModel CornerRadiusEventSettingModel { get; set; }
        public SlideSettingModel SizeEventSettingModel { get; set; }
        
    }
}

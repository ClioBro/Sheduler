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
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ShapeEventSetting _shapeSetting;

        public SheduleEventsSettingViewModel()
        {
            _shapeSetting = new ShapeEventSetting();

            Header = SettingResources.EventsHeaderLabel;

            OpacityEventSettingModel = new OpacityEventSettingModel(_shapeSetting);
            CornerRadiusEventSettingModel = new CornerRadiusEventSettingModel(_shapeSetting);
            SizeEventSettingModel = new SizeEventSettingModel(_shapeSetting);

            CircleEventModel = new CircleEventViewModel(new CircleEventModel());
            OpacityEventSettingModel.ValueChanged += (object sender, double result) =>
            {
                CircleEventModel.Opacity = OpacityEventSettingModel.ConvertToMemory(result, _shapeSetting.MaxOpacity);
            };
            CornerRadiusEventSettingModel.ValueChanged += (object sender, double result) =>
            {
                CircleEventModel.CornerRadius = (float)CornerRadiusEventSettingModel.ConvertToMemory(result, _shapeSetting.MaxCornerRadius);
            };
            SizeEventSettingModel.ValueChanged += (object sender, double result) =>
            {
                CircleEventModel.Size = SizeEventSettingModel.ConvertToMemory(result, _shapeSetting.MaxSize);
            };
        }
        public CircleEventViewModel CircleEventModel { get; set; }
        public SlideSettingModel OpacityEventSettingModel { get; set; }
        public SlideSettingModel CornerRadiusEventSettingModel { get; set; }
        public SlideSettingModel SizeEventSettingModel { get; set; }
        private void OnPropertyChanged(object sender, string propName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propName));
        }
    }
}

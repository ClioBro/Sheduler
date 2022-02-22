using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models;
using ProjectShedule.GlobalSetting.ViewModel;
using ProjectShedule.Language.Resources.Pages.Setting;
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

            OpacityEventSettingModel.ValueChanged += (object sender, double result) =>
            {
                Opacity = PercentConverter.Convert(result, _shapeSetting.MaxOpacity);
                OnPropertyChanged(this, nameof(Opacity));
            };
            CornerRadiusEventSettingModel.ValueChanged += (object sender, double result) => 
            {
                CornerRadius = PercentConverter.Convert(result, _shapeSetting.MaxCornerRadius);
                OnPropertyChanged(this, nameof(CornerRadius));
            };
            SizeEventSettingModel.ValueChanged += (object sender, double result) => 
            {
                Size = PercentConverter.Convert(result, _shapeSetting.MaxSize);
                OnPropertyChanged(this, nameof(Size));
            };
        }
        public double Opacity { get; set; }
        public double CornerRadius { get; set; }
        public double Size { get; set; }
        public SlideSettingModel OpacityEventSettingModel { get; set; }
        public SlideSettingModel CornerRadiusEventSettingModel { get; set; }
        public SlideSettingModel SizeEventSettingModel { get; set; }
        private void OnPropertyChanged(object sender, string propName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propName));
        }
    }
}

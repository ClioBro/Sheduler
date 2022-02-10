using ProjectShedule.GlobalSetting.Resource;
using ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models;
using ProjectShedule.GlobalSetting.ViewModels;
using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.ViewModels
{
    internal class SheduleEventsSettingViewModel : SettingViewModel
    {
        private readonly ShapeEventSetting _shapeSetting;
        public SheduleEventsSettingViewModel()
        {
            _shapeSetting = new ShapeEventSetting();

            Title = SettingResources.EventsHeaderLabel;

            OpacityEventSettingModel = new OpacityEventSettingModel(_shapeSetting);
            CornerRadiusEventSettingModel = new CornerRadiusEventSettingModel(_shapeSetting);
            SizeEventSettingModel = new SizeEventSettingModel(_shapeSetting);
        }
        public DataTemplate DataTemplateItems { get; set; }

        public OpacityEventSettingModel OpacityEventSettingModel { get; set; }
        public CornerRadiusEventSettingModel CornerRadiusEventSettingModel { get; set; }
        public SizeEventSettingModel SizeEventSettingModel { get; set; }
    }
}

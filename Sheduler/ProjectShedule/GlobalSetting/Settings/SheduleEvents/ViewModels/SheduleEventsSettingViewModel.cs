using ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models;
using ProjectShedule.GlobalSetting.ViewModel;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.ViewModels
{
    internal class SheduleEventsSettingViewModel : SettingBoxViewModel
    {
        private readonly ShapeEventSetting _shapeSetting;
        public SheduleEventsSettingViewModel()
        {
            _shapeSetting = new ShapeEventSetting();

            Header = SettingResources.EventsHeaderLabel;

            OpacityEventSettingModel = new OpacityEventSettingModel(_shapeSetting);
            CornerRadiusEventSettingModel = new CornerRadiusEventSettingModel(_shapeSetting);
            SizeEventSettingModel = new SizeEventSettingModel(_shapeSetting);
        }

        public OpacityEventSettingModel OpacityEventSettingModel { get; set; }
        public CornerRadiusEventSettingModel CornerRadiusEventSettingModel { get; set; }
        public SizeEventSettingModel SizeEventSettingModel { get; set; }
    }
}

using ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.ViewModels
{
    internal class SheduleEventsSettingViewModel
    {
        private readonly ShapeEventSetting _shapeSetting;
        public SheduleEventsSettingViewModel()
        {
            _shapeSetting = new ShapeEventSetting();

            OpacityEventSettingModel = new OpacityEventSettingModel(_shapeSetting);
            CornerRadiusEventSettingModel = new CornerRadiusEventSettingModel(_shapeSetting);
            SizeEventSettingModel = new SizeEventSettingModel(_shapeSetting);
        }

        public string Title { get; set; } = "Events:";

        public OpacityEventSettingModel OpacityEventSettingModel { get; set; }
        public CornerRadiusEventSettingModel CornerRadiusEventSettingModel { get; set; }
        public SizeEventSettingModel SizeEventSettingModel { get; set; }
    }
}

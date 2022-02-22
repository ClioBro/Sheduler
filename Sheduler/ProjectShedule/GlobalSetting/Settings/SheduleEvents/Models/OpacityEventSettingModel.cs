using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.Language.Resources.Pages.Setting;
using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models
{
    public class OpacityEventSettingModel : SlideSettingModel
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public OpacityEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;
            MainText = SettingResources.OpacityDopTextLabel;
            MaxValue = 100d;
            MinValue = 0d;
            Value = PercentConverter.DeConvert(_shapeEventSetting.GetOpacity(), _shapeEventSetting.MaxOpacity);
            DragCompletedCommand = new Command(() => SaveOnMemory(Value));
        }
        private void SaveOnMemory(double value)
        {
            double result = PercentConverter.Convert(value, _shapeEventSetting.MaxOpacity);
            _shapeEventSetting.SetOpacity(result);
        }
    }
}

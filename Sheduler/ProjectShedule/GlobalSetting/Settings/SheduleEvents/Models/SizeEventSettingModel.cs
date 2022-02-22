using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.Language.Resources.Pages.Setting;
using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models
{
    public class SizeEventSettingModel : SlideSettingModel
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public SizeEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;
            MainText = SettingResources.SizeDopTextLabel;
            MaxValue = 100d;
            MinValue = 0d;
            Value = PercentConverter.DeConvert(_shapeEventSetting.GetSize().Height, _shapeEventSetting.MaxSize);
            DragCompletedCommand = new Command(() => SaveOnMemory(Value));
        }
        private void SaveOnMemory(double value)
        {
            double result = PercentConverter.Convert(value, _shapeEventSetting.MaxSize);
            _shapeEventSetting.SetSize(result);
        }
    }
}

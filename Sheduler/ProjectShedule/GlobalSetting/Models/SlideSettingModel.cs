using System.Windows.Input;

namespace ProjectShedule.GlobalSetting.Models
{
    public class SlideSettingModel : DoubleValueElementCell
    {
        public ICommand DragCompletedCommand { get; set; }
        public double ConvertToMemory(double incoming, double maxIncomingValue)
        {
            return PercentConverter.DeConvertValue(incoming, maxIncomingValue, percentValue: MaxValue);
        }
        protected double ConvertToValue(double incoming, double maxIncomingValue)
        {
            return PercentConverter.ConvertToValue(incoming, maxIncomingValue, percentValue: MaxValue);
        }
       
    }
}

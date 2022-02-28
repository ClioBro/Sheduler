using System.Windows.Input;

namespace ProjectShedule.GlobalSetting.Models
{
    public class SlideSettingModel : ElementCell<double>
    {
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        
        public ICommand DragCompletedCommand { get; set; }
        public double ConvertToMemory(double incoming, double maxIncomingValue)
        {
            return PercentConverter.DeConvertValue(incoming, maxIncomingValue, percentValue: MaxValue);
        }
        public double ConvertToValue(double incoming, double maxIncomingValue)
        {
            return PercentConverter.ConvertToValue(incoming, maxIncomingValue, percentValue: MaxValue);
        }
       
    }
}

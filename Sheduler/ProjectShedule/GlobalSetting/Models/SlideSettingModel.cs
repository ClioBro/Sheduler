using System.Windows.Input;

namespace ProjectShedule.GlobalSetting.Models
{
    public class SlideSettingModel : DoubleValueElementCell
    {
        public ICommand DragCompletedCommand { get; set; }
    }
}

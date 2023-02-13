namespace ProjectShedule.GlobalSetting.Models
{
    public abstract class BaseSlideSettingModel : BaseElementCellModel<double>
    {
        public double MaxValue { get; set; } = 100d;
        public double MinValue { get; set; } = 0d;
        public abstract void SaveSettings();
    }
}

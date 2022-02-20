namespace ProjectShedule.GlobalSetting.Models
{
    public class DoubleValueElementCell : ElementCell<double>
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}

namespace ProjectShedule.GlobalSetting
{
    public static class PercentConverter
    {
        public static double Convert(double incoming, double maxValue)
        {
            double res1 = incoming / 100;
            double result = maxValue * res1;
            return result;
        }

        public static double DeConvert(double incoming, double maxValue)
        {
            double res1 = incoming * 100;
            double result = res1 / maxValue;
            return result;
        }
    }
}

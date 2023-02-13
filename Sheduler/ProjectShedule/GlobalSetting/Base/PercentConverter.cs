namespace ProjectShedule.GlobalSetting
{
    public static class PercentConverter
    {
        public static double DeConvertValue(double incoming, double maxIncomingValue, double percentValue)
        {
            double res1 = incoming / percentValue;
            double result = maxIncomingValue * res1;
            return result;
        }

        public static double ConvertToValue(double incoming, double maxIncomingValue, double percentValue)
        {
            double res1 = incoming * percentValue;
            double result = res1 / maxIncomingValue;
            return result;
        }
    }
}

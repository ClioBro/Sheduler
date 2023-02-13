using ProjectShedule.GlobalSetting.Settings;

namespace ProjectShedule.GlobalSetting.Settings.YearsRange
{
    public class YearsRangeSetting : Setting<YearsRangeSetting>
    {
        private const int _minStart = 2010;
        private const int _maxStart = 2020;
        private const int _minEnd = 2024;
        private const int _maxEnd = 2034;

        private const int _defaultStart = _maxStart;
        private const int _defaultEnd = _minEnd;

        public int Start
        {
            get => GetPreference(nameof(Start), _defaultStart);
            set
            {
                if (value > _maxStart)
                    value = _maxStart;
                else if (value < _minStart)
                    value = _minStart;

                SavePreference(nameof(Start), value);
            }
        }
        public int End
        {
            get => GetPreference(nameof(End), _defaultEnd);
            set
            {
                if (value > _maxEnd)
                    value = _maxEnd;
                else if (value < _minEnd)
                    value = _minEnd;
                SavePreference(nameof(End), value);
            }
        }

        public int MaxStart => _maxStart;
        public int MaxEnd => _maxEnd;
        public int MinStart => _minStart;
        public int MinEnd => _minEnd;
    }
}

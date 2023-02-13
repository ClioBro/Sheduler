using System;
using ProjectShedule.Core.Interfaces;

namespace ProjectShedule.Core
{
    public abstract class FloatingPercentAchievement : IPercentAchievement<float>
    {
        public event EventHandler<float> AchivementChanged;
        private readonly float _maxValue;
        private readonly float _minValue;
        private float _achivement;
        private float _currentValue;

        public FloatingPercentAchievement(float minValue, float maxValue, float achivement, float startedValue)
        {
            if (maxValue <= minValue)
                throw new ArgumentOutOfRangeException(nameof(maxValue));
            if (achivement > maxValue || achivement < minValue)
                throw new ArgumentOutOfRangeException(nameof(achivement));
            if (startedValue > maxValue || startedValue < minValue)
                throw new ArgumentOutOfRangeException(nameof(startedValue));

            _maxValue = maxValue;
            _minValue = minValue;
            _achivement = achivement;
            _currentValue = startedValue;
        }

        public float MaxValue => _maxValue;
        public float MinValue => _minValue;

        /// <summary>
        /// Some percent achivement '%'
        /// MaxValue: <see cref="MaxValue"/>  ,        
        /// MinValue: <see cref="MinValue"/>
        /// </summary>
        public float Achivement
        {
            get => _achivement;
            set
            {
                Correct(ref value);
                _achivement = value;
                AchivementChanged?.Invoke(this, value);
            }
        }
        public float CurrentValue 
        {
            get => _currentValue; 
            set => _currentValue = value; 
        }

        private void Correct(ref float value)
        {
            if (value > _maxValue)
                value = _maxValue;
            else if (value < _minValue)
                value = _minValue;
        }
    }
}

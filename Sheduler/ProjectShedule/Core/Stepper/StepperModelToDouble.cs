using System;

namespace ProjectShedule.Core.Stepper
{
    public class StepperModelToDouble : IStepperModel<double>
    {
        private double _value;
        private double _min;
        private double _max;
        private double _step;

        public StepperModelToDouble(double min, double max, double value, double increment)
        {
            _min = min;
            _max = max;
            _value = value;

            if (_max < _min)
                throw new ArgumentOutOfRangeException(nameof(MaxValue));
            if (_value > _max || _value < _min)
                throw new ArgumentOutOfRangeException(nameof(Value));

            _step = increment;
        }
        public StepperModelToDouble()
        {
            _min = 0;
            _max = 100;
            _value = 0;
            _step = 1;
        }
        
        public double MinValue
        {
            get => _min;
            private set => _min = value;
        }
        public double MaxValue
        {
            get => _max;
            private set => _max = value;
        }
        public double Value
        {
            get => _value;
            private set
            {
                if (value > _max || value < _min)
                    throw new ArgumentOutOfRangeException(nameof(Value));
                _value = value;
            }
        }
        public double Increment
        {
            get => _step;
            private set => _step = value;
        }

        public bool CanIncrementValue() => (_value + _step) <= _max;
        public bool CanDecrementValue() => (_value - _step) >= _min;
        public void IncrementValue() => Value += _step;
        public void DecrementValue() => Value -= _step;
        public void SoftIncrementValue()
        {
            if (CanIncrementValue())
                _value += _step;
            else
                _value = _max;
        }
        public void SoftDecrementValue()
        {
            if (CanDecrementValue())
                _value -= _step;
            else
                _value = _min;
        }
    }
}
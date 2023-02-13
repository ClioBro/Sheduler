using System;
using ProjectShedule.Core.Enum;
using ProjectShedule.Core.Interfaces;
using ProjectShedule.Core.Swipe.Interfaces;

namespace ProjectShedule.Core.Swipe
{
    public class FloatingSwipePercentAchievement : FloatingPercentAchievement, IPercentController<double>, IPercentAchievementNotify<float>, ISwipePercentAchievement<float>
    {
        private readonly double _threshold;
        private float _achivementRelativelyThreshold;
        private StatusPosition _position;
        private double _offSet;


        public event EventHandler<SwipePercentAchivementEventArgs<float>> PercentageValueReached;

        public FloatingSwipePercentAchievement(double threashold = 100, float achivementPercent = 0.5f)
            : base(minValue: 0f, maxValue: 1f, achivementPercent, startedValue: 0f)
        {
            _threshold = threashold;
            UpdateAchivementRelativelyThreshold();
            _position = GreaterThanOrEqual() ? StatusPosition.Passed : StatusPosition.Passes;
            AchivementChanged += OnAchivementChanged;
        }

        public double Threshold { get => _threshold; }
        public float AchivementRelativelyThreshold => _achivementRelativelyThreshold;
        public double OffSet 
        {
            get => _offSet; 
            private set 
            {
                _offSet = value;
            }
        }
        public StatusPosition CurrentPosition
        {
            get => _position;
            private set
            {
                if (value == _position)
                    return;
                _position = value;
                Notify();
            }
        }

        public void SetValue(double offSet)
        {
            OffSet = offSet;
            CurrentPosition = GreaterThanOrEqual() ? StatusPosition.Passed : StatusPosition.Passes;
        }
        
        protected virtual void OnAchivementChanged(object sender, float e) => UpdateAchivementRelativelyThreshold();

        private void Notify()
        {
            PercentageValueReached?.Invoke(this, new SwipePercentAchivementEventArgs<float>(this));
        }
        private bool GreaterThanOrEqual() => Math.Abs(_offSet) >= AchivementRelativelyThreshold;
        private void UpdateAchivementRelativelyThreshold()
        {
            _achivementRelativelyThreshold = Achivement * (float)_threshold;
        }
    }
}
using Xamarin.Essentials;

namespace ProjectShedule.Core
{
    public class SwitchableVibration
    {
        private double _durationMlseconds;
        private bool _canVibrate;

        public SwitchableVibration(double durationMlseconds, bool canVibrate = true)
        {
            _durationMlseconds = durationMlseconds;
            _canVibrate = canVibrate;
        }

        /// <summary>
        /// Default Mode: <see cref="SwitchMode.Automatically"/>
        /// </summary>
        public SwitchMode Mode { get; set; }
        public bool CanVibrate => _canVibrate;
        public double DurationMlseconds { get => _durationMlseconds; set => _durationMlseconds = value; }

        /// <summary>
        /// Vibrate operation
        /// </summary>
        /// <remarks> Automatically change Value if Mode is <see cref="SwitchMode.Automatically"/> </remarks>
        public void Invoke()
        {
            if (_canVibrate)
                Vibration.Vibrate(_durationMlseconds);

            if (Mode is SwitchMode.Automatically)
                Switch();
        }

        /// <summary>
        /// Swaps the value of value.
        /// </summary>
        public void Switch() => _canVibrate = !_canVibrate;
        public enum SwitchMode
        {
            Automatically, Manually
        }
    }
}


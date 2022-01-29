using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.ViewModels
{
    internal class SheduleEventsSettingViewModel
    {
        private ShapeEventSetting _shapeSetting;
        public SheduleEventsSettingViewModel()
        {
            _shapeSetting = new ShapeEventSetting();

            Opacity = _shapeSetting.GetOpacity();
            MaxOpacity = 1.0;
            MinOpacity = 0;

            CornerRadius = _shapeSetting.GetCornerRadius();
            MaxCornerRadius = 10;
            MinCornerRadius = 0;

        }

        public string Title { get; set; } = "EventsSetting:";

        public string OpacityText { get; set; } = "Opacity:";
        public double Opacity { get; set; }
        public double MaxOpacity { get; set; }
        public double MinOpacity { get; set; }

        public string CornerRadiusText { get; set; } = "CornerRadius:";
        public float CornerRadius { get; set; }
        public float MaxCornerRadius { get; set; }
        public float MinCornerRadius { get; set; }
    }
}

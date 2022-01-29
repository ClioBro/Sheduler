using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.ViewModels
{
    internal class SheduleEventsSettingViewModel
    {
        private ShapeEventSetting _shapeSetting;
        public SheduleEventsSettingViewModel()
        {
            _shapeSetting = new ShapeEventSetting();

            MaxOpacity = 1.0;
            MinOpacity = 0;

            MaxCornerRadius = 10;
            MinCornerRadius = 0;
        }

        public string Title { get; set; } = "Events:";

        public string OpacityText { get; set; } = "Opacity:";
        public double Opacity 
        {
            get => _shapeSetting.GetOpacity(); 
            set => _shapeSetting.SetOpacity(value); 
        }
        public double MaxOpacity { get; set; }
        public double MinOpacity { get; set; }

        public string CornerRadiusText { get; set; } = "CornerRadius:";
        public float CornerRadius 
        { 
            get => _shapeSetting.GetCornerRadius(); 
            set => _shapeSetting.SetCornerRadius(value); 
        }
        public float MaxCornerRadius { get; set; }
        public float MinCornerRadius { get; set; }

        public string SizeText { get; set; } = "Size:";
        public double Size 
        {
            get => _shapeSetting.GetSize().Height;
            set => _shapeSetting.SetSize(value);
        }
        public double MaxSize { get => _shapeSetting.MaxSize; }
        public double MinSize { get => _shapeSetting.MinSize; }
    }
}

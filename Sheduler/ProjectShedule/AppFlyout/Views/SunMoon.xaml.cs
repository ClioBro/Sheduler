using ProjectShedule.GlobalSetting.Settings.AppTheme;
using ProjectShedule.Other;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.AppFlyout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SunMoon : ContentView
    {
        private BaseViewElementAnimate _baseViewElementAnimate ;
        private ThemeController _themeController;
        public bool IsAnimated { get; protected set; }
        public SunMoon()
        {
            InitializeComponent();
            _themeController = App.ThemeController;
        }

        private void SwitchTheme(object sender, EventArgs e)
        {
            if (!IsAnimated && sender is VisualElement visualElement)
            {
                IsAnimated = true;
                OpacityAnimated();
                switch (_themeController.CurrentTheme)
                {
                    case ThemeController.Theme.Light:
                        _themeController.SetThemeOnApp(ThemeController.Theme.Dark);
                        break;
                    case ThemeController.Theme.Dark:
                        _themeController.SetThemeOnApp(ThemeController.Theme.Light);
                        break;
                    default:
                        break;
                }
                RotationAnimated();
            }

            async void OpacityAnimated()
            {
                _baseViewElementAnimate = new OpacityAnimatedViewElement();
                await _baseViewElementAnimate.SinInElementAsync(visualElement);
            }
            async void RotationAnimated()
            {
                _baseViewElementAnimate = new RotationAnimatedViewElement();
                await _baseViewElementAnimate.SinInElementAsync(visualElement, ()=> this.IsAnimated = false);
            }
        }
        
    }
}
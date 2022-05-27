using ProjectShedule.Animation;
using ProjectShedule.GlobalSetting.Settings.AppTheme;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.AppFlyout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SunMoon : ContentView
    {
        private BaseViewElementAnimate _baseViewElementAnimate ;
        private readonly IThemeController _themeController;
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
                    case ThemeKey.Light:
                        _themeController.SetThemeOnApp(ThemeKey.Dark);
                        break;
                    case ThemeKey.Dark:
                        _themeController.SetThemeOnApp(ThemeKey.Light);
                        break;
                    default:
                        break;
                }
                RotationAnimated();
            }

            void OpacityAnimated()
            {
                _baseViewElementAnimate = new OpacityAppearanceAnimatedViewElement();
                _baseViewElementAnimate.SinInElement(visualElement);
            }
            void RotationAnimated()
            {
                _baseViewElementAnimate = new RotationAnimatedViewElement();
                _baseViewElementAnimate.SinInElement(visualElement, ()=> this.IsAnimated = false);
            }
        }
        
    }
}
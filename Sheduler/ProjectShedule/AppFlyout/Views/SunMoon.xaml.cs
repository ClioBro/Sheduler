using ProjectShedule.GlobalSetting;
using ProjectShedule.Shedule.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.AppFlyout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SunMoon : ContentView
    {
        private IAnimate _animations ;
        public bool IsAnimated { get => _animations.IsAnimated; }
        public SunMoon()
        {
            InitializeComponent();
            _animations = new AnimatedViewElement();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (!IsAnimated && sender is VisualElement visualElement)
            {
                OpacityAnimated();
                switch (App.Theme.CurrentTheme)
                {
                    case ThemeController.Theme.Light:
                        App.Theme.SetThemeOnApp(ThemeController.Theme.Dark);
                        break;
                    case ThemeController.Theme.Dark:
                        App.Theme.SetThemeOnApp(ThemeController.Theme.Light);
                        break;
                    default:
                        break;
                }
                
                await visualElement.RotateTo(360, 500);
                visualElement.Rotation = 0;
            }

            void OpacityAnimated()
            {
                visualElement.Opacity = 0;
                visualElement.FadeTo(1, 700);
            }
        }
        
    }
}
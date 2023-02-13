using ProjectShedule.Animation;
using ProjectShedule.Shedule.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShedulePage : ContentPage
    {
        private readonly BaseViewElementAnimate _animations;
        public ShedulePage()
        {
            InitializeComponent();
            BindingContext = new ShedulePageViewModel(Navigation);
            _animations = new BouncingAnimatedViewElement(length: 300, firstScale: -0.03, secondScale: 0.03);
        }
        private void ViewAnimatedPush(object sender, EventArgs e)
        {
            if (!_animations.IsAnimated && sender is VisualElement visualElement)
            {
                _animations.SinInElement(visualElement);
            }
        }
    }
}
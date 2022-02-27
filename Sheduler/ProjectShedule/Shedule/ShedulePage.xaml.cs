using ProjectShedule.Shedule.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShedulePage : ContentPage
    {
        private readonly AnimatedViewElement _animations;
        public ShedulePage()
        {
            InitializeComponent();
            BindingContext = new SheduleViewModel() { Navigation = this.Navigation };
            _animations = new AnimatedViewElement();
        }
        public bool IsAnimated { get => _animations.IsAnimated; }
        private async void ViewAnimatedPush(object sender, EventArgs e)
        {
            if (!IsAnimated && sender is VisualElement visualElement)
            {
                await _animations.SinInElementAsync(visualElement);
            }
        }
    }
}
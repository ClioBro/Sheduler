using Xamarin.Forms;

namespace ProjectShedule.Animation
{
    public class OpacityAppearanceAnimatedViewElement : BaseViewElementAnimate
    {
        private double _opacity = 1;
        public OpacityAppearanceAnimatedViewElement(uint length = 700)
        {
            _length = length;
        }
        protected override async void SinIn()
        {
            IsAnimated = true;
            VisualElement.Opacity = 0;
            await VisualElement.FadeTo(_opacity, _length);
            IsAnimated = false;
            FinishCallBack?.Invoke();
        }
    }
}

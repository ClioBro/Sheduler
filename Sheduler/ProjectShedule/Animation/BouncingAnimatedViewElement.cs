using Xamarin.Forms;

namespace ProjectShedule.Animation
{
    public class BouncingAnimatedViewElement : BaseViewElementAnimate
    {
        public double FirstScale { get; set; }
        public double SecondScale { get; set; }
        public BouncingAnimatedViewElement(uint length = 500, double firstScale = -0.1, double secondScale = 0.1)
        {
            _length = length;
            FirstScale = firstScale;
            SecondScale = secondScale;
        }
        protected override async void SinIn()
        {
            IsAnimated = true;
            await VisualElement.RelScaleTo(FirstScale, _length);
            await VisualElement.RelScaleTo(SecondScale, _length);
            IsAnimated = false;
            FinishCallBack?.Invoke();
        }
    }
}

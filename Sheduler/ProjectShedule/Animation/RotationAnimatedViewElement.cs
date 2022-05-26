using Xamarin.Forms;

namespace ProjectShedule.Animation
{
    public class RotationAnimatedViewElement : BaseViewElementAnimate
    {
        private double _rotation = 360;
        public RotationAnimatedViewElement(double rotation = 360, uint length = 500)
        {
            _rotation = rotation;
            _length = length;
        }
        protected override async void SinIn()
        {
            IsAnimated = true;
            await VisualElement.RotateTo(_rotation, _length);
            VisualElement.Rotation = 0;
            IsAnimated = false;
            FinishCallBack?.Invoke();
        }
    }

}

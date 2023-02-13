using Xamarin.Forms;

namespace ProjectShedule.Animation
{
    public class RotationAnimatedViewElement : BaseViewElementAnimate
    {
        private double _rotation = 360;
        private double _originalRotation;

        public RotationAnimatedViewElement(double rotation = 360, uint length = 500)
        {
            _rotation = rotation;
            Length = length;
        }

        public bool FastReturnOriginalRotation { get; set; }
        protected override async void SinIn()
        {
            IsAnimated = true;
            _originalRotation = VisualElement.Rotation;
            await VisualElement.RotateTo(_rotation, Length);

            if(FastReturnOriginalRotation)
                VisualElement.Rotation = _originalRotation;
            
            IsAnimated = false;
            FinishCallBack?.Invoke();
        }
    }

}

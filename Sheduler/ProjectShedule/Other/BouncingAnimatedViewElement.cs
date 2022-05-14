using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectShedule.Other
{
    public abstract class BaseViewElementAnimate : IAnimate
    {
        protected uint _length;
        protected VisualElement VisualElement { get; private set; }
        public bool IsAnimated { get; protected set; }

        public Task SinInElementAsync(VisualElement visualElement, Action finishCallBack = null)
        {
            return Task.Run(() =>
            {
                VisualElement = visualElement;
                SinIn();
                finishCallBack?.Invoke();
            });
        }
        protected abstract void SinIn();
    }
    public class BouncingAnimatedViewElement : BaseViewElementAnimate
    {
        public BouncingAnimatedViewElement(uint length = 500)
        {
            _length = length;
        }
        protected override async void SinIn()
        {
            IsAnimated = true;
            await VisualElement.RelScaleTo(-0.1, _length);
            await VisualElement.RelScaleTo(0.1, _length);
            IsAnimated = false;
        }
    }
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
        }
    }
    public class OpacityAnimatedViewElement : BaseViewElementAnimate
    {
        private double _opacity = 1;
        public OpacityAnimatedViewElement(uint length = 700)
        {
            _length = length;
        }
        protected override async void SinIn()
        {
            IsAnimated = true;
            VisualElement.Opacity = 0;
            await VisualElement.FadeTo(_opacity, _length);
            IsAnimated = false;
        }
    }

}

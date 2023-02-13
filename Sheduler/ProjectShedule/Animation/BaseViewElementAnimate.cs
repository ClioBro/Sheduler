using ProjectShedule.Other;
using System;
using Xamarin.Forms;

namespace ProjectShedule.Animation
{
    public abstract class BaseViewElementAnimate : IAnimate
    {
        private uint _length;
        protected VisualElement VisualElement { get; private set; }
        protected Action FinishCallBack { get; private set; }
        public bool IsAnimated { get; protected set; }

        public void SinInElement(VisualElement visualElement, Action finishCallBack = null)
        {
            if (visualElement is null)
                throw new Exception($"{this} visualElementIsNull");
            FinishCallBack = finishCallBack;
            VisualElement = visualElement;
            SinIn();
        }
        public virtual uint Length { get => _length; set => _length = value; }
        protected abstract void SinIn();
    }

}

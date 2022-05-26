using ProjectShedule.Other;
using System;
using Xamarin.Forms;

namespace ProjectShedule.Animation
{
    public abstract class BaseViewElementAnimate : IAnimate
    {
        protected uint _length;
        protected VisualElement VisualElement { get; private set; }
        protected Action FinishCallBack { get; private set; }
        public bool IsAnimated { get; protected set; }

        public void SinInElement(VisualElement visualElement, Action finishCallBack = null)
        {
            FinishCallBack = finishCallBack;
            VisualElement = visualElement;
            SinIn();
        }
        protected abstract void SinIn();
    }

}

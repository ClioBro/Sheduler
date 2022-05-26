using System;
using Xamarin.Forms;

namespace ProjectShedule.Other
{
    public interface IAnimate
    {
        public void SinInElement(VisualElement visualElement, Action FinishCallBack = null);
    }
}

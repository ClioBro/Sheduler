using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectShedule.Other
{
    public interface IAnimate
    {
        public  Task SinInElementAsync(VisualElement visualElement, Action FinishCallBack = null);
    }
}

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectShedule.Other
{
    public interface IAnimate
    {
        bool IsAnimated { get; }
        public Task SinInElementAsync(VisualElement animatebleObj, Action finish);
    }
}

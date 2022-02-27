using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IAnimate
    {
        bool IsAnimated { get; }
        public Task SinInElementAsync(VisualElement animatebleObj, Action finish);
    }
}

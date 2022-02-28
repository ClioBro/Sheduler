using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectShedule.Other
{
    public class AnimatedViewElement : IAnimate
    {
        private VisualElement _visualElement;
        public bool IsAnimated { get; private set; }
        public Task SinInElementAsync(VisualElement visualElement, Action finish = null)
        {
            return Task.Run(()=> 
            {
                _visualElement = visualElement;
                SinIn();
                finish?.Invoke();
            });
        }

        private async void SinIn()
        {
            if (_visualElement != null)
            {
                IsAnimated = true;
                 await _visualElement.RelScaleTo(-0.1, 200);
                 await _visualElement.RelScaleTo(0.1, 200);
                IsAnimated = false;
            }
        }
    }
}

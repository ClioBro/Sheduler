using ProjectShedule.Core.Interfaces;
using Xamarin.Forms;

namespace ProjectShedule.Core.Swipe
{
    public class BackGroundColorSwich : IColorSwich
    {
        private readonly VisualElement _view;
        
        public BackGroundColorSwich(VisualElement view)
        {
            _view = view;
        }
        
        public Color On { get; set; } = Color.White;
        public Color In { get; set; } = Color.Black;
        
        public void SwichColor()
        {
            _view.BackgroundColor = _view.BackgroundColor == On ? In : On;
        }
    }
}

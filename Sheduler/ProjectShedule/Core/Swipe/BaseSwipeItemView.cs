using Xamarin.Forms;

namespace ProjectShedule.Core.Swipe
{
    public abstract class BaseSwipeItemView : SwipeItemView
    {
        private readonly Image _image = new Image();
        private readonly Label _label = new Label();
        private readonly StackLayout _stackLayout = new StackLayout();

        public BaseSwipeItemView(Image image, Label label) : this()
        {
            _image = image;
            _label = label;
        }
        public BaseSwipeItemView()
        {
            _stackLayout.Children.Add(_image);
            _stackLayout.Children.Add(_label);
            Content = _stackLayout;

            CompressedLayout.SetIsHeadless(_stackLayout, true);
            CompressedLayout.SetIsHeadless(this, true);
        }
       
        protected Image Image => _image;
        protected Label Label => _label;
        protected StackLayout StackLayout => _stackLayout;
    }
}

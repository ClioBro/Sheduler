using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectShedule.Core
{
    public class SortImageButton : ImageButton
    {
        private Animation.RotationAnimatedViewElement _animationRotate;

        public static readonly BindableProperty DescendingProperty =
            BindableProperty.Create(nameof(Descending), typeof(bool), typeof(SortImageButton), true, BindingMode.TwoWay, propertyChanged: OnDescendingPropertyChanged);

        public SortImageButton()
        {
            _animationRotate = GetAnimation();
            Pressed += OnSortImageButton_Pressed;
        }

        public bool Descending
        {
            get => (bool)GetValue(DescendingProperty);
            set => SetValue(DescendingProperty, value);
        }

        public void ScrollImage()
        {
            _animationRotate = GetAnimation();
            _animationRotate.SinInElement(this);
        }
        public Task ScrollImageAsync()
        {
            return Task.Run(() => 
            {
                _animationRotate = GetAnimation();
                _animationRotate.SinInElement(this);
            });
        }
        private static async void OnDescendingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SortImageButton sortImage)
            {
                //sortImage.ScrollImage();
                await sortImage.ScrollImageAsync();
            }
        }
        private void OnSortImageButton_Pressed(object sender, System.EventArgs e)
        {
            Descending = !Descending;
        }
        private Animation.RotationAnimatedViewElement GetAnimation()
        {
            return Descending
                ? new Animation.RotationAnimatedViewElement(0, 100)
                : new Animation.RotationAnimatedViewElement(180, 100); 
        }
    }
}

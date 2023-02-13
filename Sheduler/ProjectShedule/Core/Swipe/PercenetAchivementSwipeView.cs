using ProjectShedule.Core.Interfaces;
using Xamarin.Forms;

namespace ProjectShedule.Core.Swipe
{
    public class PercenetAchivementSwipeView : SwipeView, ISwipeViewController
    {
        #region BindableFields
        public static readonly BindableProperty RightPercenetAchivementProperty =
            BindableProperty.Create(nameof(RightPercenetAchivement), typeof(IPercentController<double>), typeof(PercenetAchivementSwipeView), null, BindingMode.OneWay);

        public static readonly BindableProperty LeftPercenetAchivementProperty =
            BindableProperty.Create(nameof(LeftPercenetAchivement), typeof(IPercentController<double>), typeof(PercenetAchivementSwipeView), null, BindingMode.OneWay);

        public static readonly BindableProperty SwipeViewControllerProperty =
            BindableProperty.Create(nameof(SwipeViewController), typeof(ISwipeViewController), typeof(PercenetAchivementSwipeView), null, BindingMode.OneWayToSource);
        #endregion

        public PercenetAchivementSwipeView()
        {
            SwipeViewController = this;
            SwipeChanging += OnSwipeView_SwipeChanging;
        }

        #region BindableProperties
        public IPercentController<double> RightPercenetAchivement
        {
            get => (IPercentController<double>)GetValue(RightPercenetAchivementProperty);
            set => SetValue(RightPercenetAchivementProperty, value);
        }
        public IPercentController<double> LeftPercenetAchivement
        {
            get => (IPercentController<double>)GetValue(LeftPercenetAchivementProperty);
            set => SetValue(LeftPercenetAchivementProperty, value);
        }
        public ISwipeViewController SwipeViewController
        {
            get => (ISwipeViewController)GetValue(SwipeViewControllerProperty);
            set => SetValue(SwipeViewControllerProperty, value);
        }
        #endregion

        private void OnSwipeView_SwipeChanging(object sender, SwipeChangingEventArgs e)
        {
            double offset = e.Offset;
            SwipeDirection swipeDirection = e.SwipeDirection;
            switch (swipeDirection)
            {
                case SwipeDirection.Right:
                    TryExecuteActionOffSet(RightPercenetAchivement, offset);
                    break;
                case SwipeDirection.Left:
                    TryExecuteActionOffSet(LeftPercenetAchivement, offset);
                    break;
            }
        }
        private void TryExecuteActionOffSet(IPercentController<double> percenetAchivement, double offSet)
        {
            if (percenetAchivement is null)
                return;

            percenetAchivement.SetValue(offSet);
        }
    }
}
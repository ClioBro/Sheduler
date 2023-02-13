using ProjectShedule.Core.Enum;
using ProjectShedule.Core.Swipe;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.Swipe;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels
{
    public class ThrashSmallTaskViewModel : DeletableSmallTaskViewModel
    {
        private ReveiveSmallTaskSwipeItemView _reviveSwipeItem;
        public ThrashSmallTaskViewModel(SmallTaskModel smallTaskModel) : base(smallTaskModel)
        {
            VisualizeHowRemoved(IsDeleted);
        }
        public ISwipeItem ReviveSwipeItem => _reviveSwipeItem;

        public void SetByDeleted()
        {
            VisualizeHowRemoved(false);
            DeletedDateTime = null;
        }
        private void VisualizeHowRemoved(bool value)
        {
            ViewIsEnable = value;
            Opacity = value ? 1 : 0.3;
        }
        public override object Clone()
        {
            return new ThrashSmallTaskViewModel(SmallTaskModel.Clone() as SmallTaskModel);
        }
        protected override void InicializationSwipe()
        {
            base.InicializationSwipe();
            _reviveSwipeItem = new ReveiveSmallTaskSwipeItemView(this);
            SwipeView.LeftItems.Mode = SwipeMode.Execute;
            SwipeView.LeftItems.Add(_reviveSwipeItem);

            SwipeView.RightDisclosurePercenetAchivement.Achivement = 0.5f;
            SwipeView.RightDisclosurePercenetAchivement.PercentageValueReached += RightSwipePercentageValueReached;
        }

        private void RightSwipePercentageValueReached(object sender, SwipePercentAchivementEventArgs<float> e)
        {
            _reviveSwipeItem.SwichBackGroundColor();
            if (e.SwipePercentAchievement.CurrentPosition is StatusPosition.Passed)
                Vibration.Vibrate(70);
        }
    }
}

using ProjectShedule.Core.Enum;
using ProjectShedule.Core.Swipe;
using ProjectShedule.Core.Swipe.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.Swipe;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels
{
    public class DeletableSmallTaskViewModel : SimpleSmallTaskViewModel
    {
        private DeleteSmallTaskSwipeItemView _deleteSwipeItem;
        public DeletableSmallTaskViewModel(SmallTaskModel smallTaskModel) : base(smallTaskModel) { }

        public DateTime? DeletedDateTime 
        {
            get => SmallTaskModel?.DeletedDateTime;
            set
            {
                if (value == SmallTaskModel?.DeletedDateTime)
                    return;
                SmallTaskModel.DeletedDateTime = value;
                OnPropertyChanged();
            } 
        }
        public ISwipeItem DeleteSwipeItem => _deleteSwipeItem;
        protected override void InicializationSwipe()
        {
            base.InicializationSwipe();
            _deleteSwipeItem = new DeleteSmallTaskSwipeItemView(commandParameter: this);
            SwipeView.RightItems.Mode = SwipeMode.Execute;
            SwipeView.RightItems.Add(_deleteSwipeItem);

            SwipeView.LeftDisclosurePercenetAchivement.Achivement = 0.5f;
            SwipeView.LeftDisclosurePercenetAchivement.PercentageValueReached += LeftSwipePercentageValueReached;
        }
        public override object Clone()
        {
            return new DeletableSmallTaskViewModel(SmallTaskModel.Clone() as SmallTaskModel);
        }

        private void LeftSwipePercentageValueReached(object sender, SwipePercentAchivementEventArgs<float> e)
        {
            _deleteSwipeItem.SwichBackGroundColor();
            if (e.SwipePercentAchievement.CurrentPosition is StatusPosition.Passed)
                Vibration.Vibrate(70);
        }
    }
}


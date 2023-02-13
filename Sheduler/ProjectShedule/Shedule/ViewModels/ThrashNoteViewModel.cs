using ProjectShedule.Core.Swipe.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Builder;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Swipe;
using ProjectShedule.Shedule.ViewModels.Base;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels
{
    public class ThrashNoteViewModel : NoteDeletableSwipebleExpanderCanDeleteSmallTaskViewModel<ThrashSmallTaskViewModel>
    {
        private ICommand _reviveSmallTaskCommand;
        private ReveiveNoteSwipeItemView _reviveSwipeItem;

        public event SmallTaskEventHandler<ThrashNoteViewModel, ThrashSmallTaskViewModel> SmallTaskRevivePressed;

        public ThrashNoteViewModel(Note note) : base(note)
        {
            VisualizeHowRemoved(IsDeleted);
        }

        public override ISmallTaskViewModelBuilder<ThrashSmallTaskViewModel> SmallTaskViewModelBuilder { get; protected set; } = new ThrashSmallTaskViewModelBuilder();

        public ISwipeItem ReviveSwipeItem => _reviveSwipeItem;
        public bool CanDeleteMe => IsDeleted == false && DeletedSmallTasksCount == 0;

        public override object Clone()
        {
            return new ThrashNoteViewModel(Note.Clone() as Note);
        }

        protected override void InicializationCommands()
        {
            base.InicializationCommands();
            _reviveSmallTaskCommand = new Command<ThrashSmallTaskViewModel>(ReviveSmallTaskCommandHandler);
        }
        protected override void InicializationSwipe()
        {
            base.InicializationSwipe();
            //_reviveMeSwipeItem = new ReviveSwipeItem(commandParameter: this);
            _reviveSwipeItem = new ReveiveNoteSwipeItemView(this);
            LeftItems.Add(_reviveSwipeItem);
        }
        protected override ThrashSmallTaskViewModel BuildViewModel(SmallTask smallTask)
        {
            ThrashSmallTaskViewModel thrashNoteViewModel = base.BuildViewModel(smallTask);
            thrashNoteViewModel.ReviveSwipeItem.Command = _reviveSmallTaskCommand;
            return thrashNoteViewModel;
        }
        protected virtual void ReviveSmallTaskCommandHandler(ThrashSmallTaskViewModel thrashSmallTaskViewModel)
        {
            thrashSmallTaskViewModel.SetByDeleted();
            SmallTaskRevivePressed?.Invoke(this, thrashSmallTaskViewModel);
        }

        private void VisualizeHowRemoved(bool value)
        {
            Opacity = value ? 1 : 0.3;
            IsExpanded = !value;
            SmallTasksContainerIsEnable = !value;
            ISwipeItemsControll swipeItemsControll = this;
            if (value == false)
                swipeItemsControll.DisableSwipeItems();
            else
                swipeItemsControll.EnableSwipeItems();
        }
    }
}

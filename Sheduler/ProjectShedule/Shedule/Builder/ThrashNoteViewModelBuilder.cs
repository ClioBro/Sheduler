using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Builder.Base;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.ViewModels;
using System.Windows.Input;


namespace ProjectShedule.Shedule.Builder
{
    public class ThrashNoteViewModelBuilder : BaseDeletableNoteViewModelBuilder<ThrashNoteViewModel>, IReviveViewModelBuilder<ThrashNoteViewModelBuilder, ThrashNoteViewModel>, IGetClearParamsBuilder<ThrashNoteViewModelBuilder>
    {
        private ICommand _reviveCommand;
        public ThrashNoteViewModelBuilder GetClearParamsBuilder() => new ThrashNoteViewModelBuilder();

        public ThrashNoteViewModelBuilder SetReviveCommand(ICommand reviveCommand)
        {
            _reviveCommand = reviveCommand;
            return this;
        }

        protected override ThrashNoteViewModel BuildViewModel()
        {
            ThrashNoteViewModel thrashNoteViewModel = new ThrashNoteViewModel(Note ?? new Note());
            thrashNoteViewModel.DeleteSwipeItem.Command = DeleteCommand;
            thrashNoteViewModel.ReviveSwipeItem.Command = _reviveCommand;
            return thrashNoteViewModel;
        }
    }
}

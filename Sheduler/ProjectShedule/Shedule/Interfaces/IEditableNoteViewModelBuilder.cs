namespace ProjectShedule.Shedule.Interfaces
{
    #region SmallTasksBuilders

    #endregion SmallTasksBuilders
    #region NotesBuilders
    public interface IEditableNoteViewModelBuilder<T> : INoteViewModelBuilder<T>, IEditableViewModelBuilder<T>
    {

    }
    //public class ThrashNoteViewModelTBuilder : BaseDeletableNoteViewModelBuilder<ThrashNoteViewModelT>, IThrashNoteViewModelBuilder<ThrashNoteViewModelT>
    //{
    //    private ICommand _reviveCommand;
    //    public IThrashNoteViewModelBuilder<ThrashNoteViewModelT> GetClearParamsBuilder() => new ThrashNoteViewModelTBuilder();

    //    public IReviveViewModelBuilder<ThrashNoteViewModelT> SetReviveCommand(ICommand reviveCommand)
    //    {
    //        _reviveCommand = reviveCommand;
    //        return this;
    //    }

    //    protected override ThrashNoteViewModelT BuildViewModel()
    //    {
    //        ThrashNoteViewModelT thrashNoteViewModel = new ThrashNoteViewModelT(Note ?? new Note());
    //        thrashNoteViewModel.DeleteSwipeItem.Command = DeleteCommand;
    //        thrashNoteViewModel.ReviveSwipeItem.Command = _reviveCommand;
    //        return thrashNoteViewModel;
    //    }
    //}
    #endregion NotesBuilders
}

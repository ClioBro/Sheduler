namespace ProjectShedule.Shedule.Interfaces
{
    public interface IThrashNoteViewModelBuilder<TType> : IDeletableViewModelBuilder<IThrashNoteViewModelBuilder<TType>, TType>, IReviveViewModelBuilder<IThrashNoteViewModelBuilder<TType>, TType>, IGetClearParamsBuilder<IThrashNoteViewModelBuilder<TType>>
    {
    }
}

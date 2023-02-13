namespace ProjectShedule.Core.RadioButton
{
    public interface IConvertRadioButtonsViewModel<T> : IRadioButtonsViewModel
    {
        public T[] ConvertedItems { get; }
        public T ConvertedSelectedItem { get; }
    }
}
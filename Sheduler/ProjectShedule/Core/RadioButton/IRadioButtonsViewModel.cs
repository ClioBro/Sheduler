namespace ProjectShedule.Core.RadioButton
{
    public interface IRadioButtonsViewModel
    {
        public RadioButtonItemModel[] Items { get; }
        public RadioButtonItemModel SelectedItem { get; }
        public string GroupName { get; }
    }
}
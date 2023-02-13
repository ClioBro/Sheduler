using ProjectShedule.Core.RadioButton;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Base;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.PutInOrder;

namespace ProjectShedule.Shedule.ViewModels
{
    public class SortNoteRadioButtonsViewModel : RadioButtonsViewModel, IConvertRadioButtonsViewModel<SortInOrderNote>
    {
        private readonly SortInOrderNote[] _sortNoteBies;
        
        public SortNoteRadioButtonsViewModel()
        {
            _sortNoteBies = new SortInOrderNote[]
            {
                new SortNoteByAppointmentDate(),
                new SortNoteByAlphabetically(),
                new SortNoteByCreatedDate(),
            };
            Items = _sortNoteBies;
            GroupName = "SimpleGroup";
            SelectedItem = Items[0];
            Wrap = Xamarin.Forms.FlexWrap.Wrap;
            Direction = Xamarin.Forms.FlexDirection.Row;
        }

        public SortInOrderNote[] ConvertedItems => _sortNoteBies;
        public SortInOrderNote ConvertedSelectedItem => (SortInOrderNote)SelectedItem;
    }
}
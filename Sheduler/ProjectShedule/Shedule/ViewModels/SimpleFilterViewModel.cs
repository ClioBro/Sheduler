using ProjectShedule.Core.RadioButton;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Base;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Interfaces;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.ViewModels
{
    internal class SimpleNoteFilterViewModel : ExpandebleRadioButtonsViewModel, INoteSortInOrder
    {
        private readonly SortNoteRadioButtonsViewModel _simpleRadioButtonsViewModel;
        private bool _descending;

        public SimpleNoteFilterViewModel()
        {
            _simpleRadioButtonsViewModel = new SortNoteRadioButtonsViewModel();
            CurrentSortInOrderNote.Descending = _descending;
            RadioButtonsViewModels.Add(_simpleRadioButtonsViewModel);
            _simpleRadioButtonsViewModel.PropertyChanged += OnSimpleRadioButtonsViewModel_PropertyChanged;
        }

        public SortInOrderNote CurrentSortInOrderNote => _simpleRadioButtonsViewModel.ConvertedSelectedItem;
        public override string Text => _simpleRadioButtonsViewModel.ConvertedSelectedItem.Text;
        public override bool Descending
        {
            get => _descending;
            set
            {
                if (_descending == value)
                    return;
                _descending = value;    
                CurrentSortInOrderNote.Descending = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<T> SortNoteInOrder<T>(IEnumerable<T> itemSort) where T : INote
        {
            SortInOrderNote sortNoteBy = _simpleRadioButtonsViewModel.ConvertedSelectedItem;
            IEnumerable<T> result = sortNoteBy.SortNoteInOrder(itemSort);
            return result;
        }
        
        private void OnSimpleRadioButtonsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(RadioButtonsViewModel.SelectedItem):
                    CurrentSortInOrderNote.Descending = Descending;
                    OnPropertyChanged(nameof(CurrentSortInOrderNote));
                    OnPropertyChanged(nameof(Text));
                    break;
            }
        }
    }
}
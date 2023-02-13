using ProjectShedule.Core.RadioButton;
using ProjectShedule.Core.Sorting;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Base;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Interfaces;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.ViewModel
{
    public class SheduleNoteFilterViewModel : ExpandebleRadioButtonsViewModel, IFilter<Note>, INoteSortInOrder
    {
        private readonly ShedulerDataRadioButtonsViewModel _shedulerDataRadioButtonsViewModel;
        private readonly SortNoteRadioButtonsViewModel _simpleRadioButtonsViewModel;
        private bool _descending;

        public SheduleNoteFilterViewModel(IGetItemsDateTime<Note> getItems, DateTime startedDateTime, IEnumerable<DateTime> selectedDateTimes)
        {
            _shedulerDataRadioButtonsViewModel = new ShedulerDataRadioButtonsViewModel(getItems, startedDateTime, selectedDateTimes);
            _simpleRadioButtonsViewModel = new SortNoteRadioButtonsViewModel();
            _simpleRadioButtonsViewModel.ConvertedSelectedItem.Descending = _descending;

            RadioButtonsViewModels = new ObservableRangeCollection<RadioButtonsViewModel>()
            {
                _shedulerDataRadioButtonsViewModel,
                _simpleRadioButtonsViewModel
            };
            _shedulerDataRadioButtonsViewModel.PropertyChanged += RadioButtonsViewModel_PropertyChanged;
            _simpleRadioButtonsViewModel.PropertyChanged += RadioButtonsViewModel_PropertyChanged;
        }

        public ShedulerDataRadioButtonsViewModel ShedulerDataRadioButtonsViewModel => _shedulerDataRadioButtonsViewModel;
        public SortNoteRadioButtonsViewModel SimpleRadioButtonsViewModel => _simpleRadioButtonsViewModel;

        public override string Text => GetReasonableHeader();
        public override bool Descending
        {
            get => _descending;
            set
            {
                if (_descending == value)
                    return;
                _descending = value;
                _simpleRadioButtonsViewModel.ConvertedSelectedItem.Descending = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Note> Filter()
        {
            SortBaseNote sortBaseNote = _shedulerDataRadioButtonsViewModel.ConvertedSelectedItem;
            IEnumerable<Note> notes = sortBaseNote.Filter();
            return SortNoteInOrder(notes);
        }
        public IEnumerable<T> SortNoteInOrder<T>(IEnumerable<T> itemSort)
            where T : INote
        {
            SortInOrderNote sortNoteBy = _simpleRadioButtonsViewModel.ConvertedSelectedItem;
            return sortNoteBy.SortNoteInOrder(itemSort);
        }

        private string GetReasonableHeader()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (RadioButtonsViewModel radioButtonsViewModel in RadioButtonsViewModels)
            {
                stringBuilder.Append(radioButtonsViewModel.SelectedItem.Text);
                if(Equals(RadioButtonsViewModels.Last(), radioButtonsViewModel) == false)
                    stringBuilder.Append(". ");

            }
            return stringBuilder.ToString();
        }
        private void RadioButtonsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(RadioButtonsViewModel.SelectedItem):
                    _simpleRadioButtonsViewModel.ConvertedSelectedItem.Descending = Descending;
                    OnPropertyChanged(nameof(Text));
                    break;
            }
        }
    }
}

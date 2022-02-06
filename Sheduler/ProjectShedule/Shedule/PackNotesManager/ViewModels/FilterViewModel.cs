using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.PackNotesManager;
using ProjectShedule.Shedule.PackNotesManager.FilterManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.Shedule
{
    public class FilterViewModel : BindableBase<FilterViewModel>
    {
        private readonly FilterPackNote _filterPackNotes;
        public FilterViewModel()
        {
            _filterPackNotes = new FilterPackNote();

            FilterTypes = new SortInDate[]
            {
                new SelectedSortInDate { Text = Resources.Filters.SelectedDateLabel},
                new ToDaySortInDate { Text = Resources.Filters.ToDayLabel},
                new AllSortInDate { Text = Resources.Filters.AllLabel},
            };

            OrderTypes = new PutInOrder[]
            {
                new PutInOrderByDate{ Text = Resources.Filters.ByDateLabel},
                new PutInOrderByAlphabet{ Text = Resources.Filters.AlphabeticallyLabel},
            };
            PropertyChanged += OnFilterControlPropertyChanged;

            SelectedFlter = FilterTypes[0];
            SelectedOrder = OrderTypes[0];
        }
        public SortInDate SelectedFlter
        {
            get => GetProperty<SortInDate>();
            set => SetProperty(value);
        }
        public PutInOrder SelectedOrder
        {
            get => GetProperty<PutInOrder>();
            set => SetProperty(value);
        }

        public SortInDate[] FilterTypes
        {
            get => GetProperty<SortInDate[]>();
            set => SetProperty(value);
        }
        public PutInOrder[] OrderTypes
        {
            get => GetProperty<PutInOrder[]>();
            set => SetProperty(value);
        }

        public void SetBySelectedDate(DateTime dateTime)
        {
            if ((SelectedFlter is SelectedSortInDate) == false)
                SelectedFlter = FilterTypes.FirstOrDefault(t => t.This is SelectedSortInDate);

            var selectedSortInDate = SelectedFlter as SelectedSortInDate;
            selectedSortInDate.Date = dateTime;
            Notify(nameof(SelectedFlter));
        } 
        public IEnumerable<PackNoteModel> GetFiltered() => _filterPackNotes.GetFiltered();

        private void OnFilterControlPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedFlter):
                    _filterPackNotes.SortInDate = (SortInDate)SelectedFlter.This;
                    break;
                case nameof(SelectedOrder):
                    _filterPackNotes.PutInOrder = (PutInOrder)SelectedOrder.This;
                    break;
                default:
                    break;
            }
        }
        private enum Types
        {
            Selected, Today, Tomorrow, All
        }
    }
}

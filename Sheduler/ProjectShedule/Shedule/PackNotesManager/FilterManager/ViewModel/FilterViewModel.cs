using ProjectShedule.Language.Resources.OtherElements;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.ViewModel
{
    public class FilterViewModel : BindableBase<FilterViewModel>
    {
        private readonly FilterPackNote _filterPackNotes;
        public FilterViewModel()
        {
            _filterPackNotes = new FilterPackNote();

            FilterTypes = new SortInDate[]
            {
                new SelectedSortInDate { Text = Filters.BySelectedDate},
                new ToDaySortInDate { Text = Filters.ByToday},
                new AllSortInDate { Text = Filters.AllItems},
            };

            OrderTypes = new PutInOrder[]
            {
                new PutInOrderByDate{ Text = Filters.ByDate},
                new PutInOrderByAlphabet{ Text = Filters.ByAlphabetically},
            };
            PropertyChanged += OnFilterControlPropertyChanged;

            SelectedFlter = FilterTypes[0];
            SelectedOrder = OrderTypes[0];

            SelectedFlter.IsChecked = true;
            SelectedOrder.IsChecked = true;
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
        public IEnumerable<IPackNote> GetFiltered() => _filterPackNotes.GetFiltered();

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

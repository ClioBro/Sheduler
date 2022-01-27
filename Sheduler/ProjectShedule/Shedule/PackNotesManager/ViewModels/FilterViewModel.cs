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
                new SelectedSortInDate { Text = "Выбранной дате"},
                new ToDaySortInDate { Text = "Сегодня"},
                new AllSortInDate { Text = "Все"},
            };

            OrderTypes = new PutInOrder[]
            {
                new PutInOrderByDate{ Text = "По Дате"},
                new PutInOrderByAlphabet{ Text = "По Алфавиту"},
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
                SelectedFlter = FilterTypes.FirstOrDefault(t => t.ThisType is SelectedSortInDate);

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
                    _filterPackNotes.SortInDate = SelectedFlter.ThisType;
                    break;
                case nameof(SelectedOrder):
                    _filterPackNotes.PutInOrder = SelectedOrder.ThisType;
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

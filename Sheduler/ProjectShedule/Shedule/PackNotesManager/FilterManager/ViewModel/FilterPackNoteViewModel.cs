using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.ViewModel
{
    public class FilterPackNoteViewModel : INotifyPropertyChanged
    {
        private readonly FilterPackNoteModel _filterPackNote;
        public event PropertyChangedEventHandler PropertyChanged;
        public FilterPackNoteViewModel(SortInDate[] sortInDates, PutInOrderNote[] putInOrderNotes)
        {
            FilterTypes = sortInDates;
            OrderTypes = putInOrderNotes;

            _filterPackNote = new FilterPackNoteModel(OrderTypes?[0], FilterTypes?[0]);

            SelectedFlter.IsChecked = true;
            SelectedOrder.IsChecked = true;
        }
        public SortInDate SelectedFlter
        {
            get => _filterPackNote.SortInDate;
            set
            {
                if (_filterPackNote.SortInDate != value)
                {
                    _filterPackNote.SortInDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public PutInOrderNote SelectedOrder
        {
            get => _filterPackNote.PutInOrder;
            set
            {
                if (_filterPackNote.PutInOrder != value)
                {
                    _filterPackNote.PutInOrder = value;
                    OnPropertyChanged();
                }
            }
        }
        public SortInDate[] FilterTypes { get; set; }
        public PutInOrderNote[] OrderTypes { get; set; }
        public bool IsCarouselSelected => SelectedFlter is CarouselSelectedDay;
        public bool IsCalendarSelected => SelectedFlter is CalendarSelectedDays;
        public void SetByCarouselDaySelecting(DateTime dateTime)
        {
            SortInDate tempSelectedFilter = FilterTypes.FirstOrDefault(sortInDate => sortInDate is CarouselSelectedDay);
            if (tempSelectedFilter is CarouselSelectedDay carouselSelectedDay)
            {
                carouselSelectedDay.Date = dateTime;
                SelectedFlter = tempSelectedFilter;
            }
            else
                throw new Exception(message: $"There is no {nameof(CarouselSelectedDay)} in the collection.");
        }

        public void SetDateInCarouselDaySelecting(DateTime dateTime)
        {
            SortInDate tempSelectedFilter = FilterTypes.FirstOrDefault(sortInDate => sortInDate is CarouselSelectedDay);
            if (tempSelectedFilter is CarouselSelectedDay carouselSelectedDay)
                carouselSelectedDay.Date = dateTime;
            else
                throw new Exception(message: $"There is no {nameof(CarouselSelectedDay)} in the collection.");
        }

        public void SetByCalendarSelecting()
        {
            SortInDate tempSelectedFilter = FilterTypes.FirstOrDefault(sortInDate => sortInDate is CalendarSelectedDays);

            if (tempSelectedFilter is CalendarSelectedDays)
                SelectedFlter = tempSelectedFilter;
            else
                throw new Exception(message: $"There is no {nameof(CalendarSelectedDays)} in the collection.");
        }
        public IEnumerable<IPackNote> GetFiltered() => _filterPackNote.GetFiltered();
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

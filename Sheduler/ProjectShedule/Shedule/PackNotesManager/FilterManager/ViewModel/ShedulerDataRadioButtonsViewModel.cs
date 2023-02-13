using ProjectShedule.Core.RadioButton;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Language.Resources.OtherElements;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Base;
using System;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.ViewModel
{
    public class ShedulerDataRadioButtonsViewModel : RadioButtonsViewModel, IConvertRadioButtonsViewModel<SortBaseNote>
    {
        private readonly SortBaseNote[] _sortBaseNotes;

        private readonly CalendarSelectedDays _calendarSelectedDays;
        private readonly CarouselSelectedDay _carouselSelectedDay;

        public ShedulerDataRadioButtonsViewModel(IGetItemsDateTime<Note> getItems, DateTime startedDateTime, IEnumerable<DateTime> selectedDateTimes)
        {
            _calendarSelectedDays = new CalendarSelectedDays(getItems, selectedDateTimes);
            _carouselSelectedDay = new CarouselSelectedDay(getItems, startedDateTime);
            _sortBaseNotes = new SortBaseNote[]
            {
                _carouselSelectedDay,
                _calendarSelectedDays,
                new AllSort(getItems)
            };
            Items = _sortBaseNotes;
            GroupName = "DataGroup";
            SelectedItem = Items[0];
            Wrap = Xamarin.Forms.FlexWrap.Wrap;
            Direction = Xamarin.Forms.FlexDirection.Row;
        }

        public bool IsCarouselSelected => SelectedItem is CarouselSelectedDay;
        public bool IsCalendarSelected => SelectedItem is CalendarSelectedDays;

        public CarouselSelectedDay CarouselSelectedDay => _carouselSelectedDay;
        public CalendarSelectedDays CalendarSelectedDays => _calendarSelectedDays;

        public SortBaseNote[] ConvertedItems => _sortBaseNotes;
        public SortBaseNote ConvertedSelectedItem => (SortBaseNote)SelectedItem;

        public void SetByCarouselSelected() => SelectedItem = _carouselSelectedDay;
    }
}

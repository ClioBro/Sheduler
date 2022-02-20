﻿using ProjectShedule.Calendar.Models;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.PackNotesManager;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.ViewModel;
using ProjectShedule.Shedule.ShapeEvents;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProjectShedule.Shedule.Models
{
    public partial class SheduleModel
    {
        #region Fields
        private readonly ObservableCollection<PackNoteViewModel> _packNotes;
        private List<PackNoteViewModel> _selectedPackNotes;
        private IEnumerable<ICircleEvent> _calendarCircleEvents;

        private List<DateTime> _selectedDates;
        private DateTime _displayedDateOnCarousel;

        private FilterViewModel _filterPackNotes;
        #endregion

        public delegate bool QuestionForDelete();
        public QuestionForDelete ConfirmationOperation;
        public delegate void PackNoteViewModelDelegate(PackNoteViewModel packNoteViewModel);

        public event PackNoteViewModelDelegate PackNoteDeleted;
        public event Action PackNoteListUpdated;
        public event Action SelectedPackNotesChanged;
        public event Action CalendarCirleEventsUpdated;
        public event Action SelectedDatesChanged;
        public event Action DisplayedDateChanged;
        

        private readonly PackNoteDataBaseController _packNoteDB;
        public SheduleModel()
        {
            _packNotes = new ObservableCollection<PackNoteViewModel>();
            _calendarCircleEvents = new List<ICircleEvent>();
            _packNoteDB = new PackNoteDataBaseController();
            _selectedPackNotes = new List<PackNoteViewModel>();
            _selectedDates = new List<DateTime>();
            FilterPackNotes = new FilterViewModel();
            _filterPackNotes.PropertyChanged += (sender, propName) => UpdatePackNotes();
        }

        
        #region Properties
        public ICommand DeletePackNoteCommand { get; set; }
        public ICommand EditPackNoteCommand { get; set; }

        public ICommand DeleteTaskCommand { get; set; }
        public ICommand TaskCheckCommand { get; set; }
        public FilterViewModel FilterPackNotes { get => _filterPackNotes; set => _filterPackNotes = value; }
        public ObservableCollection<PackNoteViewModel> PackNotes => _packNotes;
        public IEnumerable<ICircleEvent> CalendarCircleEvents => _calendarCircleEvents;
        public List<PackNoteViewModel> SelectedPackNotes
        {
            get => _selectedPackNotes;
            set 
            {
                _selectedPackNotes = value;
                SelectedPackNotesChanged?.Invoke();
            } 
        }
        public List<DateTime> SelectedDates
        {
            get => _selectedDates;
            set
            {
                if (value.Count() >= 1)
                {
                    DisplayedDateOnCarousel = value.First();
                }
                else
                {
                    DisplayedDateOnCarousel = DateTime.Today;
                }
                _selectedDates = value;
                SelectedDatesChanged?.Invoke();
            }
        }
        public DateTime DisplayedDateOnCarousel
        {
            get => _displayedDateOnCarousel;
            set
            {
                if (_displayedDateOnCarousel != value)
                {
                    _displayedDateOnCarousel = value;
                    FilterPackNotes.SetBySelectedDate(value);
                    UpdatePackNotes();
                    DisplayedDateChanged?.Invoke();
                }
            }
        }
        #endregion
        public void UpdatePackNotes()
        {
            _packNotes.Clear();
            foreach (PackNoteModel packNote in _filterPackNotes.GetFiltered())
            {
                PackNoteViewModel newPNVM = new PackNoteViewModel(packNote)
                {
                    DeleteMeCommand = DeletePackNoteCommand,
                    EditMeCommand = EditPackNoteCommand,
                };
                newPNVM.TaskCheckChanged += SmallTaskCheckChangedEventHandler;
                newPNVM.TaskDeletePressed += SmallTaskDeletedEventHandler;
                _packNotes.Add(newPNVM);
            }
            PackNoteListUpdated?.Invoke();
        }

        public void UpdateEvents()
        {
            SheduleEventsCreater evemtsManager = new SheduleEventsCreater();
            DateTime minDate = new DateTime(DisplayedDateOnCarousel.Year, DisplayedDateOnCarousel.Month, DisplayedDateOnCarousel.Day).AddDays(-7);
            DateTime maxDate = new DateTime(DisplayedDateOnCarousel.Year, DisplayedDateOnCarousel.Month, DisplayedDateOnCarousel.Day).AddDays(7);
            _calendarCircleEvents = evemtsManager.Create(minDate, maxDate);
            CalendarCirleEventsUpdated?.Invoke();
        }
        public void DeletePackNote(PackNoteViewModel packNoteViewModel)
        {
            if (_packNotes.Remove(packNoteViewModel))
            {
                var repository = new PackNoteDataBaseController();
                repository.Delete(packNoteViewModel.Model);
                PackNoteDeleted?.Invoke(packNoteViewModel);
                UpdateEvents();
            }
        }

        private void SmallTaskCheckChangedEventHandler(IHasSmallTask hasSmallTask)
        {
            _packNoteDB.SaveInDataBase(hasSmallTask.SmallTask);
        }
        private void SmallTaskDeletedEventHandler(IHasSmallTask hasSmallTask)
        {
            _packNoteDB.DeleteInDataBase(hasSmallTask.SmallTask);
        }
        
    }
}

using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.DataBase.Interfaces;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.ViewModel;
using ProjectShedule.Shedule.ShapeEvents;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ProjectShedule.Shedule.Models
{
    public abstract class BaseSheduleModel
    {
        #region Fields
        protected IPackNoteDataBaseController _packNoteDataBaseController;

        protected ObservableRangeCollection<BasePackNoteViewModel> _packNotes;
        protected ReadOnlyObservableCollection<BasePackNoteViewModel> _readOnlyPackNotes;

        protected ObservableRangeCollection<CircleEventModel> _calendarCircleEvents;
        protected ReadOnlyObservableCollection<CircleEventModel> _readOnlyCircleEvents;

        protected ObservableRangeCollection<BasePackNoteViewModel> _selectedPackNotes;
        protected ObservableRangeCollection<DateTime> _selectedDates;

        protected DateTime _displayedDateOnCarousel;

        protected FilterPackNoteViewModel _filterPackNotes;
        protected IBuilderCalendarCircleEvent _builderCircleEvent;
        protected IBuilderPackNoteViewModel _builderPackNoteViewModel;
        #endregion

        #region Properties
        public ICommand DeletePackNoteCommand { get; set; }
        public ICommand EditPackNoteCommand { get; set; }

        public ICommand DeleteTaskCommand { get; set; }
        public ICommand TaskCheckCommand { get; set; }
        public FilterPackNoteViewModel FilterPackNotes => _filterPackNotes;
        public ReadOnlyObservableCollection<BasePackNoteViewModel> PackNotes => _readOnlyPackNotes;
        public ReadOnlyObservableCollection<CircleEventModel> CalendarCircleEvents => _readOnlyCircleEvents;
        public ObservableRangeCollection<BasePackNoteViewModel> SelectedPackNotes => _selectedPackNotes;
        public ObservableRangeCollection<DateTime> SelectedDates => _selectedDates;
        public abstract DateTime DisplayedDateOnCarousel { get; set; }
        #endregion
        public abstract void UpdatePackNotes();
        public abstract void UpdateEvents();
        public abstract void DeletePackNote(BasePackNoteViewModel packNoteViewModel);
        public abstract void SavePackNote(BasePackNoteViewModel packNote);
    }
}

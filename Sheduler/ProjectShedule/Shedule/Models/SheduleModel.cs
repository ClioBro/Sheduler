using ProjectShedule.DataBase.Entities.Base;
using ProjectShedule.Language.Resources.OtherElements;
using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.DataBase.Interfaces;
using ProjectShedule.Shedule.PackNotesManager.FilterManager;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.ViewModel;
using ProjectShedule.Shedule.ShapeEvents;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ProjectShedule.Shedule.Models
{
    public partial class SheduleModel : BaseSheduleModel
    {
        private readonly DateTime _startedDateTime = DateTime.Today;
        public SheduleModel(IPackNoteDataBaseController packNoteDataBaseController, IBuilderPackNoteViewModel builderPackNoteViewModel)
        {
            _packNoteDataBaseController = packNoteDataBaseController;
            _builderPackNoteViewModel = builderPackNoteViewModel;

            FieldsInicialization();
            InicializationFilterPackNotes();
            
            _selectedDates.CollectionChanged += (sender, e) =>
            {
                if (_filterPackNotes.SelectedFlter is CalendarSelectedDays)
                    UpdatePackNotes();
            };
            DisplayedDateOnCarousel = _startedDateTime;
        }
        private void FieldsInicialization()
        {
            _displayedDateOnCarousel = DateTime.Today;
            _packNotes = new ObservableRangeCollection<BasePackNoteViewModel>();
            _readOnlyPackNotes = new ReadOnlyObservableCollection<BasePackNoteViewModel>(_packNotes);
            _calendarCircleEvents = new ObservableRangeCollection<CircleEventModel>();
            _readOnlyCircleEvents = new ReadOnlyObservableCollection<CircleEventModel>(_calendarCircleEvents);
            _selectedPackNotes = new ObservableRangeCollection<BasePackNoteViewModel>();
            _selectedDates = new ObservableRangeCollection<DateTime>();
            _builderCircleEvent = new BuilderCalendarCircleEvents(_packNoteDataBaseController.PartsControl.NoteRepository);
        }
        private void InicializationFilterPackNotes()
        {
            SortInDate[] FilterTypes = new SortInDate[]
            {
                new CarouselSelectedDay(_packNoteDataBaseController)
                {
                    Text = Filters.ByCarouselDate,
                    Date = _startedDateTime
                },
                new CalendarSelectedDays(_packNoteDataBaseController) { Text = Filters.ByCalendar, Dates = _selectedDates },
                new AllSortInDate(_packNoteDataBaseController) { Text = Filters.AllItems },
            };
            PutInOrderNote[] OrderTypes = new PutInOrderNote[]
            {
                new PutInOrderNoteByDate{ Text = Filters.ByDate},
                new PutInOrderNoteByAlphabet{ Text = Filters.ByAlphabetically},
            };

            _filterPackNotes = new FilterPackNoteViewModel(FilterTypes, OrderTypes);
        }

        public override DateTime DisplayedDateOnCarousel
        {
            get => _displayedDateOnCarousel;
            set
            {
                _displayedDateOnCarousel = value;
                _filterPackNotes.SetDayInCarosuelSelecting(value);
                if (_filterPackNotes.SelectedFlter is CarouselSelectedDay)
                {
                    UpdatePackNotes();
                    UpdateEvents();
                }
            }
        }

        public override void UpdatePackNotesAsync()
        {
            Task.Run(() =>
            {
                UpdatePackNotes();
                UpdateEvents();
            });
        }
        public override void UpdatePackNotes()
        {
            List<BasePackNoteViewModel> tempNotes = new List<BasePackNoteViewModel>();
            foreach (BasePackNoteModel packNoteModel in _filterPackNotes.GetFiltered())
            {
                BasePackNoteViewModel basePackNoteVM = _builderPackNoteViewModel
                    .SetDeleteMeCommand(DeletePackNoteCommand)
                    .SetEditMeCommand(EditPackNoteCommand)
                    .SetModel(packNoteModel)
                    .Build();

                basePackNoteVM.TaskCheckChanged += SmallTaskCheckChangedEventHandler;
                basePackNoteVM.TaskDeletePressed += SmallTaskDeletedEventHandler;

                tempNotes.Add(basePackNoteVM);
            }
            _packNotes.ReplaceRange(tempNotes);
        }
        public override void UpdateEvents()
        {
            _calendarCircleEvents.ReplaceRange(BuildCircleEvents());
        }
        public override void SavePackNote(BasePackNoteViewModel packNoteViewModel)
        {
            IHasModel<BasePackNoteModel> hasModel = packNoteViewModel;
            _packNoteDataBaseController.Save(hasModel.Model);
        }
        public override void DeletePackNote(BasePackNoteViewModel packNoteViewModel)
        {
            if (_packNotes.Remove(packNoteViewModel))
            {
                IHasModel<BasePackNoteModel> hasModel = packNoteViewModel;
                _packNoteDataBaseController.Delete(hasModel.Model);

                int noteID = hasModel.Model.Note.Id;
                DeleteCircleEvent(noteID);
            }
        }
        private void DeleteCircleEvent(int id)
        {
            CircleEventModel circleEventModel = _calendarCircleEvents
                    .FirstOrDefault(c => c.ID == id);
            DeleteCircleEvent(circleEventModel);
        }
        private void DeleteCircleEvent(CircleEventModel circleEventModel) => _calendarCircleEvents.Remove(circleEventModel);
        
        private void SmallTaskCheckChangedEventHandler(BaseSmallTaskViewModel hasSmallTask)
        {
            _packNoteDataBaseController.PartsControl.SaveInDataBase(((IHasModel<BaseSmallTask>)hasSmallTask).Model);
        }
        private void SmallTaskDeletedEventHandler(BaseSmallTaskViewModel hasSmallTask)
        {
            _packNoteDataBaseController.PartsControl.DeleteInDataBase(((IHasModel<BaseSmallTask>)hasSmallTask).Model);
        }
        
        private IEnumerable<CircleEventModel> BuildCircleEvents()
        {
            DateTime minDate = new DateTime(_displayedDateOnCarousel.Year, _displayedDateOnCarousel.Month, _displayedDateOnCarousel.Day).AddMonths(-1).AddDays(-5);
            DateTime maxDate = new DateTime(_displayedDateOnCarousel.Year, _displayedDateOnCarousel.Month, _displayedDateOnCarousel.Day).AddMonths(1).AddDays(12);

            return _builderCircleEvent.Build(minDate, maxDate);
        }
    }
}

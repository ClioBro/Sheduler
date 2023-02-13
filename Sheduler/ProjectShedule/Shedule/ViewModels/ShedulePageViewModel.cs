using ProjectShedule.Core;
using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Language.Resources.Pages.AppFlyout;
using ProjectShedule.PopUpAlert;
using ProjectShedule.PopUpAlert.Question;
using ProjectShedule.Shedule.Builder;
using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.ViewModel;
using ProjectShedule.Shedule.Service;
using ProjectShedule.Shedule.ShapeEvents;
using ProjectShedule.Shedule.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels
{
    public class ShedulePageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Fields
        private readonly ObservableRangeCollection<NoteViewModel> _notesViewModels = new ObservableRangeCollection<NoteViewModel>();
        private readonly ReadOnlyObservableCollection<NoteViewModel> _readOnlyNotesViewModels;

        private readonly ObservableRangeCollection<CircleEventModel> _eventsForCalendar = new ObservableRangeCollection<CircleEventModel>();
        private readonly ReadOnlyObservableCollection<CircleEventModel> _readOnlyEventsForCalendar;

        private readonly IBuilderCalendarCircleEvent _builderCalendarCircleEvent;
        private readonly NoteViewModelBuilder _noteViewModelBuilder;

        private readonly INavigation _navigation;
        private readonly ObservableRangeCollection<DateTime> _calendarSelectedDates = new ObservableRangeCollection<DateTime>();
        private readonly ServiceToOpenNoteEditorPageWriteInDataBase _noteEditorPageService;
        private ICommand _noteDeleteCommand;
        private ICommand _noteEditCommand;
        private readonly ShedulePageModel _shedulePageModel;
        #endregion Fields

        #region Constructor
        public ShedulePageViewModel(INavigation navigation)
        {
            try
            {
                _navigation = navigation;
                _shedulePageModel = new ShedulePageModel();
                _noteEditorPageService = new ServiceToOpenNoteEditorPageWriteInDataBase(_navigation, _shedulePageModel);
                _noteEditorPageService.Saved += (sender, e) => UpdateContents();
                _noteViewModelBuilder = new NoteViewModelBuilder();
                InicializationCommands();

                SetCommandsInBuilder(_noteViewModelBuilder);

                _readOnlyNotesViewModels = new ReadOnlyObservableCollection<NoteViewModel>(_notesViewModels);
                _readOnlyEventsForCalendar = new ReadOnlyObservableCollection<CircleEventModel>(_eventsForCalendar);
                _builderCalendarCircleEvent = new CalendarCircleEventsBuilder(_shedulePageModel.DataBaseGetItemsDateTime);

                FilterNoteViewModel = new SheduleNoteFilterViewModel(_shedulePageModel.DataBaseGetItemsDateTime, DateTime.Now, _calendarSelectedDates);
                FilterNoteViewModel.PropertyChanged += (sender, e) => UpdateContents();

                _calendarSelectedDates.CollectionChanged += (sender, e) =>
                {
                    if (FilterNoteViewModel.ShedulerDataRadioButtonsViewModel.IsCalendarSelected)
                        UpdateContents();
                    else
                        FilterNoteViewModel.ShedulerDataRadioButtonsViewModel.SetByCarouselSelected();
                };

                UpdateContents();
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }
        #endregion Constructor

        #region Inicializations
        private void InicializationCommands()
        {
            _noteDeleteCommand = new Command<NoteViewModel>(TryDeletePackNote);
            _noteEditCommand = new Command<NoteViewModel>(OpenEditPage);
            
            ExpandedCalendarCommand = new Command(ExpandCalendar);
            OpenEditorCommand = new Command(OpenEditorPage);
            CalendarDayLongPressedCommand = new Command<DateTime>(OpenEditorPageOnDate);
            MoveToDayCommand = new Command(() => DisplayedDateOnCarousel = DateTime.Today);
        }
        private void SetCommandsInBuilder(NoteViewModelBuilder sheduleNoteViewModelBuilder)
        {
            sheduleNoteViewModelBuilder
                .SetQuestionConfirmation(QuestionForDeleteAsync)
                .SetEditCommand(_noteEditCommand)
                .SetDeleteCommand(_noteDeleteCommand);
        }

        #endregion Inicializations

        #region BindableProperties
        public ReadOnlyObservableCollection<NoteViewModel> NotesViewModels => _readOnlyNotesViewModels;
        public SheduleNoteFilterViewModel FilterNoteViewModel { get; set; }

        #region DateCalendar
        public DateTime DisplayedDateOnCarousel
        {
            get => FilterNoteViewModel.ShedulerDataRadioButtonsViewModel.CarouselSelectedDay.Date;
            set
            {
                var shedulerDataRadioButtonsViewModel = FilterNoteViewModel.ShedulerDataRadioButtonsViewModel;
                if (shedulerDataRadioButtonsViewModel.CarouselSelectedDay.Date == value)
                    return;

                shedulerDataRadioButtonsViewModel.CarouselSelectedDay.Date = value;

                if (shedulerDataRadioButtonsViewModel.IsCarouselSelected == false)
                    shedulerDataRadioButtonsViewModel.SetByCarouselSelected();

                UpdateContents();
                OnPropertyChanged();
            }
        }
        public ObservableRangeCollection<DateTime> CalendarSelectedDates => _calendarSelectedDates;

        public ReadOnlyObservableCollection<CircleEventModel> EventsForCalendar => _readOnlyEventsForCalendar;
        #endregion DateCalendar

        #region Commands
        public ICommand OpenEditorCommand { get; private set; }
        public ICommand ExpandedCalendarCommand { get; private set; }
        public ICommand MoveToDayCommand { get; private set; }
        public ICommand CalendarDayLongPressedCommand { get; set; }
        #endregion

        #region ForView
        public bool ExpandedCalendar { get; set; }
        public string Title => Lobby.SheduleTitle;
        #endregion

        #endregion BindableProperties

        private void UpdateContents()
        {
            IList<NoteViewModel> noteViewModels = new List<NoteViewModel>();
            IEnumerable<Note> notes = FilterNoteViewModel.Filter();
            foreach (Note note in notes)
            {
                _noteViewModelBuilder.SetData(note);
                NoteViewModel noteViewModel = _noteViewModelBuilder.Build();
                noteViewModels.Add(noteViewModel);
            }

            _notesViewModels.ReplaceRange(noteViewModels);
            AssigmentEvents(_notesViewModels);
            UpdateEvents();
        }
        private void UpdateEvents()
        {
            _builderCalendarCircleEvent.SetRange(GetDateTimeRange());
            _eventsForCalendar.ReplaceRange(_builderCalendarCircleEvent.Build());
        }

        private void AssigmentEvents(IEnumerable<NoteViewModel> noteViewModels)
        {
            foreach (var noteVM in noteViewModels)
                AssigmentEvents(noteVM);
        }
        private void AssigmentEvents(NoteViewModel noteViewModel)
        {
            noteViewModel.SmallTasksCollectionChanged += NoteViewModel_SmallTasksCollectionChanged;
        }

        private void NoteViewModel_SmallTasksCollectionChanged(object sender, SmallTasksCollectionChangedEventArgs<DeletableSmallTaskViewModel> e)
        {
            if (e.Action is NotifyCollectionChangedAction.Remove)
            {
                foreach (var s in e.OldItems)
                    DeleteSmallTaskInDataBase(s);
            }
        }


        #region CommandsHandlers
        private async void TryDeletePackNote(NoteViewModel sheduleNoteViewModel)
        {
            var result = await QuestionForDeleteAsync(sheduleNoteViewModel);
            if (result.Value is true)
            {
                _notesViewModels.Remove(sheduleNoteViewModel);
                DeleteNoteInDataBase(sheduleNoteViewModel);
                UpdateEvents();
            }
        }
        private void ExpandCalendar()
        {
            ExpandedCalendar = !ExpandedCalendar;
            OnPropertyChanged(nameof(ExpandedCalendar));
        }

        #region EditorPageOpenActions

        private void OpenEditorPage() => OpenEditorPageOnDate(DateTime.Now);
        private void OpenEditorPageOnDate(DateTime dateTime) => _noteEditorPageService.OpenEditorAsync(dateTime);
        private void OpenEditPage(IHasData<Note> note)
        {
            _noteEditorPageService.OpenEditorAsync(note);
        }

        #endregion EditorPageOpenActions

        #endregion CommandsHandlers

        #region Убрать в другой класс
        private void DeleteNoteInDataBase(IHasData<Note> note)
        {
            _shedulePageModel.Delete(note);
        }
        private void DeleteSmallTaskInDataBase(IHasData<SmallTask> smallTask)
        {
            _shedulePageModel.Delete(smallTask);
        }
        private async Task<QuestionView.Answer> QuestionForDeleteAsync(IHasHeader hasHeader)
        {
            return await _navigation.ShowQuestionForDeletionAsync(hasHeader.Header);
        }

        private DateTimeRange GetDateTimeRange()
        {
            DateTime dateTime = DisplayedDateOnCarousel;
            DateTime minDate = dateTime.AddMonths(-1).AddDays(-5);
            DateTime maxDate = dateTime.AddMonths(1).AddDays(12);
            return new DateTimeRange(minDate, maxDate);
        }
        #endregion Убрать в другой класс

        private async void ShowException(Exception exception)
        {
            await _navigation.ShowExceptionViewAsync(exception);
        }
    }
}
using ProjectShedule.Calendar.Models;
using ProjectShedule.GlobalSetting;
using ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete;
using ProjectShedule.PopUpAlert;
using ProjectShedule.Shedule.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels
{
    public class SheduleViewModel : INotifyPropertyChanged
    {
        private readonly SheduleModel _sheduleModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public SheduleViewModel()
        {
            _sheduleModel = new SheduleModel();
            Title = Resources.AppResources.ShedulePageTitle;
            AssigmentCommandEvents();
        }

        #region Properties
        public ICommand OpenEditorCommand { get; set; }
        public ICommand ExpandedCalendarCommand { get; set; }
        public ICommand MoveToDayCommand { get; private set; }
        public INavigation Navigation { get; set; }
        public bool ExpandedCalendar { get; set; }
        public string Title { get; set; }
        public ObservableCollection<PackNoteViewModel> PackNotes => _sheduleModel.PackNotes;
        public FilterViewModel FilterControl { get => _sheduleModel.FilterPackNotes; }
        public IEnumerable<ICircleEvent> EventsForCalendar => _sheduleModel.CalendarCircleEvents;
        public List<PackNoteViewModel> SelectedPackNotes
        {
            get => _sheduleModel.SelectedPackNotes;
            set => _sheduleModel.SelectedPackNotes = value;
        }
        public List<DateTime> SelectedDates
        {
            get => _sheduleModel.SelectedDates;
            set
            {
                if (value is List<DateTime> newDate && newDate.FirstOrDefault() != DateTime.MinValue)
                {
                    _sheduleModel.DisplayedDateOnCarousel = newDate.FirstOrDefault();
                }
                _sheduleModel.SelectedDates = value;
            }
        }
        public DateTime DisplayedDateTime
        {
            get => _sheduleModel.DisplayedDateOnCarousel;
            set => _sheduleModel.DisplayedDateOnCarousel = value;
        }
        #endregion

        private void AssigmentCommandEvents()
        {
            _sheduleModel.DeletePackNoteCommand = new Command<PackNoteViewModel>(DeletePackNoteCommandHandler);
            _sheduleModel.EditPackNoteCommand = new Command<PackNoteViewModel>(OpenEditorCommandHandler);

            ExpandedCalendarCommand = new Command(ExpandCalendar);
            OpenEditorCommand = new Command(OpenEditorCommandHandler);
            MoveToDayCommand = new Command(() => _sheduleModel.DisplayedDateOnCarousel = DateTime.Today);

            _sheduleModel.PackNoteListUpdated += () => OnPropertyChanged(nameof(PackNotes));
            _sheduleModel.SelectedPackNotesChanged += () => OnPropertyChanged(nameof(SelectedPackNotes));
            _sheduleModel.CalendarCirleEventsUpdated += () => OnPropertyChanged(nameof(EventsForCalendar));
            _sheduleModel.SelectedDatesChanged += () => OnPropertyChanged(nameof(SelectedDates));
            _sheduleModel.DisplayedDateChanged += () => OnPropertyChanged(nameof(DisplayedDateTime));


            _sheduleModel.DisplayedDateOnCarousel = DateTime.Today; // UpdatePackNotes
            _sheduleModel.UpdateEvents();
        }
        private async void OpenEditorCommandHandler()
        {
            if (EditorPackNotePage.IsPageOpened)
                return;
            EditorPackNotePage editorPage = CreaterPageManager
                .CreateEditorPage(SavePressedCallBack: UpdatePage);
            await Navigation.PushModalAsync(editorPage);
        }
        private async void OpenEditorCommandHandler(PackNoteViewModel packNoteViewModel)
        {
            if (EditorPackNotePage.IsPageOpened)
                return;
            EditorPackNotePage editorPage = CreaterPageManager
                .CreateEditorPage(SavePressedCallBack: UpdatePage, packNoteViewModel.Model);
            await Navigation.PushModalAsync(editorPage);
        }
        private async void DeletePackNoteCommandHandler(PackNoteViewModel packNoteViewModel)
        {
            IDeleteConfirmation deleteConfirmation = new DeleteConfirmationSetting();
            if (deleteConfirmation.AskQuestion)
            {
                string header = packNoteViewModel.Header;
                string createdText = PopUpAlert.Question.QuestionResource.CreationTimeLabel;
                string createdDateTime = packNoteViewModel.CreatedDateTime.ToString();
                string createdDateTimeText = $"{createdText}: {createdDateTime}";
                var answer = await Navigation.ShowQuestionForDeletion(itemNameToBeDeleted: header, dopText: createdDateTimeText);
                if (answer.Value == false)
                    return;
            }
            _sheduleModel.DeletePackNote(packNoteViewModel);
        }
        private void UpdatePage()
        {
            _sheduleModel.UpdatePackNotes();
            _sheduleModel.UpdateEvents();
        }

        private void ExpandCalendar()
        {
            ExpandedCalendar = !ExpandedCalendar;
            OnPropertyChanged(nameof(ExpandedCalendar));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "" )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

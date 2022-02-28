using ProjectShedule.GlobalSetting;
using ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete;
using ProjectShedule.Language.Resources.Pages.AppFlyout;
using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
            Title = Lobby.SheduleTitle;
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
        public FilterViewModel FilterControl => _sheduleModel.FilterPackNotes;
        public IEnumerable<ICircleEvent> EventsForCalendar => _sheduleModel.CalendarCircleEvents;
        public List<DateTime> SelectedDates
        {
            get => _sheduleModel.SelectedDates;
            set => _sheduleModel.SelectedDates = value;
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
            _sheduleModel.CalendarCirleEventsUpdated += () => OnPropertyChanged(nameof(EventsForCalendar));
            _sheduleModel.DisplayedDateChanged += () => OnPropertyChanged(nameof(DisplayedDateTime));


            _sheduleModel.DisplayedDateOnCarousel = DateTime.Today;
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
                var answer = await Navigation.ShowQuestionForDeletion(itemNameToBeDeleted: header);
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

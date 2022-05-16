using ProjectShedule.GlobalSetting;
using ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete;
using ProjectShedule.Language.Resources.Pages.AppFlyout;
using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.PackNotesManager.WorkWithDataBase;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels
{
    public class SheduleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly BaseSheduleModel _sheduleModel;

        private readonly IBuilderSmallTaskViewModel _builderSmallTaskViewModel;
        private readonly IBuilderPackNote _builderPackNoteModel;
        private readonly IBuilderPackNoteViewModel _builderPackNoteViewModel;
        private readonly IBuilderPackNoteEditorPage _builderPackNoteEditorPage;

        private readonly IPackNoteDataBaseController _dataBaseController;
        public SheduleViewModel()
        {
            Title = Lobby.SheduleTitle;
            _builderPackNoteEditorPage = new BuilderPackNoteEditorPage();
            _builderPackNoteViewModel = new BuilderPackNoteViewModel();
            _builderSmallTaskViewModel = new BuilderSmallTaskViewModel();
            _builderPackNoteModel = new BuilderPackNoteModel(_builderSmallTaskViewModel);

            _dataBaseController = new PackNoteDataBaseController(App.ApplicationContext);
            _sheduleModel = new SheduleModel(_dataBaseController, _builderPackNoteViewModel);

            AssigmentCommands();
        }
        #region Properties
        public ReadOnlyObservableCollection<BasePackNoteViewModel> PackNotes => _sheduleModel.PackNotes;
        public ReadOnlyObservableCollection<CircleEventModel> EventsForCalendar => _sheduleModel.CalendarCircleEvents;
        public BaseFilterViewModel FilterControl => _sheduleModel.FilterPackNotes;
        public ObservableCollection<DateTime> SelectedDates => _sheduleModel.SelectedDates;
        public DateTime DisplayedDateTime
        {
            get => _sheduleModel.DisplayedDateOnCarousel;
            set
            {
                _sheduleModel.DisplayedDateOnCarousel = value;
                OnPropertyChanged(nameof(DisplayedDateTime));
            }
        }
        public ICommand OpenEditorCommand { get; set; }
        public ICommand ExpandedCalendarCommand { get; set; }
        public ICommand MoveToDayCommand { get; private set; }
        public INavigation Navigation { get; set; }
        public bool ExpandedCalendar { get; set; }
        public string Title { get; set; }
        #endregion

        private void AssigmentCommands()
        {
            _sheduleModel.DeletePackNoteCommand = new Command<BasePackNoteViewModel>(DeletePackNoteCommandHandler);
            _sheduleModel.EditPackNoteCommand = new Command<BasePackNoteViewModel>(OpenEditorCommandHandler);

            ExpandedCalendarCommand = new Command(ExpandCalendar);
            OpenEditorCommand = new Command(OpenEditorCommandHandler);
            MoveToDayCommand = new Command(() => DisplayedDateTime = DateTime.Today);
        }
        private void OpenEditorCommandHandler()
        {
            AttemptOpenEditor(_builderPackNoteModel.Build());
        }
        private void OpenEditorCommandHandler(BasePackNoteViewModel packNoteViewModel)
        {
            IHasModel<BasePackNoteModel> hasModel = packNoteViewModel;
            BasePackNoteModel cloneBasePackNoteModel = hasModel.Model.Clone() as BasePackNoteModel;
            AttemptOpenEditor(cloneBasePackNoteModel);
        }
        private async void AttemptOpenEditor(BasePackNoteModel basePackNoteModel)
        {
            if (EditorPackNotePage.IsPageOpened)
                return;

            ContentPage contentPage = _builderPackNoteEditorPage
                .SetEditTarget(basePackNoteModel)
                .SetSavePressedCallBack(SavedPackNoteEventHandler)
                .SetDataBaseController(_dataBaseController)
                .Build();
            await Navigation.PushModalAsync(contentPage);

            void SavedPackNoteEventHandler(ReadOnlyPackNote savedPackNote)
            {
                _sheduleModel.UpdatePackNotesAsync();
            }
        }

        private async void DeletePackNoteCommandHandler(BasePackNoteViewModel packNoteViewModel)
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

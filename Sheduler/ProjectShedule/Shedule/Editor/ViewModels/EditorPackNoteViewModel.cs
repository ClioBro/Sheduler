using ProjectShedule.Language.Resources.PopUp.Repeads;
using ProjectShedule.PopUpAlert;
using ProjectShedule.PopUpAlert.ColorSelection;
using ProjectShedule.Shedule.Editor.Models;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ProjectShedule.Shedule.Editor.ViewModels
{
    public class EditorPackNoteViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private readonly IBuilderPackNoteViewModel _builderPackNoteViewModel;
        private readonly BaseEditorPackNoteModel _baseEditorPackNote;
        public EditorPackNoteViewModel(EditorPackNoteModel editorPackNoteModel)
        {
            _baseEditorPackNote = editorPackNoteModel;
            _builderPackNoteViewModel = new BuilderPackNoteViewModel();

            SavePackNoteCommand = new Command(Save);
            AddTaskCommand = new Command(AddTask);
            ShowPackNoteViewCommand = new Command(ShowPackNoteView);
            ShowColorSelectionPageCommand = new Command(ShowColorSelectionPageAcync);
            ShowAvailableRepeadTypesCommand = new Command(ShowAvailableRepeads);

            _baseEditorPackNote.DeleteTaskCommand = new Command<BaseSmallTaskViewModel>(DeleteTask);

            _baseEditorPackNote.LineColorChanged += (object sender, Color e) => OnPropertyChanged(nameof(LineColor));
            _baseEditorPackNote.BackGroundColorChanged += (object sender, Color e) => OnPropertyChanged(nameof(BackGroundColor));

            _baseEditorPackNote.SelectedRepeadChanged += OnSelectedRepeadChanged;
            _baseEditorPackNote.SmallTasksAdded += (object sender, BaseSmallTaskViewModel e) => TaskAddingEntryText = string.Empty;
            
            _baseEditorPackNote.PackNoteSaved += PackNoteSavedEventHandler;
        }

        #region Commands
        public ICommand SavePackNoteCommand { get; private protected set; }
        public ICommand AddTaskCommand { get; private protected set; }
        public ICommand ShowPackNoteViewCommand { get; private protected set; }
        public ICommand ShowColorSelectionPageCommand { get; private protected set; }
        public ICommand ShowAvailableRepeadTypesCommand { get; private protected set; }
        #endregion

        #region Properties
        public string Header
        {
            get => _baseEditorPackNote.Header;
            set
            {
                _baseEditorPackNote.Header = value;
                OnPropertyChanged();
            }
        }
        public string DopText
        {
            get => _baseEditorPackNote.DopText;
            set
            {
                _baseEditorPackNote.DopText = value;
                OnPropertyChanged();
            }
        }
        public DateTime CreatedDateTime
        {
            get => _baseEditorPackNote.CreatedDateTime;
            set
            {
                _baseEditorPackNote.CreatedDateTime = value;
                OnPropertyChanged();
            }
        }
        public DateTime ReminderDateTime
        {
            get => _baseEditorPackNote.AppointmentDate;
            set
            {
                _baseEditorPackNote.AppointmentDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get => ReminderDateTime.Date;
            set
            {
                var time = ReminderDateTime.TimeOfDay;
                ReminderDateTime = value += time;
            }
        }
        public TimeSpan Time
        {
            get => ReminderDateTime.TimeOfDay;
            set
            {
                var date = ReminderDateTime.Date;
                ReminderDateTime = date += value;
            }
        }
        public bool DateSelected
        {
            get => _baseEditorPackNote.OnTheDate;
            set
            {
                _baseEditorPackNote.OnTheDate = value;
                OnPropertyChanged();
            }
        }
        public Color BackGroundColor
        {
            get => _baseEditorPackNote.BackGroundColor;
            set
            {
                _baseEditorPackNote.BackGroundColor = value;
                OnPropertyChanged();
            }
        }
        public Color LineColor
        {
            get => _baseEditorPackNote.LineColor;
            set
            {
                _baseEditorPackNote.LineColor = value;
                OnPropertyChanged();
            }
        }
        public bool HasSmallTasks => SmallTasks.Count() > 0;
        public bool EnableNotification
        {
            get => _baseEditorPackNote.Notify;
            set
            {
                if (_baseEditorPackNote.Notify != value)
                {
                    _baseEditorPackNote.Notify = value;
                    OnPropertyChanged();
                }
            }
        }
        public RepeadItem SelectedRepead
        {
            get => _baseEditorPackNote.SelectedRepead;
            set
            {
                if (_baseEditorPackNote.SelectedRepead != value)
                {
                    _baseEditorPackNote.SelectedRepead = value;
                    OnPropertyChanged();
                }
            }
        }
        public string SelectedRepeadText => _baseEditorPackNote.SelectedRepead.Text;
        public ReadOnlyObservableCollection<BaseSmallTaskViewModel> SmallTasks => _baseEditorPackNote.SmallTasks;

        private string _taskAddingEntryText;
        public string TaskAddingEntryText 
        { 
            get => _taskAddingEntryText; 
            set
            {
                _taskAddingEntryText = value;
                OnPropertyChanged();
            } 
        }
        #endregion
        public INavigation Navigation { get; set; }

        private void AddTask()
        {
            _baseEditorPackNote.AddNewSmallTask(TaskAddingEntryText);
        }
        private void DeleteTask(BaseSmallTaskViewModel smallTask)
        {
            _baseEditorPackNote.RemoveTask(smallTask);
        }
        private async void ShowPackNoteView()
        {
            if (DemonstrationViewPackNote.isPageOpened)
                return;

            Rg.Plugins.Popup.Pages.PopupPage popUpPage = 
                new DemonstrationViewPackNote(_builderPackNoteViewModel
                                                .SetModel(_baseEditorPackNote.BasePackNoteModel)
                                                .Build());

            await Navigation.PushPopupAsync(popUpPage);
        }
        private async void ShowColorSelectionPageAcync()
        {
            if (ColorSelectionPage.IsPageOpened)
                return;

            ColorSelectionPageCreation colorSelectionPageCreation = 
                new ColorSelectionPageCreation(_builderPackNoteViewModel
                                                .SetModel(_baseEditorPackNote.BasePackNoteModel)
                                                .Build());

            colorSelectionPageCreation.ColorSelection.LineTarget.ColorSelected += (sender, color) => LineColor = color;
            colorSelectionPageCreation.ColorSelection.BackGroundTarget.ColorSelected += (sender, color) => BackGroundColor = color;

            await Navigation.ShowColorSelection(colorSelectionPageCreation.Create());
        }
        private async void ShowAvailableRepeads()
        {
            if (RadioButtonsSelecterPage.IsPageOpened)
                return;
            RepeadItem[] repeadItems = CustomRepeads.RepeadsItems;

            var radioButtonPage = new RadioButtonsSelecterPage(repeadItems,
                                                repeadItems.IndexOf(SelectedRepead),
                                                Repeads.HeaderLabel);

            radioButtonPage.SelectedItemChanged
                += (sender, selectedItem)
                => SelectedRepead = (RepeadItem)selectedItem;

            await Navigation.ShowAvailableRepeadsAsync(radioButtonPage);
        }
        private void Save() => _baseEditorPackNote.Save();
        private async void ReturnNavigationPageAsync() => await Navigation.PopModalAsync();

        private void PackNoteSavedEventHandler(object sender, ReadOnlyPackNote readOnlyPackNote) => ReturnNavigationPageAsync();
        //private void OnSmallTasksChangedHandler(object sender, ReadOnlyObservableCollection<ISmallTaskViewModel> e)
        //{
        //    OnPropertyChanged(nameof(SmallTasks));
        //    OnPropertyChanged(nameof(TaskAddingEntryText));
        //}
        private void OnSelectedRepeadChanged(object sender, RepeadItem e)
        {
            OnPropertyChanged(nameof(SelectedRepeadText));
        }
    }
}

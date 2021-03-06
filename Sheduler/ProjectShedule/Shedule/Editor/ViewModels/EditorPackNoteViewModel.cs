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
        private readonly BaseEditorPackNoteModel _baseEditorPackNoteModel;
        public EditorPackNoteViewModel(EditorPackNoteModel editorPackNoteModel)
        {
            _baseEditorPackNoteModel = editorPackNoteModel;
            _builderPackNoteViewModel = new BuilderPackNoteViewModel();

            SavePackNoteCommand = new Command(Save);
            AddTaskCommand = new Command(AddTask);
            ShowPackNoteViewCommand = new Command(ShowPackNoteView);
            ShowColorSelectionPageCommand = new Command(ShowColorSelectionPageAcync);
            ShowAvailableRepeadTypesCommand = new Command(ShowAvailableRepeads);

            _baseEditorPackNoteModel.DeleteTaskCommand = new Command<BaseSmallTaskViewModel>(DeleteTask);

            _baseEditorPackNoteModel.LineColorChanged += (object sender, Color e) => OnPropertyChanged(nameof(LineColor));
            _baseEditorPackNoteModel.BackGroundColorChanged += (object sender, Color e) => OnPropertyChanged(nameof(BackGroundColor));

            _baseEditorPackNoteModel.SelectedRepeadChanged += OnSelectedRepeadChanged;
            _baseEditorPackNoteModel.SmallTasksAdded += (object sender, BaseSmallTaskViewModel e) => TaskAddingEntryText = string.Empty;
            
            _baseEditorPackNoteModel.PackNoteSaved += PackNoteSavedEventHandler;
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
            get => _baseEditorPackNoteModel.Header;
            set
            {
                _baseEditorPackNoteModel.Header = value;
                OnPropertyChanged();
            }
        }
        public string DopText
        {
            get => _baseEditorPackNoteModel.DopText;
            set
            {
                _baseEditorPackNoteModel.DopText = value;
                OnPropertyChanged();
            }
        }
        public DateTime CreatedDateTime
        {
            get => _baseEditorPackNoteModel.CreatedDateTime;
            set
            {
                _baseEditorPackNoteModel.CreatedDateTime = value;
                OnPropertyChanged();
            }
        }
        public DateTime ReminderDateTime
        {
            get => _baseEditorPackNoteModel.AppointmentDate;
            set
            {
                _baseEditorPackNoteModel.AppointmentDate = value;
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
            get => _baseEditorPackNoteModel.OnTheDate;
            set
            {
                _baseEditorPackNoteModel.OnTheDate = value;
                OnPropertyChanged();
            }
        }
        public Color BackGroundColor
        {
            get => _baseEditorPackNoteModel.BackGroundColor;
            set
            {
                _baseEditorPackNoteModel.BackGroundColor = value;
                OnPropertyChanged();
            }
        }
        public Color LineColor
        {
            get => _baseEditorPackNoteModel.LineColor;
            set
            {
                _baseEditorPackNoteModel.LineColor = value;
                OnPropertyChanged();
            }
        }
        public bool HasSmallTasks => SmallTasks.Count() > 0;
        public bool EnableNotification
        {
            get => _baseEditorPackNoteModel.Notify;
            set
            {
                if (_baseEditorPackNoteModel.Notify != value)
                {
                    _baseEditorPackNoteModel.Notify = value;
                    OnPropertyChanged();
                }
            }
        }
        public RepeadItem SelectedRepead
        {
            get => _baseEditorPackNoteModel.SelectedRepead;
            set
            {
                if (_baseEditorPackNoteModel.SelectedRepead != value)
                {
                    _baseEditorPackNoteModel.SelectedRepead = value;
                    OnPropertyChanged();
                }
            }
        }
        public string SelectedRepeadText => _baseEditorPackNoteModel.SelectedRepead.Text;
        public ReadOnlyObservableCollection<BaseSmallTaskViewModel> SmallTasks => _baseEditorPackNoteModel.SmallTasks;

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
            _baseEditorPackNoteModel.AddNewSmallTask(TaskAddingEntryText);
        }
        private void DeleteTask(BaseSmallTaskViewModel smallTask)
        {
            _baseEditorPackNoteModel.RemoveTask(smallTask);
        }
        private async void ShowPackNoteView()
        {
            if (DemonstrationViewPackNote.isPageOpened)
                return;

            Rg.Plugins.Popup.Pages.PopupPage popUpPage = 
                new DemonstrationViewPackNote(_builderPackNoteViewModel
                                                .SetModel(_baseEditorPackNoteModel.BasePackNoteModel)
                                                .Build());

            await Navigation.PushPopupAsync(popUpPage);
        }
        private async void ShowColorSelectionPageAcync()
        {
            if (ColorSelectionPage.IsPageOpened)
                return;

            ColorSelectionPageCreation colorSelectionPageCreation = 
                new ColorSelectionPageCreation(_builderPackNoteViewModel
                                                .SetModel(_baseEditorPackNoteModel.BasePackNoteModel)
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
        private void Save() => _baseEditorPackNoteModel.Save();
        private void PackNoteSavedEventHandler(object sender, ReadOnlyPackNote readOnlyPackNote) => ReturnNavigationPageAsync();
        private async void ReturnNavigationPageAsync() => await Navigation.PopModalAsync();
        private void OnSelectedRepeadChanged(object sender, RepeadItem e)
        {
            OnPropertyChanged(nameof(SelectedRepeadText));
        }
    }
}
